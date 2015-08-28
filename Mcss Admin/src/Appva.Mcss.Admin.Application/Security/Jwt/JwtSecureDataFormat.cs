// <copyright file="SecureJwtTokenProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.ServiceModel.Security.Tokens;
    using Appva.Mcss.Admin.Application.Security.Jwt;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Extensions;
    using Microsoft.Owin.Security;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class JwtSecureDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        #region Variables.

        private const string JwtIdentityClaim = "nameid";

        /// <summary>
        /// The <see cref="JwtSecurityTokenHandler"/>.
        /// </summary>
        private readonly JwtSecurityTokenHandler handler;

        /// <summary>
        /// The <see cref="TokenValidationParameters"/>.
        /// </summary>
        private readonly TokenValidationParameters tokenValidationParameters;

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
        /// <param name="clientManager">The <see cref="IClientManager"/></param>
        public JwtSecureDataFormat(JwtSecurityTokenHandler handler, ISettingsService settings, IAccountService accounts)
        {
            this.settings = settings;
            this.handler = handler;
            this.accounts = accounts;
        }

        #endregion

        private TokenValidationParameters CreateNewTokenValidationParameters(byte[] key, string issuer, string audience)
        {
            return new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                AuthenticationType = "JWT",
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateActor = false,
                IssuerSigningToken = new BinarySecretSecurityToken(key)
            };
        }

        #region ISecureDataFormat<AuthenticationTicket> Members.

        /// <inheritdoc />
        public string Protect(AuthenticationTicket ticket)
        {
            Requires.NotNull(ticket, "ticket");
            return null;
        }

        /// <inheritdoc />
        public AuthenticationTicket Unprotect(string protectedText)
        {
            Requires.NotNullOrWhiteSpace(protectedText, "protectedText");
            var token = this.handler.ReadToken(protectedText) as JwtSecurityToken;
            Requires.ValidState(token != null, "Invalid token");
            Requires.ValidState(token.Claims.Any(x => x.Type == JwtIdentityClaim), "Invalid token");
            var id = new Guid(token.Claims.Where(x => x.Type == JwtIdentityClaim).First().Value);
            var user = this.accounts.Find(id);
            Requires.ValidState(user != null, "Invalid token");
            var config = this.settings.ResetPasswordTokenConfiguration();
            SecurityToken validatedToken;
            var principal = this.handler.ValidateToken(
                    protectedText,
                    CreateNewTokenValidationParameters(JwtToken.CreateSymmetricKey(config.Key, user.SymmetricKey), config.Issuer, config.Audience),
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
            var identity = (ClaimsIdentity)principal.Identity;
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName));
            return new AuthenticationTicket((ClaimsIdentity) principal.Identity, properties);
        }

        #endregion
    }
}