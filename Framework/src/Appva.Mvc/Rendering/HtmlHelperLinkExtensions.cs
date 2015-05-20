// <copyright file="HtmlHelperLinkExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    //using System.Security;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Appva.Core.Contracts.Permissions;
    using Core.Extensions;
    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// Link-related extensions for <see cref="HtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperLinkExtensions
    {
        /// <summary>
        /// Returns a permission link.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="permission">The permission which the user account must be a member of</param>
        /// <param name="linkText">The link  text</param>
        /// <param name="routeValues">The route values</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An action link if permitted; otherwise nothing</returns>
        public static MvcHtmlString ActionLink([NotNull] this HtmlHelper htmlHelper, IPermission permission, string linkText, object routeValues = null, object htmlAttributes = null)
        {
            return htmlHelper.ActionLink(permission, linkText, routeValues, htmlAttributes);
        }

        /// <summary>
        /// Returns a permission link.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="permission">The permission which the user account must be a member of</param>
        /// <param name="linkText">The link  text</param>
        /// <param name="actionName">The action name</param>
        /// <param name="routeValues">The route values</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An action link if permitted; otherwise nothing</returns>
        public static MvcHtmlString ActionLink([NotNull] this HtmlHelper htmlHelper, IPermission permission, string linkText, string actionName, object routeValues = null, object htmlAttributes = null)
        {
            return htmlHelper.ActionLink(permission, linkText, actionName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// Returns a permission link.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="permission">The permission which the user account must be a member of</param>
        /// <param name="linkText">The link  text</param>
        /// <param name="actionName">The action name</param>
        /// <param name="controllerName">The controller name</param>
        /// <param name="routeValues">The route values</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An action link if permitted; otherwise nothing</returns>
        public static MvcHtmlString ActionLink([NotNull] this HtmlHelper htmlHelper, IPermission permission, string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            var principal = htmlHelper.ViewContext.HttpContext.User as ClaimsPrincipal;
            if (principal == null || principal.Identity == null || ! principal.Identity.IsAuthenticated)
            {
                return MvcHtmlString.Empty;
            }
            //// If Access control is enabled or in preview mode for appva administrative account role
            //// then verify that the user account has permission to view the link.
            if (principal.HasClaim(Core.Resources.ClaimTypes.AclEnabled, "Y") || (principal.HasClaim(Core.Resources.ClaimTypes.AclPreview, "Y") && principal.IsInRole(Core.Resources.RoleTypes.Appva)))
            {
                if (! principal.HasClaim(permission.Key, permission.Value))
                {
                    return MvcHtmlString.Empty;
                }
            }
            //// The user account has access to the link due to either access control is not enabled 
            //// or the permission requirement is fulfilled.
            var routes = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            return htmlHelper.ActionLink(linkText, actionName, controllerName, routes, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
    }
}