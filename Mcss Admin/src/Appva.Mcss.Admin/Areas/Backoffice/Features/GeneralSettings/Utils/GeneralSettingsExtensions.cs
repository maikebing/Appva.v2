// <copyright file="ListGeneralSettingsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Admin.Utils.Html
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class GeneralSettingsExtensions
    {
        /// <summary>
        /// Returns a label, textbox and a color picker.
        /// </summary>
        public static MvcHtmlString ColorInputGroup<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string[] classNames, string scriptName, dynamic colorObject = null)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string id = HtmlHelper.GenerateIdFromName(name) ?? metaData.PropertyName;

            string hexValue = "#000000";
            if (colorObject != null)
                hexValue = "#" + colorObject.R.ToString("X2") + colorObject.G.ToString("X2") + colorObject.B.ToString("X2");

            var container = new TagBuilder("div");
            if (classNames.Length > 0)
                container.MergeAttribute("class", classNames[0]);

            var label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.InnerHtml = htmlHelper.DisplayNameFor(expression).ToString();
            if (classNames.Length > 1)
                label.MergeAttribute("class", classNames[1]);

            var textBox = new TagBuilder("input");
            textBox.MergeAttribute("id", id);
            textBox.MergeAttribute("name", id);
            textBox.MergeAttribute("type", "text");
            textBox.MergeAttribute("value", hexValue);
            textBox.MergeAttribute("onchange", scriptName + "('" + id + "p', '" + id + "')");
            if (classNames.Length > 2)
                textBox.MergeAttribute("class", classNames[2]);

            var colorPicker = new TagBuilder("input");
            colorPicker.MergeAttribute("id", id + "p");
            colorPicker.MergeAttribute("name", id + "p");
            colorPicker.MergeAttribute("type", "color");
            colorPicker.MergeAttribute("value", hexValue);
            colorPicker.MergeAttribute("onchange", scriptName + "('" + id + "', '" + id + "p')");

            container.InnerHtml += label.ToString() + textBox.ToString(TagRenderMode.SelfClosing) + colorPicker.ToString(TagRenderMode.SelfClosing);

            return new MvcHtmlString(container.ToString());
        }
    }
}