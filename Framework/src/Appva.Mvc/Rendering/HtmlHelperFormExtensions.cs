// <copyright file="HtmlHelperFormExtensions.cs" company="Appva AB">
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
    using System.Web.Routing;
    using JetBrains.Annotations;
    using Resources;

    #endregion

    /// <summary>
    /// Form-related extensions for <see cref="HtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperFormExtensions
    {
        /// <summary>
        /// Creates a HTTP GET request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Get([NotNull] this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return Get(htmlHelper, null, null, null, htmlAttributes);
        }

        /// <summary>
        /// Creates a HTTP GET request form.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="routeValues">The route values</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Get([NotNull] this HtmlHelper htmlHelper, object routeValues, object htmlAttributes)
        {
            return Get(htmlHelper, null, null, routeValues, htmlAttributes);
        }

        /// <summary>
        /// Creates a HTTP GET request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="actionName">The action name</param>
        /// <param name="controllerName">The controller name</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Get([NotNull] this HtmlHelper htmlHelper, string actionName, string controllerName, object htmlAttributes)
        {
            return Get(htmlHelper, actionName, controllerName, null, htmlAttributes);
        }

        /// <summary>
        /// Creates a HTTP GET request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="actionName">The action name</param>
        /// <param name="controllerName">The controller name</param>
        /// <param name="routeValues">The route values</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Get([NotNull] this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var formAction = urlHelper.Action(actionName, controllerName, new RouteValueDictionary(routeValues));
            var form = new TagBuilder(Tags.Form);
            if (htmlAttributes != null)
            {
                form.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            form.MergeAttribute(TagAttributes.Action, formAction);
            form.MergeAttribute(TagAttributes.Method, "get");
            form.MergeAttribute(TagAttributes.AcceptCharset, "utf-8");
            form.MergeAttribute(TagAttributes.Role, Tags.Form);
            htmlHelper.ViewContext.Writer.Write(form.ToString(TagRenderMode.StartTag));
            return new MvcForm(htmlHelper.ViewContext);
        }

        /// <summary>
        /// Creates an HTTP POST request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <param name="routeValues">Optional route values</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm PostWithAttachment([NotNull] this HtmlHelper htmlHelper, object htmlAttributes = null, object routeValues = null)
        {
            return Post(htmlHelper, null, null, routeValues, htmlAttributes, true);
        }

        /// <summary>
        /// Creates an HTTP POST request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <param name="routeValues">Optional route values</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Post([NotNull] this HtmlHelper htmlHelper, object htmlAttributes = null, object routeValues = null)
        {
            return Post(htmlHelper, null, null, routeValues, htmlAttributes, false);
        }

        /// <summary>
        /// Creates an HTTP POST request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="actionName">The action name</param>
        /// <param name="controllerName">The controller name</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Post([NotNull] this HtmlHelper htmlHelper, string actionName, string controllerName, object htmlAttributes)
        {
            return Post(htmlHelper, actionName, controllerName, null, htmlAttributes, false);
        }

        /// <summary>
        /// Creates an HTTP POST request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="actionName">The action name</param>
        /// <param name="controllerName">The controller name</param>
        /// <param name="routeValues">The route values</param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <param name="isMultiPart">Whether or not there should be file upload control</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Post([NotNull] this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, object htmlAttributes, bool isMultiPart)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var formAction = urlHelper.Action(actionName, controllerName, new RouteValueDictionary(routeValues));
            var form = new TagBuilder(Tags.Form);
            if (htmlAttributes != null)
            {
                form.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            form.MergeAttribute(TagAttributes.Action, formAction);
            form.MergeAttribute(TagAttributes.Method, "post");
            form.MergeAttribute(TagAttributes.AcceptCharset, "utf-8");
            form.MergeAttribute(TagAttributes.Role, Tags.Form);
            if (isMultiPart)
            {
                form.MergeAttribute(TagAttributes.EncType, "multipart/form-data");
            }
            form.AddCssClass("form");
            htmlHelper.ViewContext.Writer.Write(form.ToString(TagRenderMode.StartTag));
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken());
            return new MvcForm(htmlHelper.ViewContext);
        }

        /// <summary>
        /// Creates a validation alert if the model state is invalid.
        /// </summary>
        /// <typeparam name="TModel">The model type</typeparam>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="header">The validation title</param>
        /// <param name="body">The validation message</param>
        /// <param name="htmlAttributes">Optional HTML attributes </param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString ValidationAlert<TModel>([NotNull] this HtmlHelper<TModel> htmlHelper, string header, string body, object htmlAttributes = null)
        {
            if (htmlHelper.ViewData.ModelState.IsValid)
            {
                return MvcHtmlString.Empty;
            }
            var title = new TagBuilder(Tags.Span);
            var alert = new TagBuilder(Tags.Div);
            alert.Attributes.Add(TagAttributes.Role, "alert");
            if (htmlAttributes != null)
            {
                alert.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            alert.AddCssClass("alert alert-danger");
            title.AddCssClass("title");
            title.SetInnerText(header);
            alert.InnerHtml = title + " " + body;
            return MvcHtmlString.Create(alert.ToString());
        }
    }
}