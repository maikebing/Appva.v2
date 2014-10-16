// <copyright file="CheckBoxExtensions.cs" company="Appva AB">
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
    using Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class CheckBoxExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="labelText"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxWithLabelFor<TModel>(this HtmlHelper<TModel> htmlHelper,
          string labelText, Expression<Func<TModel, bool>> expression,
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