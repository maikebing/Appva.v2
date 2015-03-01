// <copyright file="FormExtensions.cs" company="Appva AB">
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
    using System.Web.Routing;

    #endregion

    /// <summary>
    /// Extension helpers for HtmlHelper.
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        /// Creates a HTTP GET request form element.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An <see cref="MvcForm"/></returns>
        public static MvcForm Get(this HtmlHelper htmlHelper, object htmlAttributes)
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
        public static MvcForm Get(this HtmlHelper htmlHelper, object routeValues, object htmlAttributes)
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
        public static MvcForm Get(this HtmlHelper htmlHelper, string actionName, string controllerName, object htmlAttributes)
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
        public static MvcForm Get(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var formAction = urlHelper.Action(actionName, controllerName, new RouteValueDictionary(routeValues));
            var form = new TagBuilder("form");
            if (htmlAttributes != null)
            {
                form.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            form.MergeAttribute("action", formAction);
            form.MergeAttribute("method", "get");
            form.MergeAttribute("accept-charset", "utf-8");
            form.MergeAttribute("role", "form");
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
        public static MvcForm PostWithAttachment(this HtmlHelper htmlHelper, object htmlAttributes = null, object routeValues = null)
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
        public static MvcForm Post(this HtmlHelper htmlHelper, object htmlAttributes = null, object routeValues = null)
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
        public static MvcForm Post(this HtmlHelper htmlHelper, string actionName, string controllerName, object htmlAttributes)
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
        public static MvcForm Post(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, object htmlAttributes, bool isMultiPart)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var formAction = urlHelper.Action(actionName, controllerName, new RouteValueDictionary(routeValues));
            var form = new TagBuilder("form");
            if (htmlAttributes != null)
            {
                form.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            form.MergeAttribute("action", formAction);
            form.MergeAttribute("method", "post");
            form.MergeAttribute("accept-charset", "utf-8");
            form.MergeAttribute("role", "form");
            if (isMultiPart)
            {
                form.MergeAttribute("enctype", "multipart/form-data");
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
        public static MvcHtmlString ValidationAlert<TModel>(this HtmlHelper<TModel> htmlHelper, string header, string body, object htmlAttributes = null)
        {
            if (! htmlHelper.ViewData.ModelState.IsValid)
            {
                var title = new TagBuilder("span");
                var alert = new TagBuilder("div");
                alert.Attributes.Add("role", "alert");
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
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// Creates a submit button element (not an submit input element).
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="text">The submit text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString Submit(this HtmlHelper htmlHelper, string text, object htmlAttributes = null)
        {
            var button = new TagBuilder("button");
            button.Attributes.Add("type", "submit");
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
        public static MvcHtmlString File<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
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
        public static MvcHtmlString File<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string text, object htmlAttributes = null)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var span = new TagBuilder("span");
            var input = new TagBuilder("input");
            input.Attributes.Add("id", HtmlHelper.GenerateIdFromName(modelMetadata.PropertyName));
            input.Attributes.Add("name", modelMetadata.PropertyName);
            input.Attributes.Add("type", "file");
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
        public static MvcHtmlString LabelWithAsteriskFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
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
        public static MvcHtmlString LabelWithAsteriskFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes = null)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var label = new TagBuilder("label");
            label.Attributes.Add("for", HtmlHelper.GenerateIdFromName(modelMetadata.PropertyName));
            if (htmlAttributes != null)
            {
                label.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            label.InnerHtml = (string.IsNullOrEmpty(labelText) ? modelMetadata.DisplayName : labelText) + " <span class=\"required\">*</span>";
            return MvcHtmlString.Create(label.ToString());
        }
    }
}