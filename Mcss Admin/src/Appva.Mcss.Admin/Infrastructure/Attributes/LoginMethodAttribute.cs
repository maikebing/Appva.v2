// <copyright file="LoginMethodAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure.Attributes
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RequiredAuthenticationMethodAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        #region Fields.

        /// <summary>
        /// The method
        /// </summary>
        private string method;

        /// <summary>
        /// The method
        /// </summary>
        private string unauthorizedView;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredAuthenticationMethodAttribute"/> class.
        /// </summary>
        public RequiredAuthenticationMethodAttribute(string method)
        {
            this.method = method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredAuthenticationMethodAttribute"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="view">The view.</param>
        public RequiredAuthenticationMethodAttribute(string method, string unauthorizedView)
        {
            this.method           = method;
            this.unauthorizedView = unauthorizedView;
        }

        #endregion

        #region IAuthenticationFilter members.

        /// <inheritdoc />
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!filterContext.Principal.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            var principal = filterContext.Principal as ClaimsPrincipal;
            if (principal == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            if (principal.HasClaim(Core.Resources.ClaimTypes.AclEnabled, "Y"))
            {
                if (!principal.HasClaim(ClaimTypes.AuthenticationMethod, this.method))
                {
                    filterContext.Result = this.SetResult(filterContext);
                    return;
                }
            }
        }

        /// <inheritdoc />
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            return;
        }

        #endregion

        #region Private members.

        /// <summary>
        /// Creates an ActionResult to return
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private ActionResult SetResult(AuthenticationContext filterContext)
        {
            if(this.unauthorizedView.IsEmpty()){
                return new HttpUnauthorizedResult();
            }
            return new ViewResult
            {
                ViewName = this.unauthorizedView,
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData
            };
        }

        #endregion
    }
}