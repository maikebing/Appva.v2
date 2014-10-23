// <copyright file="FormExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
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
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcForm Get(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return Get(htmlHelper, null, null, null, htmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcForm Get(this HtmlHelper htmlHelper, object routeValues, object htmlAttributes)
        {
            return Get(htmlHelper, null, null, routeValues, htmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcForm Get(this HtmlHelper htmlHelper, string actionName, string controllerName, object htmlAttributes)
        {
            return Get(htmlHelper, actionName, controllerName, null, htmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcForm PostWithAttachment(this HtmlHelper htmlHelper, object htmlAttributes = null, object routeValues = null)
        {
            return Post(htmlHelper, null, null, routeValues, htmlAttributes, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcForm Post(this HtmlHelper htmlHelper, object htmlAttributes = null, object routeValues = null)
        {
            return Post(htmlHelper, null, null, routeValues, htmlAttributes, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcForm Post(this HtmlHelper htmlHelper, string actionName, string controllerName, object htmlAttributes)
        {
            return Post(htmlHelper, actionName, controllerName, null, htmlAttributes, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="isMultiPart"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="header"></param>
        /// <param name="body"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="text"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString File<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return File(htmlHelper, expression, null, htmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="text"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString LabelWithAsteriskFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return LabelWithAsteriskFor(htmlHelper, expression, null, htmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
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