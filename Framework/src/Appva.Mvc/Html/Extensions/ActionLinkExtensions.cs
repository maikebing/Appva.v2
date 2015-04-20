// <copyright file="ActionLinkExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Html.Extensions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// Html helper extensions.
    /// </summary>
    public static class ActionLinkExtensions
    {
        private const string AclIsEnabledClaimType = "https://schemas.appva.se/2015/04/acl/claims/enabled";
        private const string AclIsPreviewClaimType = "https://schemas.appva.se/2015/04/acl/claims/preview";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="permission"></param>
        /// <param name="linkText"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="areaName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PermissionLink(
            this HtmlHelper htmlHelper, 
            string permission, 
            string linkText, 
            string actionName,
            string controllerName, 
            string areaName = null, 
            object routeValues = null,
            object htmlAttributes = null)
        {
            var principal = htmlHelper.ViewContext.HttpContext.User as ClaimsPrincipal;
            if (principal == null || principal.Identity == null || ! principal.Identity.IsAuthenticated)
            {
                return MvcHtmlString.Empty;
            }
            //// If Access control is enabled or in preview mode for appva administrative account role
            //// then verify that the user account has permission to view the link.
            if (principal.HasClaim(AclIsEnabledClaimType, "Y") || (principal.HasClaim(AclIsPreviewClaimType, "Y") && principal.IsInRole("_AA")))
            {
                if (! principal.HasClaim(Appva.Core.Identity.Identity.Permission, permission))
                {
                    return MvcHtmlString.Empty;
                }
            }
            //// The user account has access to the link due to either access control is not enabled 
            //// or the permission requirement is fulfilled.
            var routes = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            if (areaName.IsNotEmpty())
            {
                routes.Add("Area", areaName);
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName, routes, htmlAttributes);
        }
    }
}