// <copyright file="HtmlHelperLinkExtension.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.BetaMvc
{
    #region Imports.

    using Appva.Core.Contracts.Permissions;
    using Appva.Mvc;
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class HtmlHelperLinkExtension
    {
        #region Static members.

        /// <summary>
        /// Begins the link.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="permission">The permission.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="fragment">The fragment.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static RestrictedMvcHtmlString BeginLink([NotNull] this HtmlHelper htmlHelper, IPermission permission, string actionName, string controllerName, string fragment, object routeValues, object htmlAttributes)
        {
            var link     = htmlHelper.ActionLink(permission, "HTMLLINKEXTENSIONHELPER", actionName, controllerName, fragment, routeValues, htmlAttributes);
            var startTag = new MvcHtmlString(link.ToString().Replace("HTMLLINKEXTENSIONHELPER</a>", ""));
            var endTag   = new MvcHtmlString("</a>");

            return new RestrictedMvcHtmlString(htmlHelper, startTag, endTag, link != MvcHtmlString.Empty);
        }

        #endregion
    }
}