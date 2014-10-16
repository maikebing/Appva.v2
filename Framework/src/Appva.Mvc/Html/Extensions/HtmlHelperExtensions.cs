// <copyright file="HtmlHelperExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Html.Extensions
{
    #region Imports.

    using System.Web.Mvc;
    using Filters;

    #endregion

    /// <summary>
    /// Html helper extensions.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Alert(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            var key = typeof(AbstractAlertAttribute).FullName;
            var text = htmlHelper.ViewContext.TempData[key] as string;
            if (string.IsNullOrEmpty(text))
            {
                return MvcHtmlString.Empty;
            }
            var alert = new TagBuilder("div");
            alert.Attributes.Add("role", "alert");
            alert.SetInnerText(text);
            if (htmlAttributes != null)
            {
                alert.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            alert.AddCssClass(htmlHelper.ViewContext.TempData[key + "class"] as string);
            return MvcHtmlString.Create(alert.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="text"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Help(this HtmlHelper htmlHelper, string text, object htmlAttributes = null)
        {
            var span = new TagBuilder("span");
            span.SetInnerText(text);
            if (htmlAttributes != null)
            {
                span.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            span.AddCssClass("help-block");
            return MvcHtmlString.Create(span.ToString());
        }
    }
}