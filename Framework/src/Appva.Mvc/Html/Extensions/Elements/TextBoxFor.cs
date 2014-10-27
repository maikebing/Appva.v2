// <copyright file="TextBoxFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Html.Extensions.Elements
{
    #region Imports.

    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
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
        public override MvcHtmlString Build()
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(this.expression, this.Root.HtmlHelper.ViewData);
            var modelState = this.Root.HtmlHelper.ViewData.ModelState;
            var id = this.Root.HtmlHelper.IdFor(expression).ToString();
            var name = this.Root.HtmlHelper.IdFor(expression).ToString();
            var textbox = new TagBuilder("input");
            textbox.Attributes.Add("id", id);
            textbox.Attributes.Add("name", name);
            textbox.Attributes.Add("maxlength", "255");
            textbox.Attributes.Add("type", "text");
            if (this.placeholder.IsNotEmpty())
            {
                textbox.Attributes.Add("placeholder", this.placeholder);
            }
            if (htmlAttributes.IsNotNull())
            {
                textbox.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            textbox.AddCssClass("form-control");
            string value = null;
            if (modelState.ContainsKey(name))
            {
                value = Convert.ToString(modelState[name].Value, CultureInfo.InvariantCulture);
            }
            if (! modelMetadata.IsComplexType)
            {
                value = Convert.ToString(modelMetadata.Model, CultureInfo.InvariantCulture);
            }
            if (value.IsNotEmpty())
            {
                textbox.Attributes.Add("value", HttpUtility.HtmlEncode(value));
            }
            return MvcHtmlString.Create(textbox.ToString(TagRenderMode.SelfClosing));
        }

        #endregion
    }
}