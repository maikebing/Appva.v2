// <copyright file="LabelAndAsteriskFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Html.Extensions.Elements
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Creates an HTML label element with an asterisk (*) suffix.
    /// </summary>
    /// <typeparam name="TRoot">The root type</typeparam>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class LabelAndAsteriskFor<TRoot, TModel, TProperty> : Element<TRoot>
        where TRoot : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The html attributes.
        /// </summary>
        private readonly object htmlAttributes;

        /// <summary>
        /// The label text.
        /// </summary>
        private readonly string labelText;

        /// <summary>
        /// The label expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelAndAsteriskFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="labelText">Optional label text</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal LabelAndAsteriskFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string labelText = null, object htmlAttributes = null)
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
            return this.Root.HtmlHelper.LabelWithAsteriskFor(this.expression, this.labelText, this.htmlAttributes);
        }

        #endregion
    }
}