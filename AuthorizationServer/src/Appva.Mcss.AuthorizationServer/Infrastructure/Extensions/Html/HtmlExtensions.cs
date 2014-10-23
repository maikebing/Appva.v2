// <copyright file="HtmlExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Core.Extensions;
    using Infrastructure.Extensions.Html.Elements;
    using Models;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// @Html.FormGroup(x => x.Name)
        ///     .Label("Name", new { @class = "label" })
        ///     .TextBox(new { @class = "input" })
        ///     .Validate()
        ///     .Help("A help message").Build()
        /// </example>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static FormGroupFor<TModel, TProperty> FormGroup<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return new FormGroupFor<TModel, TProperty>(htmlHelper, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString IsSetFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = modelMetadata.Model;
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                var alert = new TagBuilder("span");
                alert.Attributes.Add("role", "alert");
                alert.SetInnerText("Not set");
                if (htmlAttributes != null)
                {
                    alert.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
                }
                alert.AddCssClass("label label-danger");
                return MvcHtmlString.Create(alert.ToString());
            }
            return MvcHtmlString.Create(value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString IsActive<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (bool) modelMetadata.Model;
            var alert = new TagBuilder("span");
            alert.AddCssClass("label label-" + ((value) ? "info" : "danger"));
            alert.SetInnerText((value) ? "Active" : "Inactive");
            return MvcHtmlString.Create(alert.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="iconHtmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString ProfilePicture<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, object iconHtmlAttributes = null)
            where TProperty : Thumbnail
        {
            return Thumbnail(htmlHelper, expression, htmlAttributes, iconHtmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="iconHtmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Logotype<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, object iconHtmlAttributes = null)
            where TProperty : Thumbnail
        {
            return Thumbnail(htmlHelper, expression, htmlAttributes, iconHtmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="iconHtmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Thumbnail<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, object iconHtmlAttributes = null)
            where TProperty : Thumbnail
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var thumbnail = modelMetadata.Model as Thumbnail;
            return thumbnail.IsNotNull() ? Thumbnail(htmlHelper, thumbnail.FileName, thumbnail.MimeType, htmlAttributes, iconHtmlAttributes) : Thumbnail(htmlHelper, null, null, htmlAttributes, iconHtmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="iconHtmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Thumbnail<TModel>(this HtmlHelper<TModel> htmlHelper, object htmlAttributes = null, object iconHtmlAttributes = null)
        {
            return Thumbnail(htmlHelper, null, null, htmlAttributes, iconHtmlAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="iconHtmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Thumbnail(this HtmlHelper htmlHelper, string fileName, string mimeType, object htmlAttributes = null, object iconHtmlAttributes = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            if (fileName.IsEmpty() || mimeType.IsEmpty())
            {
                var inner = new TagBuilder("span");
                inner.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes), true);
                inner.AddCssClass("fa");
                var outer = new TagBuilder("span");
                outer.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);
                outer.AddCssClass("thumbnail-icon");
                outer.InnerHtml = inner.ToString();
                return new MvcHtmlString(outer.ToString());
            }
            var img = new TagBuilder("img");
            img.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);
            img.Attributes.Add("src", urlHelper.Action("Resolve", "Image", new {fileName, mimeType }));
            return new MvcHtmlString(img.ToString());
        }

        /// <summary>
        /// Creates a horizontal line break.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="htmlAttributes">The HTML attributes</param>
        /// <returns>An HR tag</returns>
        public static MvcHtmlString Hr(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            var hr = new TagBuilder("hr");
            hr.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new MvcHtmlString(hr.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Br(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            var hr = new TagBuilder("br");
            hr.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new MvcHtmlString(hr.ToString(TagRenderMode.SelfClosing));
        }
    }
}