// <copyright file="HtmlHelperInputExtensions.cs" company="Appva AB">
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
    using JetBrains.Annotations;
    using Rendering.Html;
    using Resources;

    #endregion

    /// <summary>
    /// Input-related extensions for <see cref="HtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperInputExtensions
    {
        /// <summary>
        /// Creates a form group with elements.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <typeparam name="TProperty">The property type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="expression">The expression</param>
        /// <param name="group">The outer element css class; defaults to 'form-group'</param>
        /// <returns>A <see cref="FormGroupFor{TModel,TProperty}"/></returns>
        public static FormGroupFor<TModel, TProperty> FormGroup<TModel, TProperty>([NotNull] this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string group = null)
        {
            return new FormGroupFor<TModel, TProperty>(htmlHelper, expression, group);
        }

        /// <summary>
        /// Creates a submit input element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="text">The submit text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString Submit([NotNull] this HtmlHelper htmlHelper, string text, object htmlAttributes = null)
        {
            var button = new TagBuilder(Tags.Input);
            button.Attributes.Add(TagAttributes.Type, "submit");
            button.Attributes.Add(TagAttributes.Value, text);
            if (htmlAttributes != null)
            {
                button.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            return MvcHtmlString.Create(button.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Creates a submit button element (not an submit input element).
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="text">The submit text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString Button([NotNull] this HtmlHelper htmlHelper, string text, object htmlAttributes = null)
        {
            var button = new TagBuilder(Tags.Button);
            button.Attributes.Add(TagAttributes.Type, "submit");
            button.SetInnerText(text);
            if (htmlAttributes != null)
            {
                button.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            button.AddCssClass("btn");
            return MvcHtmlString.Create(button.ToString());
        }

        /// <summary>
        /// Creates a file input element.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="expression">The property expression</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString File<TModel, TValue>([NotNull] this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return File(htmlHelper, expression, null, htmlAttributes);
        }

        /// <summary>
        /// Creates a file input element.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="expression">The property expression</param>
        /// <param name="text">The text message</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString File<TModel, TValue>([NotNull] this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string text, object htmlAttributes = null)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var span = new TagBuilder(Tags.Span);
            var input = new TagBuilder(Tags.Input);
            input.Attributes.Add(TagAttributes.Id, HtmlHelper.GenerateIdFromName(modelMetadata.PropertyName));
            input.Attributes.Add(TagAttributes.Name, modelMetadata.PropertyName);
            input.Attributes.Add(TagAttributes.Type, "file");
            if (htmlAttributes != null)
            {
                span.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            span.AddCssClass("btn btn-block btn-default btn-file");
            span.InnerHtml = (string.IsNullOrEmpty(text) ? modelMetadata.DisplayName : text) + " " + input.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(span.ToString());
        }

        /// <summary>
        /// Creates an HTML label element with an asterisk (*) suffix.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="expression">The property expression</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString LabelWithAsteriskFor<TModel, TValue>([NotNull] this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return LabelWithAsteriskFor(htmlHelper, expression, null, htmlAttributes);
        }

        /// <summary>
        /// Creates an HTML label element with an asterisk (*) suffix.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="expression">The property expression</param>
        /// <param name="labelText">The label text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString LabelWithAsteriskFor<TModel, TValue>([NotNull] this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes = null)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var label = new TagBuilder(Tags.Label);
            label.Attributes.Add(TagAttributes.For, HtmlHelper.GenerateIdFromName(modelMetadata.PropertyName));
            if (htmlAttributes != null)
            {
                label.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            label.InnerHtml = (string.IsNullOrEmpty(labelText) ? modelMetadata.DisplayName : labelText) + " <span class=\"required\">*</span>";
            return MvcHtmlString.Create(label.ToString());
        }
    }
}