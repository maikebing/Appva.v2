// <copyright file="CheckBoxListFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Html.Extensions.Elements
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Appva.Core.Extensions;
    using Models;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class CheckBoxListFor<TRoot, TModel, TProperty> : Element<TRoot>
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
        /// The checkbox list expression.
        /// </summary>
        private readonly Expression<Func<TModel, IList<Tickable>>> expression;

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
            this.expression = expression as Expression<Func<TModel, IList<Tickable>>>;
            this.htmlAttributes = htmlAttributes;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public override MvcHtmlString Build()
        {
            var builder = new StringBuilder();
            var modelMetadata = ModelMetadata.FromLambdaExpression(this.expression, this.Root.HtmlHelper.ViewData);
            var list = modelMetadata.Model as IList<Tickable>;
            if (list.IsNotEmpty())
            {
                builder.Append("<ul class=\"checkbox-grid\">");
                builder.Append(this.Root.HtmlHelper.EditorFor(this.expression).ToString());
                builder.Append("</ul>");
                return MvcHtmlString.Create(builder.ToString());
            }
            return MvcHtmlString.Empty;
        }

        #endregion
    }
}