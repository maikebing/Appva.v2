// <copyright file="PermissionsAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using Appva.Core.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PermissionsAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        #region Variables.

        /// <summary>
        /// The role permissions which a user must a member of. 
        /// </summary>
        private string[] permissions;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsAttribute"/> class.
        /// </summary>
        /// <param name="permissions">The permissions which a user account must have</param>
        public PermissionsAttribute(params string[] permissions)
        {
            this.permissions = permissions;
        }

        #endregion

        #region IAuthenticationFilter Members.

        /// <inheritdoc />
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (! filterContext.Principal.Identity.IsAuthenticated)
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
            if (! principal.Claims.Where(x => x.Type == Identity.Permission && this.permissions.Contains(x.Value)).Any())
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
        }

        /// <inheritdoc />
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            return;
        }

        #endregion
    }
}