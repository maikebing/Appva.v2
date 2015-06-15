// <copyright file="SithsAuthentication.cs" company="Appva AB">
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
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Siths;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISithsAuthenticationAsync
    {
        /// <summary>
        /// Returns the external identity provider (IdP) URI asynchronous.
        /// </summary>
        /// <returns>The IdP URI</returns>
        Task<Uri> ExternalLoginUrlAsync();

        /// <summary>
        /// Authenticates the external identity provider (IdP) response token asynchronous.
        /// </summary>
        /// <param name="token">The response token</param>
        /// <returns>A <see cref="IAuthenticationResult"/></returns>
        Task<IAuthenticationResult> AuthenticateTokenAsync(string token);

        /// <summary>
        /// Logs out the user account from the identity provider (IdP) asynchronous.
        /// </summary>
        /// <param name="token">The authentication token</param>
        /// <returns>A <see cref="Task{bool}"/>; true if successful</returns>
        Task<bool> LogoutAsync(string token);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISithsAuthentication : ISithsAuthenticationAsync, IService
    {
        /// <summary>
        /// Returns the external identity provider (IdP) URI.
        /// </summary>
        /// <returns>The IdP URI</returns>
        Uri ExternalLoginUrl();

        /// <summary>
        /// Authenticates the external identity provider (IdP) response token.
        /// </summary>
        /// <param name="hsaId">The response token</param>
        /// <param name="result">The authentication result</param>
        /// <returns>True, if the authentication was successful</returns>
        bool AuthenticateToken(string token, out IAuthenticationResult result);

        /// <summary>
        /// Logs out the user account from the identity provider (IdP).
        /// </summary>
        /// <param name="token">The authentication token</param>
        /// <returns>True if successful</returns>
        bool Logout(string token);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SithsAuthentication : Authentication, ISithsAuthentication
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISithsClient"/>.
        /// </summary>
        private readonly ISithsClient client;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SithsAuthentication"/> class.
        /// </summary>
        public SithsAuthentication(
            ISithsClient client, 
            IIdentityService identity, 
            ITenantService tenants, 
            IAccountService accounts, 
            ISettingsService settings,
            IAuditService auditing)
            : base(identity, tenants, accounts, settings, auditing, AuthenticationMethod.Siths, AuthenticationType.Administrative)
        {
            this.client = client;
            this.accounts = accounts;
        }

        #endregion

        #region ISithsAuthentication Members.

        /// <inheritdoc />
        public Uri ExternalLoginUrl()
        {
            return this.ExternalLoginUrlAsync().Result;
        }

        /// <inheritdoc />
        public bool AuthenticateToken(string token, out IAuthenticationResult result)
        {
            result = this.AuthenticateTokenAsync(token).Result;
            return result.IsAuthorized;
        }

        /// <inheritdoc />
        public bool Logout(string token)
        {
            return this.LogoutAsync(token).Result;
        }

        #endregion

        #region ISithsAuthenticationAsync Members.

        /// <inheritdoc />
        public async Task<Uri> ExternalLoginUrlAsync()
        {
            return await this.client.ExternalLoginUri();
        }

        /// <inheritdoc />
        public async Task<IAuthenticationResult> AuthenticateTokenAsync(string token)
        {
            var identity = await this.client.Identity(token);
            if (identity == null)
            {
                return AuthenticationResult.Failure;
            }
            var account = this.accounts.FindByHsaId(identity.HsaId);
            var result = this.Authenticate(identity.HsaId, account, null);
            this.VerifyAuthenticationResult(account, result);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> LogoutAsync(string token)
        {
            return await this.client.Logout(token);
        }

        #endregion
    }
}