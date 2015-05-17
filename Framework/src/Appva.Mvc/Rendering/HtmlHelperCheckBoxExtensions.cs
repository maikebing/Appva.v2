// <copyright file="HtmlHelperCheckBoxExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Core.Extensions;
    using JetBrains.Annotations;
    using Resources;

    #endregion

    /// <summary>
    ///  Checkbox-related extensions for <see cref="HtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperCheckBoxExtensions
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
        public static MvcHtmlString CheckBoxWithLabelFor<TModel>([NotNull] this HtmlHelper<TModel> htmlHelper, string labelText, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            var label = new TagBuilder(Tags.Label);
            label.Attributes.Add(TagAttributes.For, htmlHelper.IdFor(expression).ToString());
            if (htmlAttributes.IsNotNull())
            {
                label.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            label.AddCssClass("checkbox-inline");
            label.InnerHtml = htmlHelper.CheckBoxFor(expression).ToHtmlString() + labelText;
            return MvcHtmlString.Create(label.ToString());
        }
    }
}