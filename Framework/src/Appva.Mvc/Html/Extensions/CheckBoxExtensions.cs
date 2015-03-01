// <copyright file="CheckBoxExtensions.cs" company="Appva AB">
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
    using System.Web.Mvc.Html;
    using Core.Extensions;

    #endregion

    /// <summary>
    ///  Extension helpers for HtmlHelper.
    /// </summary>
    public static class CheckBoxExtensions
    {
        /// <summary>
        /// Creates a checkbox element.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="labelText">The label text</param>
        /// <param name="expression">The property expression</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString CheckBoxWithLabelFor<TModel>(
            this HtmlHelper<TModel> htmlHelper, 
            string labelText, 
            Expression<Func<TModel, bool>> expression, 
            object htmlAttributes = null)
        {
            var label = new TagBuilder("label");
            label.Attributes.Add("for", htmlHelper.IdFor(expression).ToString());
            if (htmlAttributes.IsNotNull())
            {
                label.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            label.AddCssClass("checkbox-inline");
            label.InnerHtml = htmlHelper.CheckBoxFor(expression)
                .ToHtmlString() + labelText;
            return MvcHtmlString.Create(label.ToString());
        }
    }
}