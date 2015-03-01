// <copyright file="HtmlHelperExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Html.Extensions
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Elements;
    using Filters;

    #endregion

    /// <summary>
    /// Html helper extensions.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates a form group with elements.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <typeparam name="TProperty">The property type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="expression">The expression</param>
        /// <returns>A <see cref="FormGroupFor{TModel, TProperty}"/></returns>
        public static FormGroupFor<TModel, TProperty> FormGroup<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return new FormGroupFor<TModel, TProperty>(htmlHelper, expression);
        }

        /// <summary>
        /// Creates an alert message from a controller attribute.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
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
        /// Creates a span help text element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="text">The help text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
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