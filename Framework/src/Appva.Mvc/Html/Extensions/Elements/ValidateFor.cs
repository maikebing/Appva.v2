// <copyright file="ValidateFor.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ValidateFor<TRoot, TModel, TProperty> : Element<TRoot>
        where TRoot : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The validate expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        internal ValidateFor(TRoot root, Expression<Func<TModel, TProperty>> expression)
            : base(root)
        {
            this.expression = expression;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public override MvcHtmlString Build()
        {
            return this.Root.HtmlHelper.ValidationMessageFor(this.expression);
        }

        #endregion
    }
}