// <copyright file="HtmlHelperRadioButtonExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mvc.Rendering
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class HtmlHelperRadioButtonExtensions
    {
        /// <summary>
        /// Creates a radiobutton element
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="labelText">The label text</param>
        /// <param name="value"></param>
        /// <param name="expression">The property expression</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString RadioButtonWithLabelFor<TModel, TProperty>([NotNull] this HtmlHelper<TModel> htmlHelper, string labelText, object value, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var label = new TagBuilder(Tags.Label);
            label.Attributes.Add(TagAttributes.For, htmlHelper.IdFor(expression).ToString());
            if (htmlAttributes.IsNotNull())
            {
                label.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            label.InnerHtml = htmlHelper.RadioButtonFor<TModel,TProperty>(expression, value).ToHtmlString() + labelText;
            return MvcHtmlString.Create(label.ToString());
        }
    }
}