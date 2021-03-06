﻿// <copyright file="PasswordFor.cs" company="Appva AB">
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
    using Internal;

    #endregion

    /// <summary>
    /// Creates an HTML text input password element.
    /// </summary>
    /// <typeparam name="TRoot">The root type</typeparam>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class PasswordFor<TRoot, TModel, TProperty> : Element<TRoot>
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
        /// Initializes a new instance of the <see cref="PasswordFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="placeholder">Optional placeholder</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal PasswordFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string placeholder = null, object htmlAttributes = null)
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
            var htmlDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes);
            htmlDictionary.AddClass("form-control");
            if (this.placeholder.IsNotEmpty())
            {
                htmlDictionary.Add("placeholder", this.placeholder);
            }
            return this.Root.HtmlHelper.PasswordFor(this.expression, htmlDictionary);
        }

        #endregion
    }
}
