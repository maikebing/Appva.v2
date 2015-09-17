// <copyright file="JwtSecureDataFormat.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.ServiceModel.Security.Tokens;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Jwt;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Microsoft.Owin.Security;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class JwtSecureDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        #region Variables.

        /// <summary>
        /// The identity claim.
        /// </summary>
        private const string JwtIdentityClaim = "nameid";

        /// <summary>
        /// The <see cref="JwtSecurityTokenHandler"/>.
        /// </summary>
        private readonly JwtSecurityTokenHandler handler;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtSecureDataFormat"/> class.
        /// </summary>
        /// <param name="handler">The <see cref="JwtSecurityTokenHandler"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="accounts">The <see cref="IAccountService"/></param>
        public JwtSecureDataFormat(JwtSecurityTokenHandler handler, ISettingsService settings, IAccountService accounts)
        {
            this.settings = settings;
            this.handler = handler;
            this.accounts = accounts;
        }

        #endregion

        #region Internal Enums.

        /// <summary>
        /// The create token type.
        /// </summary>
        internal enum TokenType
        {
            /// <summary>
            /// The reset password token.
            /// </summary>
            ResetToken,

            /// <summary>
            /// The register new user token.
            /// </summary>
            RegistrationToken
        }

        #endregion

        #region ISecureDataFormat<AuthenticationTicket> Members.

        /// <inheritdoc />
        public string Protect(AuthenticationTicket ticket)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public AuthenticationTicket Unprotect(string protectedText)
        {
            Requires.NotNullOrWhiteSpace(protectedText, "protectedText");
            var token = this.handler.ReadToken(protectedText) as JwtSecurityToken;
            Requires.ValidState(token != null, "Invalid token");
            Requires.ValidState(token.Claims.Any(x => x.Type == JwtIdentityClaim), "Invalid token");
            var user = this.accounts.Find(new Guid(token.Claims.Where(x => x.Type == JwtIdentityClaim).First().Value));
            Requires.ValidState(user != null, "Invalid token");
            SecurityToken validatedToken;
            var configuration = this.settings.SecurityTokenConfiguration();
            var principal = this.handler.ValidateToken(
                    protectedText,
                    this.CreateNewTokenValidationParameters(JwtToken.CreateSymmetricKey(configuration.SigningKey, user.SymmetricKey), configuration.Issuer, configuration.Audience),
                    out validatedToken);
            var properties = new AuthenticationProperties
            {
                AllowRefresh = true
            };
            if (validatedToken.ValidFrom != DateTime.MinValue)
            {
                properties.IssuedUtc = validatedToken.ValidFrom.ToUniversalTime();
            }
            if (validatedToken.ValidTo != DateTime.MinValue)
            {
                properties.ExpiresUtc = validatedToken.ValidTo.ToUniversalTime();
            }
            var identity = (ClaimsIdentity) principal.Identity;
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName));
            return new AuthenticationTicket((ClaimsIdentity) principal.Identity, properties);
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Creates a new reset password token.
        /// </summary>
        /// <param name="id">The user account ID</param>
        /// <param name="base64Key">The user account partial symmetric signing key as base64</param>
        /// <returns>A JWT token as string</returns>
        public string CreateNewResetPasswordToken(Guid id, string base64Key)
        {
            return this.CreateNewJwtToken(id, base64Key, TokenType.ResetToken);
        }

        /// <summary>
        /// Creates a new registration token.
        /// </summary>
        /// <param name="id">The user account ID</param>
        /// <param name="base64Key">The user account partial symmetric signing key as base64</param>
        /// <returns>A JWT token as string</returns>
        public string CreateNewRegistrationToken(Guid id, string base64Key)
        {
            return this.CreateNewJwtToken(id, base64Key, TokenType.RegistrationToken);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates a new JWT token.
        /// </summary>
        /// <param name="id">The user account ID</param>
        /// <param name="base64Key">The user account partial symmetric signing key as base64</param>
        /// <param name="tokenType">The token type</param>
        /// <returns>A JWT token as string</returns>
        private string CreateNewJwtToken(Guid id, string base64Key, TokenType tokenType)
        {
            var key = base64Key.FromBase64();
            if (key.Length != 32)
            {
                throw new SecurityTokenException(string.Format("Incorrect key length. Expected [32], actual [{0}]", key.Length));
            }
            var configuration = this.settings.SecurityTokenConfiguration();
            var systemKey = configuration.SigningKey.FromBase64();
            var bytes = new byte[systemKey.Length + key.Length];
            Buffer.BlockCopy(systemKey, 0, bytes, 0, systemKey.Length);
            Buffer.BlockCopy(key, 0, bytes, systemKey.Length, key.Length);
            var expiresAt = tokenType.Equals(TokenType.RegistrationToken) ? DateTime.Now.Add(configuration.RegistrationTokenLifetime) : DateTime.Now.Add(configuration.ResetTokenLifetime);
            var claims    = new List<Claim>
            {
                new Claim(Core.Resources.ClaimTypes.AclEnabled, "Y"),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            if (tokenType.Equals(TokenType.RegistrationToken))
            {
                claims.Add(new Claim(Core.Resources.ClaimTypes.Permission, Permissions.Token.Register));
            }
            if (tokenType.Equals(TokenType.ResetToken))
            {
                claims.Add(new Claim(Core.Resources.ClaimTypes.Permission, Permissions.Token.Reset));
            }
            return this.handler.WriteToken(JwtToken.CreateNew(
                bytes.ToBase64(),
                configuration.Issuer,
                configuration.Audience,
                expiresAt,
                claims));
        }

        /// <summary>
        /// Creates a new <c>TokenValidationParameters</c>.
        /// </summary>
        /// <param name="key">The signing key</param>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        /// <returns>A new <see cref="TokenValidationParameters"/> instance</returns>
        private TokenValidationParameters CreateNewTokenValidationParameters(byte[] key, string issuer, string audience)
        {
            return new TokenValidationParameters
            {
                ValidIssuer              = issuer,
                ValidAudience            = audience,
                AuthenticationType       = "JWT",
                ValidateAudience         = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime         = true,
                ValidateIssuer           = true,
                ValidateActor            = false,
                IssuerSigningToken       = new BinarySecretSecurityToken(key)
            };
        }

        #endregion
    }
}