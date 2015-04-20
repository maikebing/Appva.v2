// <copyright file="Authentication.cs" company="Appva AB">
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
    using System.Security.Claims;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Validation;

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
        /// The <see cref="AuthenticationMethod"/> used.
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
        /// <param name="identity">The <see cref="IIdentityService"/> implementation</param>
        /// <param name="tenants">The <see cref="ITenantService"/> implementation</param>
        /// <param name="settings">The <see cref="ISettingsService"/> implementation</param>
        /// <param name="method">The authentication method used</param>
        /// <param name="type">The authentication type used</param>
        protected Authentication(AuthenticationMethod method, AuthenticationType type)
        {
            Requires.NotNull(method, "method");
            Requires.NotNull(type, "type");
            this.method = method;
            this.type = type;
        }

        #endregion

        #region IAuthentication Members.

        /// <inheritdoc />
        public virtual void SignIn(Account account, bool isPersistent = false)
        {
            Requires.NotNull(account, "account");
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
            this.RevokeToken(this.method, this.type);
        }

        #endregion

        #region Abstract Members.

        /// <summary>
        /// Issues a new authenticated token.
        /// </summary>
        /// <param name="principal">The current claims principal</param>
        /// <param name="expiresUtc">The token lifetime</param>
        /// <param name="isPersistent">Optional; whether or not to persist the cookie, defaults to false</param>
        protected abstract void IssueToken(ClaimsPrincipal principal, AuthenticationMethod method, AuthenticationType type, TimeSpan? expiresUtc = null, bool isPersistent = false);

        /// <summary>
        /// Revokes the current authenticated token.
        /// </summary>
        protected abstract void RevokeToken(AuthenticationMethod method, AuthenticationType type);

        /// <summary>
        /// The <see cref="Claim"/> creation for the authenticated user account.
        /// </summary>
        /// <param name="account">The user account</param>
        /// <param name="method">The authentication method</param>
        /// <returns>A collection of user account claims</returns>
        protected abstract IEnumerable<Claim> IssueClaims(Account account, AuthenticationMethod method);

        #endregion
    }
}