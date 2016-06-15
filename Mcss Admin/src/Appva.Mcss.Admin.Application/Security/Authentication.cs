﻿// <copyright file="Authentication.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Extensions;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Tenant.Identity;
    using Microsoft.Owin.Security;
    using Validation;
    using Appva.Core.Environment;

    #endregion

    /// <summary>
    /// Handles user account authentication.
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// Signs in a user account to the web application.
        /// </summary>
        /// <param name="account">The user account</param>
        /// <param name="isPersistent">Whether or not persistent cookies are enabled, defaults to false</param>
        void SignIn(Account account, bool isPersistent = false);

        /// <summary>
        /// Signs out the current user account from the web application.
        /// </summary>
        void SignOut();
    }

    /// <summary>
    /// The abstract base authentication service <see cref="IAuthentication"/> 
    /// implementation.
    /// </summary>
    public abstract class Authentication : IAuthentication
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenants;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="AuthenticationMethod"/>.
        /// </summary>
        private readonly AuthenticationMethod method;

        /// <summary>
        /// The authentication type currently enabled.
        /// </summary>
        private readonly AuthenticationType type;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentication"/> class.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentityService"/></param>
        /// <param name="tenants">The <see cref="ITenantService"/></param>
        /// <param name="accounts">The <see cref="IAccountService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        /// <param name="method">The authentication method used</param>
        /// <param name="type">The authentication type used</param>
        protected Authentication(
            IIdentityService identity,
            ITenantService tenants,
            IAccountService accounts,
            ISettingsService settings,
            IAuditService auditing,
            AuthenticationMethod method,
            AuthenticationType type)
        {
            Requires.NotNull(identity, "identity");
            Requires.NotNull(tenants, "tenants");
            Requires.NotNull(accounts, "accounts");
            Requires.NotNull(settings, "settings");
            Requires.NotNull(method, "method");
            Requires.NotNull(type, "type");
            this.identity = identity;
            this.tenants = tenants;
            this.accounts = accounts;
            this.settings = settings;
            this.auditing = auditing;
            this.method = method;
            this.type = type;
        }

        #endregion

        #region IAuthentication Members.

        /// <inheritdoc />
        public virtual void SignIn(Account account, bool isPersistent = false)
        {
            if (account == null)
            {
                return;
            }
            this.auditing.SignIn(account);
            this.IssueToken(
                new ClaimsPrincipal(
                    new ClaimsIdentity(this.IssueClaims(account, this.method), this.method.Value)),
                    this.method,
                    this.type,
                    null,
                    isPersistent);
        }

        /// <inheritdoc />
        public virtual void SignOut()
        {
            this.auditing.SignOut();
            this.RevokeToken(this.method, this.type);
        }

        #endregion

        #region Protected Members.

        /// <summary>
        /// Issues a new authenticated token.
        /// </summary>
        /// <param name="principal">The current claims principal</param>
        /// <param name="method">The authentication method</param>
        /// <param name="type">The authentication type</param>
        /// <param name="expiresUtc">The token lifetime</param>
        /// <param name="isPersistent">Optional; whether or not to persist the cookie, defaults to false</param>
        protected virtual void IssueToken(ClaimsPrincipal principal, AuthenticationMethod method, AuthenticationType type, TimeSpan? expiresUtc = null, bool isPersistent = false)
        {
            Requires.NotNull(principal, "principal");
            this.RevokeToken(method, type);
            if (principal.Identity.IsAuthenticated)
            {
                this.identity.SignIn(
                    new AuthenticationProperties
                    {
                        IsPersistent = isPersistent,
                        ExpiresUtc = expiresUtc.HasValue ? DateTime.UtcNow.Add(expiresUtc.Value) : (DateTimeOffset?)null
                    },
                    new ClaimsIdentity(principal.Claims, type.Value));
            }
        }

        /// <summary>
        /// Revokes the current authenticated token.
        /// </summary>
        /// <param name="method">The authentication method</param>
        /// <param name="type">The authentication type</param>
        protected virtual void RevokeToken(AuthenticationMethod method, AuthenticationType type)
        {
            this.identity.SignOut(type.Value);
        }

        /// <summary>
        /// The <see cref="Claim"/> creation for the authenticated user account.
        /// </summary>
        /// <param name="account">The user account</param>
        /// <param name="method">The authentication method</param>
        /// <returns>A collection of user account claims</returns>
        protected virtual IEnumerable<Claim> IssueClaims(Account account, AuthenticationMethod method)
        {
            var retval = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString("D")),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.AuthenticationMethod, method.Value),
                new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToString("s")),
                new Claim(Core.Resources.ClaimTypes.Taxon, account.Taxon.Id.ToString()),
                new Claim(ClaimTypes.Version, ApplicationEnvironment.Info.Version)
            };
            if (this.settings.IsAccessControlListInstalled() && this.settings.IsAccessControlListActivated())
            {
                retval.Add(new Claim(Core.Resources.ClaimTypes.AclEnabled, "Y"));
            }
            if (this.settings.IsSithsAuthorizationEnabled())
            {
                retval.Add(new Claim(Core.Resources.ClaimTypes.SithsEnabled, "Y"));
            }
            ITenantIdentity identity = null;
            if (this.tenants.TryIdentifyTenant(out identity))
            {
                retval.Add(new Claim(Core.Resources.ClaimTypes.TenantId, identity.Id.Value));
            }
            retval.AddRange(this.accounts.Roles(account).Select(x => new Claim(ClaimTypes.Role, x.MachineName)).ToList());
            retval.AddRange(this.accounts.Permissions(account).Select(x => new Claim(Core.Resources.ClaimTypes.Permission, x.Resource)).ToList());
            retval.AddRange(TaskService.GetAllRoleScheduleSettingsList(account).Select(x => new Claim(Core.Resources.ClaimTypes.SchedulePermission, x.Id.ToString())).ToList());
            return retval;
        }

        /// <summary>
        /// Authenticates the account and password and returns a result.
        /// </summary>
        /// <param name="credentials">The user name, hsa id or personal identity number</param>
        /// <param name="account">The account</param>
        /// <param name="password">The password</param>
        /// <returns>A <see cref="IAuthenticationResult"/></returns>
        protected IAuthenticationResult Authenticate(object credentials, Account account, string password)
        {
            if (account == null)
            {
                this.auditing.FailedAuthentication(null,   "Användare {0} misslyckades att autentisera p g a att användarkontot ej existerar", credentials);
                return AuthenticationResult.NotFound;
            }
            if (account.IsInactive())
            {
                this.auditing.FailedAuthentication(account, "misslyckades att autentisera p g a att användarkontot är inaktiverad.");
                return AuthenticationResult.Failure;
            }
            if (account.IsPaused)
            {
                this.auditing.FailedAuthentication(account, "misslyckades att autentisera p g a att användarkontot är pausad.");
                return AuthenticationResult.Failure;
            }
            if (account.IsLockout())
            {
                this.auditing.FailedAuthentication(account, "misslyckades att autentisera p g a att användarkontot är spärrat.");
                return AuthenticationResult.Lockout;
            }
            if (password != null && account.IsIncorrectPassword(password))
            {
                this.auditing.FailedAuthentication(account, "misslyckades att autentisera p g a felaktigt lösenord.");
                return AuthenticationResult.InvalidCredentials;
            }

            if (this.settings.IsAccessControlListActivated())
            {
                //// TODO: if (this.settings.IsAccessControlListActivated()) is temporary - this should not have to be checked later on.
                if (! this.accounts.HasPermissions(account, Permissions.Admin.Login.Value))
                {
                    this.auditing.FailedAuthentication(account, "misslyckades att autentisera p g a otillräcklig behörighet.");
                    return AuthenticationResult.Failure;
                }
            }
            else if (! this.accounts.IsInRoles(account, Core.Resources.RoleTypes.Backend))
            {
                //// TODO: if (! this.accounts.IsInRoles(account, Core.Resources.RoleTypes.Backend)) is temporary - this should be removed.
                this.auditing.FailedAuthentication(account, "misslyckades att autentisera p g a otillräcklig behörighet.");
                return AuthenticationResult.Failure;
            }
            return AuthenticationResult.CreateSuccessResult(account);
        }

        /// <summary>
        /// Verifies the authentication result and updates the account.
        /// </summary>
        /// <param name="account">The account to be authenticated</param>
        /// <param name="result">The authentication result</param>
        protected virtual void VerifyAuthenticationResult(Account account, IAuthenticationResult result)
        {
            if (account == null)
            {
                return;
            }
            if (! result.IsAuthorized)
            {
                if (! account.IsLockout())
                {
                    if (account.FailedPasswordAttemptsCount > 3)
                    {
                        account.Lockout(3);
                    }
                    else
                    {
                        account.IncrementFailedPasswordAttempts();
                    }
                }
            }
            else
            {
                if (account.FailedPasswordAttemptsCount > 0)
                {
                    account.ResetFailedPasswordAttempts();
                }
            }
        }

        #endregion
    }
}