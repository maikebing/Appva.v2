// <copyright file="CheckboxFor.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// Creates an input checkbox for a model and property.
    /// </summary>
    /// <typeparam name="TRoot">The root type</typeparam>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class CheckboxFor<TRoot, TModel, TProperty> : Element<TRoot>
        where TRoot : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The checkbox label text.
        /// </summary>
        private readonly string labelText;

        /// <summary>
        /// The html attributes.
        /// </summary>
        private readonly object htmlAttributes;

        /// <summary>
        /// The checkbox expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckboxFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="labelText">The checkbox label text</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal CheckboxFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string labelText, object htmlAttributes = null)
            : base(root)
        {
            this.expression = expression;
            this.labelText = labelText;
            this.htmlAttributes = htmlAttributes;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public override MvcHtmlString Build()
        {
            return this.Root.HtmlHelper.CheckBoxWithLabelFor(this.labelText, this.expression as Expression<Func<TModel, bool>>, this.htmlAttributes);
        }

        #endregion
    }
}