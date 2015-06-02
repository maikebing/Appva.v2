// <copyright file="HtmlHelperExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Admin.Utils.Html
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string text, string action, string controller)
        {
            return MenuLink(htmlHelper, text, action, controller, false, new
            {
            });
        }

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string text, string action, string controller, bool IgnoreAction)
        {
            return MenuLink(htmlHelper, text, action, controller, IgnoreAction, new
            {
            });
        }

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string text, string action, string controller, bool IgnoreAction, object routevalues)
        {
            var Element = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (!IgnoreAction)
            {
                if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
                {
                    Element.AddCssClass("sel");
                }
            }
            else
            {
                if (string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
                {
                    Element.AddCssClass("sel");
                }
            }
            Element.InnerHtml = htmlHelper.ActionLink(text, action, controller, routevalues, new
            {
            }).ToHtmlString();
            return MvcHtmlString.Create(Element.ToString());
        }
    }
}