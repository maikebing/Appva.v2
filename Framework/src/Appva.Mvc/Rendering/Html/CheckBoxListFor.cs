// <copyright file="CheckBoxListFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Rendering.Html
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// Creates a list of input checkbox for a List{model} and property.
    /// </summary>
    /// <typeparam name="TRoot">The root type</typeparam>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class CheckBoxListFor<TRoot, TModel, TProperty> : Element<TRoot>
        where TRoot : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The html attributes.
        /// </summary>
        private readonly object htmlAttributes;

        /// <summary>
        /// The checkbox list expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxListFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal CheckBoxListFor(TRoot root, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
            : base(root)
        {
            this.expression = expression;
            this.htmlAttributes = htmlAttributes;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public override MvcHtmlString Build()
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(this.expression, this.Root.HtmlHelper.ViewData);
            var list = modelMetadata.Model;
            if (list == null)
            {
                return MvcHtmlString.Empty;
            }
            var ul = new TagBuilder("ul");
            if (this.htmlAttributes.IsNotNull())
            {
                ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes));
            }
            else
            {
                ul.AddCssClass("checkbox-grid");
            }
            ul.InnerHtml = this.Root.HtmlHelper.EditorFor(this.expression).ToHtmlString();
            return MvcHtmlString.Create(ul.ToString());
        }

        #endregion
    }
}