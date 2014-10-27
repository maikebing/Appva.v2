// <copyright file="LabelFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Html.Extensions.Elements
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Appva.Mvc.Html.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class LabelFor<TRoot, TModel, TProperty> : Element<TRoot>
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
        /// Initializes a new instance of the <see cref="LabelFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="labelText">Optional label text</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal LabelFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string labelText = null, object htmlAttributes = null)
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
            var modelMetadata = ModelMetadata.FromLambdaExpression(this.expression, this.Root.HtmlHelper.ViewData);
            if (modelMetadata.IsRequired)
            {
                return this.Root.HtmlHelper.LabelWithAsteriskFor(this.expression, this.labelText, this.htmlAttributes);
            }
            else
            {
                return this.Root.HtmlHelper.LabelFor(this.expression, this.labelText, this.htmlAttributes);
            }
        }

        #endregion
    }
}