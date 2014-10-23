// <copyright file="AuthenticationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Security.Claims;
    using Domain.Entities;
    using Validation;
    using Domain.Services;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="persistentCookie"></param>
        void SignIn(User user, bool? persistentCookie = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="method"></param>
        /// <param name="persistentCookie"></param>
        void SignIn(User user, string method, bool? persistentCookie = null);
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AuthenticationService : IAuthenticationService
    {
        #region Variables.

        /// <summary>
        /// 
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="userService">The <see cref="IUserService"/></param>
        protected AuthenticationService(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Abstract Members.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract ClaimsPrincipal GetCurrentPrincipal();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="tokenLifetime"></param>
        /// <param name="persistentCookie"></param>
        protected abstract void IssueToken(ClaimsPrincipal principal, TimeSpan? tokenLifetime = null, bool? persistentCookie = null);
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract void RevokeToken();

        #endregion

        #region IAuthenticationService Members.

        /// <inheritdocs />
        public void SignIn(User user, bool? persistentCookie = null)
        {
            this.SignIn(user, AuthenticationMethods.Password, persistentCookie);
        }

        /// <inheritdocs />
        public void SignIn(User user, string method, bool? persistentCookie = null)
        {
            Requires.NotNull(user, "user");
            Requires.NotNullOrWhiteSpace(method, "method");
            if (this.userService.IsPasswordExpired(user))
            {
                this.IssuePartialSignInToken(user, method);
                return;
            }
            var claims = this.GetClaims(user, method);
            this.IssueToken(new ClaimsPrincipal(new ClaimsIdentity(claims, method)), persistentCookie: persistentCookie);
        }

        /// <inheritdocs />
        public virtual void SignOut()
        {
            this.RevokeToken();
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="method"></param>
        private void IssuePartialSignInToken(User user, string method)
        {
            Requires.NotNull(user, "user");
            Requires.NotNullOrWhiteSpace(method, "method");
            var claims = this.GetClaims(user, method);
            this.IssueToken(new ClaimsPrincipal(new ClaimsIdentity(claims)), null, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private IEnumerable<Claim> GetClaims(User user, string method)
        {
            Requires.NotNull(user, "user");
            Requires.NotNullOrWhiteSpace(method, "method");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.AuthenticationMethod, method),
                new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToString("s"))
            };
            claims.AddRange(user.GetIdentificationClaims());
            return claims;
        }

        #endregion
    }
}