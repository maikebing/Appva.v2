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
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsAuthentication"/> class.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentityService"/></param>
        /// <param name="tenants">The <see cref="ITenantService"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        public FormsAuthentication(
            IIdentityService identity, 
            ITenantService tenants, 
            IAccountService accountService, 
            ISettingsService settings,
            IAuditService auditing)
            : base(identity, tenants, accountService, settings, auditing, AuthenticationMethod.Password, AuthenticationType.Administrative)
        {
            this.accountService = accountService;
        }

        #endregion

        #region IFormsAuthentication Members.

        /// <inheritdoc />
        public bool AuthenticateWithPersonalIdentityNumberAndPassword(PersonalIdentityNumber personalIdentityNumber, string password, out IAuthenticationResult result)
        {
            var account = this.accountService.FindByPersonalIdentityNumber(personalIdentityNumber);
            result = this.Authenticate(personalIdentityNumber, account, password);
            this.VerifyAuthenticationResult(account, result);
            return result.IsAuthorized;
        }

        /// <inheritdoc />
        public bool AuthenticateWithUserNameAndPassword(string username, string password, out IAuthenticationResult result)
        {
            var account = this.accountService.FindByUserName(username);
            result = this.Authenticate(username, account, password);
            this.VerifyAuthenticationResult(account, result);
            return result.IsAuthorized;
        }

        #endregion
    }
}