// <copyright file="TextAreaFor.cs" company="Appva AB">
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
    /// Creates an HTML textarea element.
    /// </summary>
    /// <typeparam name="TRoot">The root type</typeparam>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class TextAreaFor<TRoot, TModel, TProperty> : Element<TRoot>
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
        /// The text area expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAreaFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="placeholder">Optional placeholder</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal TextAreaFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string placeholder = null, object htmlAttributes = null)
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
            var modelMetadata = ModelMetadata.FromLambdaExpression(this.expression, this.Root.HtmlHelper.ViewData);
            var modelState = this.Root.HtmlHelper.ViewData.ModelState;
            var id = this.Root.HtmlHelper.IdFor(this.expression).ToString();
            var name = this.Root.HtmlHelper.IdFor(this.expression).ToString();
            var textarea = new TagBuilder("textarea");
            textarea.Attributes.Add("id", id);
            textarea.Attributes.Add("name", name);
            textarea.Attributes.Add("rows", "3");
            textarea.Attributes.Add("size", "255");
            textarea.Attributes.Add("maxlength", "255");
            if (this.placeholder.IsNotEmpty())
            {
                textarea.Attributes.Add("placeholder", this.placeholder);
            }
            if (this.htmlAttributes.IsNotNull())
            {
                textarea.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes));
            }
            textarea.AddCssClass("form-control");
            string value = null;
            if (modelState.ContainsKey(name))
            {
                value = Convert.ToString(modelState[name].Value, CultureInfo.InvariantCulture);
            }
            if (!modelMetadata.IsComplexType)
            {
                value = Convert.ToString(modelMetadata.Model, CultureInfo.InvariantCulture);
            }
            if (value.IsNotEmpty())
            {
                textarea.InnerHtml = HttpUtility.HtmlEncode(value);
            }
            return MvcHtmlString.Create(textarea.ToString());
        }

        #endregion
    }
}