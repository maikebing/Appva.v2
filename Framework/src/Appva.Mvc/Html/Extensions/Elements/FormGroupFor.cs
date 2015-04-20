// <copyright file="FormGroupFor.cs" company="Appva AB">
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
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public interface IFormGroupFor<TModel, TProperty>
    {
        /// <summary>
        /// Creates an HTML label element.
        /// </summary>
        /// <param name="labelText">The label text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> Label(string labelText = null, object htmlAttributes = null);

        /// <summary>
        /// Creates an HTML label element with an asterisk (*) suffix.
        /// </summary>
        /// <param name="labelText">The label text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> AsteriskLabel(string labelText = null, object htmlAttributes = null);

        /// <summary>
        /// Creates an HTML text password element.
        /// </summary>
        /// <param name="placeholder">Optional placeholder</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> Password(string placeholder = null, object htmlAttributes = null);

        /// <summary>
        /// Creates an HTML text input element.
        /// </summary>
        /// <param name="placeholder">Optional placeholder</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> TextBox(string placeholder = null, object htmlAttributes = null);

        /// <summary>
        /// Creates an HTML textarea element.
        /// </summary>
        /// <param name="placeholder">Optional placeholder</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> TextArea(string placeholder = null, object htmlAttributes = null);

        /// <summary>
        /// Creates an HTML checkbox input element.
        /// </summary>
        /// <param name="labelText">The label text</param>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> Checkbox(string labelText, object htmlAttributes = null);

        /// <summary>
        /// Creates a list of HTML checkbox input elements.
        /// </summary>
        /// <param name="htmlAttributes">Optional HTML attributes</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> CheckboxList(object htmlAttributes = null);

        /// <summary>
        /// Creates validation errors for an element, nothing if valid.  
        /// </summary>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> Validate();

        /// <summary>
        /// Creates a span element help text.
        /// </summary>
        /// <param name="helpText">The help text to show</param>
        /// <returns><see cref="IFormGroupFor{TModel, TProperty}"/></returns>
        IFormGroupFor<TModel, TProperty> Help(string helpText);

        /// <summary>
        /// Builds the Html.
        /// </summary>
        /// <returns>An <see cref="MvcHtmlString"/></returns>
        MvcHtmlString Build();
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="TModel">The model type</typeparam>
    /// <typeparam name="TProperty">The property type</typeparam>
    public class FormGroupFor<TModel, TProperty> : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        /// <summary>
        /// The outer element group css class.
        /// </summary>
        private readonly string group;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormGroupFor{TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        /// <param name="expression">The property expression</param>
        /// <param name="group">The outer element css class; defaults to 'form-group'</param>
        internal FormGroupFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string group = "form-group")
            : base(htmlHelper)
        {
            this.expression = expression;
            this.group = group;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> Label(string labelText = null, object htmlAttributes = null)
        {
            this.Add(new LabelFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression, labelText, htmlAttributes));
            return this;
        }

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> AsteriskLabel(string labelText = null, object htmlAttributes = null)
        {
            this.Add(new LabelAndAsteriskFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression, labelText, htmlAttributes));
            return this;
        }

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> Password(string placeholder = null, object htmlAttributes = null)
        {
            this.Add(new PasswordFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression, placeholder, htmlAttributes));
            return this;
        }

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> TextBox(string placeholder = null, object htmlAttributes = null)
        {
            this.Add(new TextBoxFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression, placeholder, htmlAttributes));
            return this;
        }
        
        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> TextArea(string placeholder = null, object htmlAttributes = null)
        {
            this.Add(new TextAreaFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression, placeholder, htmlAttributes));
            return this;
        }

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> Checkbox(string labelText, object htmlAttributes = null)
        {
            this.Add(new CheckboxFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression, labelText, htmlAttributes));
            return this;
        }

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> CheckboxList(object htmlAttributes = null)
        {
            this.Add(new CheckBoxListFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression, htmlAttributes));
            return this;
        }

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> Validate()
        {
            this.Add(new ValidateFor<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, this.expression));
            return this;
        }

        /// <inheritdoc />
        public IFormGroupFor<TModel, TProperty> Help(string helpText)
        {
            this.Add(new Help<FormGroupFor<TModel, TProperty>, TModel, TProperty>(this, helpText));
            return this;
        }

        /// <inheritdoc />
        public override MvcHtmlString Build()
        {
            return MvcHtmlString.Create("<div class=\"" + this.group + "\">" + this.ToHtml() + "</div>");
        }

        #endregion
    }
}
