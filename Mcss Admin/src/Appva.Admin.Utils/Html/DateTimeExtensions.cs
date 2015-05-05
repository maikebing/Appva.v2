// <copyright file="DateTimeExtensions.cs" company="Appva AB">
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
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="condition"></param>
        /// <param name="dateIfTrue"></param>
        /// <param name="dateIfFalse"></param>
        /// <returns></returns>
        public static MvcHtmlString DateString(this HtmlHelper htmlHelper, bool condition, DateTime? dateIfTrue, DateTime? dateIfFalse)
        {
            return condition ? DateString(htmlHelper, dateIfTrue) : DateString(htmlHelper, dateIfFalse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static MvcHtmlString DateString(this HtmlHelper htmlHelper, DateTime? date)
        {
            var today = DateTime.Now.Date;
            var temp = (!date.HasValue) ? date.GetValueOrDefault() : date.Value;
            if (temp.Date.Equals(today))
            {
                return new MvcHtmlString("Idag");
            }
            if (temp.Date.Equals(today.AddDays(-1)))
            {
                return new MvcHtmlString("Igår");
            }
            return new MvcHtmlString(temp.Date.ToShortDateString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString Cancel(this HtmlHelper htmlHelper, string text)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", "#");
            tag.MergeAttribute("class", "cancel");
            tag.SetInnerText(text);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, 
            string linkText, string actionName, string controllerName, string areaName, object routevalues)
        {
            var tag = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.ParentActionViewContext.RouteData;
            var action = routeData.GetRequiredString("action");
            var controller = routeData.GetRequiredString("controller");
            var area = routeData.DataTokens["area"] as string;
            if (action.Equals(actionName) && controller.Equals(controllerName) && area.Equals(areaName))
            {
                tag.AddCssClass("sel");
            }
            tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routevalues, null).ToHtmlString();
            return MvcHtmlString.Create(tag.ToString());
        }


    }
}