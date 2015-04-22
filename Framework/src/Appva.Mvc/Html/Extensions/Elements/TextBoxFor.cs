// <copyright file="TextBoxFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Html.Extensions.Elements
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// Creates an HTML text input element.
    /// </summary>
    /// <typeparam name="TRoot">The root type</typeparam>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class TextBoxFor<TRoot, TModel, TProperty> : Element<TRoot>
        where TRoot : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.
        
        /// <summary>
        /// The html placeholder.
        /// </summary>
        private readonly string placeholder;

        /// <summary>
        /// The html attributes.
        /// </summary>
        private readonly object htmlAttributes;

        /// <summary>
        /// The text box expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="placeholder">Optional placeholder</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal TextBoxFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string placeholder = null, object htmlAttributes = null)
            : base(root)
        {
            this.expression = expression;
            this.placeholder = placeholder;
            this.htmlAttributes = htmlAttributes;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "Reviewed.")]
        public override MvcHtmlString Build()
        {
            /*
            var modelMetadata = ModelMetadata.FromLambdaExpression(this.expression, this.Root.HtmlHelper.ViewData);
            var modelState = this.Root.HtmlHelper.ViewData.ModelState;
            var id = this.Root.HtmlHelper.IdFor(this.expression).ToString();
            var name = this.Root.HtmlHelper.IdFor(this.expression).ToString();
            var textbox = new TagBuilder("input");
            textbox.Attributes.Add("id", id);
            textbox.Attributes.Add("name", name);
            textbox.Attributes.Add("maxlength", "255");
            textbox.Attributes.Add("type", "text");
            if (this.placeholder.IsNotEmpty())
            {
                textbox.Attributes.Add("placeholder", this.placeholder);
            }
            if (this.htmlAttributes.IsNotNull())
            {
                textbox.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes));
            }
            textbox.AddCssClass("form-control");
            string value = null;
            if (modelState.ContainsKey(name))
            {
                value = Convert.ToString(modelState[name].Value.RawValue, CultureInfo.InvariantCulture);
            }
            else if (! modelMetadata.IsComplexType)
            {
                value = Convert.ToString(modelMetadata.Model, CultureInfo.InvariantCulture);
            }
            if (value.IsNotEmpty())
            {
                textbox.Attributes.Add("value", HttpUtility.HtmlEncode(value));
            }
            return MvcHtmlString.Create(textbox.ToString(TagRenderMode.SelfClosing));
            */
            var htmlDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes);
            htmlDictionary.AddClass("form-control");
            if (this.placeholder.IsNotEmpty())
            {
                htmlDictionary.Add("placeholder", this.placeholder);
            }
            return this.Root.HtmlHelper.TextBoxFor(this.expression, htmlDictionary);
        }

        #endregion
    }
}