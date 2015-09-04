// <copyright file="RadioButtonFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mvc.Rendering.Html
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Creates an input radiobutton for a model and property
    /// </summary>    
    /// <typeparam name="TRoot">The root type</typeparam>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class RadioButtonFor<TRoot, TModel, TProperty> : Element<TRoot>
        where TRoot : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The radiobutton label text.
        /// </summary>
        private readonly string labelText;

        /// <summary>
        /// The radiobutton value.
        /// </summary>
        private readonly TProperty value;

        /// <summary>
        /// The html attributes.
        /// </summary>
        private readonly object htmlAttributes;

        /// <summary>
        /// The radiobutton expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonFor"/> class.
        /// </summary>
        internal RadioButtonFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string labelText, TProperty value, object htmlAttributes = null)
            : base(root)
        {
            this.expression = expression;
            this.value = value;
            this.labelText = labelText;
            this.htmlAttributes = htmlAttributes;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public override MvcHtmlString Build()
        {
            return this.Root.HtmlHelper.RadioButtonWithLabelFor(this.labelText, this.value, this.expression as Expression<Func<TModel, TProperty>>, this.htmlAttributes);
        }

        #endregion
    }
}