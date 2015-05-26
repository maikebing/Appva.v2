// <copyright file="FormsAuthentication.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Extensions;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Tenant.Identity;
    using Microsoft.Owin.Security;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IFormsAuthentication : IAuthentication
    {
        /// <summary>
        /// Authenticates the user account by its unique Personal Identity Number.
        /// </summary>
        /// <param name="personalIdentityNumber">The unique Personal Identity Number</param>
        /// <param name="password">The password in clear text</param>
        /// <param name="result">The authentication result</param>
        /// <returns>True, if the authentication was successful</returns>
        bool AuthenticateWithPersonalIdentityNumberAndPassword(PersonalIdentityNumber personalIdentityNumber, string password, out IAuthenticationResult result);

        /// <summary>
        /// Authenticates the user account by its unique Personal Identity Number.
        /// </summary>
        /// <param name="username">The unique user name</param>
        /// <param name="password">The password in clear text</param>
        /// <param name="result">The authentication result</param>
        /// <returns>True, if the authentication was successful</returns>
        bool AuthenticateWithUserNameAndPassword(string username, string password, out IAuthenticationResult result);
    }

    /// <summary>
    /// The form (username/password) authentication implementation.
    /// </summary>
    public sealed class FormsAuthentication : Authentication, IFormsAuthentication
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/> implementation.
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="ITenantService"/> implementation.
        /// </summary>
        private readonly ITenantService tenants;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ISettingsService"/> implementation.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsAuthentication"/> class.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentityService"/> implementation</param>
        /// <param name="accountService">The <see cref="IAccountService"/> implementation</param>
        public FormsAuthentication(IIdentityService identity, ITenantService tenants, IAccountService accountService, ISettingsService settings)
            : base(AuthenticationMethod.Password, AuthenticationType.Administrative)
        {
            Requires.NotNull(identity, "identity");
            Requires.NotNull(identity, "tenants");
            Requires.NotNull(settings, "settings");
            this.identity = identity;
            this.tenants = tenants;
            this.accountService = accountService;
            this.settings = settings;
        }

        #endregion

        #region IFormsAuthentication Members.

        /// <inheritdoc />
        public bool AuthenticateWithPersonalIdentityNumberAndPassword(PersonalIdentityNumber personalIdentityNumber, string password, out IAuthenticationResult result)
        {
            var account = this.accountService.FindByPersonalIdentityNumber(personalIdentityNumber);
            result = this.Authenticate(account, password);
            this.Haasdf(account, result);
            return result.IsAuthorized;
        }

        /// <inheritdoc />
        public bool AuthenticateWithUserNameAndPassword(string username, string password, out IAuthenticationResult result)
        {
            var account = this.accountService.FindByUserName(username);
            result = this.Authenticate(account, password);
            this.Haasdf(account, result);
            return result.IsAuthorized;
        }

        #endregion

        #region Authentication Members.

        /// <inheritdoc />
        protected override void IssueToken(ClaimsPrincipal principal, AuthenticationMethod method, AuthenticationType type, TimeSpan? expiresUtc = null, bool isPersistent = false)
        {
            Requires.NotNull(principal, "principal");
            this.SignOut();
            if (principal.Identity.IsAuthenticated)
            {
                this.identity.SignIn(
                    new AuthenticationProperties
                    {
                        IsPersistent = isPersistent,
                        ExpiresUtc = expiresUtc.HasValue ? DateTime.UtcNow.Add(expiresUtc.Value) : (DateTimeOffset?) null
                    }, 
                    new ClaimsIdentity(principal.Claims, type.Value));
            }
            
        }

        /// <inheritdoc />
        protected override void RevokeToken(AuthenticationMethod method, AuthenticationType type)
        {
            this.identity.SignOut(type.Value);
        }

        /// <inheritdoc />
        protected override IEnumerable<Claim> IssueClaims(Account account, AuthenticationMethod method)
        {
            var retval = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString("D")),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.AuthenticationMethod, method.Value),
                new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToString("s")),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(1).ToString("s")),
                new Claim(Core.Resources.ClaimTypes.Taxon, account.Taxon.Id.ToString())
            };
            if (this.settings.IsAccessControlListInstalled() && this.settings.IsAccessControlListActivated())
            {
                retval.Add(new Claim(Core.Resources.ClaimTypes.AclEnabled, "Y"));
            }
            ITenantIdentity identity = null;
            if (this.tenants.TryIdentifyTenant(out identity))
            {
                retval.Add(new Claim(Core.Resources.ClaimTypes.Tenant, identity.Id.Value));
            }
            retval.AddRange(this.accountService.Roles(account).Select(x => new Claim(ClaimTypes.Role, x.MachineName)).ToList());
            retval.AddRange(this.accountService.Permissions(account).Select(x => new Claim(Core.Resources.ClaimTypes.Permission, x.Resource)).ToList());
            return retval;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private IAuthenticationResult Authenticate(Account account, string password)
        {
            if (account == null)
            {
                return AuthenticationResult.NotFound;
            }
            if (! account.IsActive)
            {
                return AuthenticationResult.Failure;
            }
            if (account.IsPaused)
            {
                return AuthenticationResult.Failure;
            }
            if (account.IsLockout())
            {
                return AuthenticationResult.Lockout;
            }
            if (account.IsIncorrectPassword(password))
            {
                return AuthenticationResult.InvalidCredentials;
            }
            if (this.settings.IsAccessControlListActivated())
            {
                if (! this.accountService.HasPermissions(account, Permissions.Admin.Login.Value))
                {
                    return AuthenticationResult.Failure;
                }
            }
            else if (! this.accountService.IsInRoles(account, Core.Resources.RoleTypes.Backend))
            {
                return AuthenticationResult.Failure;
            }
            return AuthenticationResult.CreateSuccessResult(account);
        }

        private void Haasdf(Account account, IAuthenticationResult result)
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