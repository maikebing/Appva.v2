// <copyright file="FormGroupFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
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
    public interface IFormGroupFor<TModel, TProperty>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        IFormGroupFor<TModel, TProperty> Label(string labelText = null, object htmlAttributes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        IFormGroupFor<TModel, TProperty> AsteriskLabel(string labelText = null, object htmlAttributes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        IFormGroupFor<TModel, TProperty> TextBox(string placeholder = null, object htmlAttributes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        IFormGroupFor<TModel, TProperty> TextArea(string placeholder = null, object htmlAttributes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        IFormGroupFor<TModel, TProperty> Checkbox(string labelText, object htmlAttributes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        IFormGroupFor<TModel, TProperty> CheckboxList(object htmlAttributes = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IFormGroupFor<TModel, TProperty> Validate();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helpText"></param>
        /// <returns></returns>
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
    public class FormGroupFor<TModel, TProperty> : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormGroupFor{TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        internal FormGroupFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            : base(htmlHelper)
        {
            this.expression = expression;
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
            return MvcHtmlString.Create("<div class=\"form-group\">" + this.ToHtml() + "</div>");
        }

        #endregion
    }
}