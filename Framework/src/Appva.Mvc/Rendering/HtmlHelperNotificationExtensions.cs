// <copyright file="HtmlHelperNotificationExtensions.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using Resources;

    #endregion

    /// <summary>
    /// Html helper extensions.
    /// </summary>
    public static class HtmlHelperNotificationExtensions
    {
        /// <summary>
        /// Creates an alert message from a controller attribute.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString Alert([NotNull] this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            var key = typeof(AbstractAlertAttribute).FullName;
            var text = htmlHelper.ViewContext.TempData[key] as string;
            if (string.IsNullOrEmpty(text))
            {
                return MvcHtmlString.Empty;
            }
            var alert = new TagBuilder(Tags.Div);
            alert.Attributes.Add(TagAttributes.Role, "alert");
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
            var span = new TagBuilder(Tags.Span);
            span.SetInnerText(text);
            if (htmlAttributes != null)
            {
                span.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            span.AddCssClass("help-block");
            return MvcHtmlString.Create(span.ToString());
        }

        /// <summary>
        /// Serialize an object to json string.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="obj">The object to be serialized</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString Serialize(this HtmlHelper htmlHelper, object obj)
        {
            var code = new TagBuilder(Tags.Code);
            try
            {
                code.SetInnerText(JsonConvert.SerializeObject(obj));
            }
            catch (Exception)
            {
                code.SetInnerText(obj.GetType().AssemblyQualifiedName);
            }
            return MvcHtmlString.Create(code.ToString());
        }
    }
}