// <copyright file="OwinAuthenticationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>


namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Core.Extensions;
    using Domain.Services;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Validation;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class OwinAuthenticationService : AuthenticationService
    {
        #region Variables.

        /// <summary>
        /// 
        /// </summary>
        private readonly IOwinContext owinContext;

        /// <summary>
        /// 
        /// </summary>
        private readonly string authenticationType;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinAuthenticationService"/> class.
        /// </summary>
        public OwinAuthenticationService(string authenticationType, IUserService userService, IDictionary<string, object> environment)
            : base(userService)
        {
            this.owinContext = new OwinContext(environment);
            this.authenticationType = authenticationType;
        }

        #endregion

        /// <inheritdoc />
        protected override void IssueToken(ClaimsPrincipal principal, TimeSpan? tokenLifetime = null, bool? persistentCookie = null)
        {
            Requires.NotNull(principal, "principal");
            this.IssueCookie(principal.Claims,
                principal.Identity.IsAuthenticated
                    ? authenticationType
                    : AuthorizationServerOwinConstants.AuthenticationTwoFactorType, tokenLifetime, persistentCookie);
        }

        /// <inheritdoc />
        private void IssueCookie(IEnumerable<Claim> enumerable, string authType, TimeSpan? tokenLifetime, bool? persistentCookie)
        {
            this.SignOut();
            var properties = new AuthenticationProperties();
            if (tokenLifetime.HasValue)
            {
                properties.ExpiresUtc = DateTime.UtcNow.Add(tokenLifetime.Value);
            }
            if (persistentCookie.HasValue)
            {
                properties.IsPersistent = persistentCookie.Value;
            }
            this.owinContext.Authentication.SignIn(properties, new ClaimsIdentity(enumerable, authType));
        }

        /// <inheritdoc />
        protected override void RevokeToken()
        {
            this.owinContext.Authentication.SignOut(this.authenticationType, AuthorizationServerOwinConstants.AuthenticationTwoFactorType);
        }

        /// <inheritdoc />
        protected override ClaimsPrincipal GetCurrentPrincipal()
        {
            var user = this.owinContext.Request.User;
            if (user.IsNull() || ! user.Identity.AuthenticationType.Equals(authenticationType))
            {
                return null;
            }
            var principal = this.owinContext.Request.User as ClaimsPrincipal;
            return principal ?? new ClaimsPrincipal(this.owinContext.Request.User);
        }
    }
}