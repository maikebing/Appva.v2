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
    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Appva.GrandId;
    using Appva.GrandId.Http.Response;
    using Appva.GrandId.Identity;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISithsAuthentication : IService
    {
        /// <summary>
        /// Returns the external identity provider (IdP) URI asynchronous.
        /// </summary>
        /// <param name="redirectUri">The redirect uri.</param>
        /// <returns>The <see cref="FederatedLogin"/>.</returns>
        Task<FederatedLogin> ExternalLoginUrlAsync(Uri redirectUri);

        /// <summary>
        /// Authenticates the external identity provider (IdP) response token asynchronous.
        /// </summary>
        /// <param name="sessionId">The session ID.</param>
        /// <returns>A <see cref="IAuthenticationResult"/>.</returns>
        Task<IAuthenticationResult> AuthenticateTokenAsync(string sessionId);

        /// <summary>
        /// Signs out the user account from the identity provider (IdP) asynchronous.
        /// </summary>
        /// <param name="sessionId">The session ID.</param>
        /// <returns>A <see cref="Task{bool}"/>; true if successful.</returns>
        Task<bool> LogoutAsync(string sessionId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SithsAuthentication : Authentication, ISithsAuthentication
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<SithsAuthentication>();

        /// <summary>
        /// The <see cref="IGrandIdClient"/>.
        /// </summary>
        private readonly IGrandIdClient client;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SithsAuthentication"/> class.
        /// </summary>
        /// <param name="client">The <see cref="ISithsClient"/></param>
        /// <param name="identity">The <see cref="IIdentityService"/></param>
        /// <param name="tenants">The <see cref="ITenantService"/></param>
        /// <param name="accounts">The <see cref="IAccountService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        public SithsAuthentication(
            IGrandIdClient client,
            IIdentityService identity,
            ITenantService tenants,
            IAccountService accounts,
            ISettingsService settings,
            IAuditService auditing)
            : base(identity, tenants, accounts, settings, auditing, AuthenticationMethod.Siths, AuthenticationType.Administrative)
        {
            this.client   = client;
            this.accounts = accounts;
        }

        #endregion

        #region ISithsAuthenticationAsync Members.

        /// <inheritdoc />
        public async Task<FederatedLogin> ExternalLoginUrlAsync(Uri callback)
        {
            var result = await this.client.FederatedLoginAsync(callback);
            if (result == null)
            {
                Log.Error("GrandID 'FederatedLogin' failed for callback {0}", callback);
                return null;
            }
            if (result.HasErrors)
            {
                Log.Error("GrandID 'FederatedLogin' failed due to {0}", result.Error.Message);
                return null;
            }
            return result;
        }

        /// <inheritdoc />
        public async Task<IAuthenticationResult> AuthenticateTokenAsync(string sessionId)
        {
            var response = await this.client.GetSessionAsync<SithsIdentity>(sessionId);
            if (response == null)
            {
                Log.Error("GrandID 'GetSession' failed for session ID {0}", sessionId);
                return AuthenticationResult.Failure;
            }
            if (response.HasErrors)
            {
                Log.Error("GrandID 'GetSession' failed due to {0}", response.Error.Message);
                return AuthenticationResult.Failure;
            }
            if (! response.IsAuthenticated)
            {
                Log.Error("GrandID 'GetSession' failed due to not authenticated for session ID {0}", sessionId);
                return AuthenticationResult.Failure;
            }
            var account = this.accounts.FindByHsaId(response.UserAttributes.HsaId);
            var result  = this.Authenticate(response.UserAttributes.HsaId, account, null);
            this.VerifyAuthenticationResult(account, result);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> LogoutAsync(string sessionId)
        {
            var response = await this.client.LogoutAsync(sessionId);
            if (response == null)
            {
                Log.Error("GrandID 'Logout' failed for session ID {0}", sessionId);
                return false;
            }
            if (response.HasErrors)
            {
                Log.Error("GrandID 'Logout' failed due to {0}", response.Error.Message);
                return false;
            }
            if (response.IsSessionDeleted == false)
            {
                Log.Warn("GrandID 'Logout' session is not deleted for session ID {0}", sessionId);
            }
            return response.IsSessionDeleted;
        }

        #endregion
    }
}