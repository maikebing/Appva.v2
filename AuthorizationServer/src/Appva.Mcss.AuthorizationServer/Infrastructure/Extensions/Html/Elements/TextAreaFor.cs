// <copyright file="TextAreaFor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure.Extensions.Html.Elements
{
    #region Imports.

    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class TextAreaFor<TRoot, TModel, TProperty> : Element<TRoot>
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
        /// The text area expression.
        /// </summary>
        private readonly Expression<Func<TModel, TProperty>> expression;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAreaFor{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="expression">The expression</param>
        /// <param name="placeholder">Optional placeholder</param>
        /// <param name="htmlAttributes">Optional html attributes</param>
        internal TextAreaFor(TRoot root, Expression<Func<TModel, TProperty>> expression, string placeholder = null, object htmlAttributes = null)
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
            var modelState = this.Root.HtmlHelper.ViewData.ModelState;
            var id = this.Root.HtmlHelper.IdFor(expression).ToString();
            var name = this.Root.HtmlHelper.IdFor(expression).ToString();
            var textarea = new TagBuilder("textarea");
            textarea.Attributes.Add("id", id);
            textarea.Attributes.Add("name", name);
            textarea.Attributes.Add("rows", "3");
            textarea.Attributes.Add("size", "255");
            textarea.Attributes.Add("maxlength", "255");
            if (this.placeholder.IsNotEmpty())
            {
                textarea.Attributes.Add("placeholder", this.placeholder);
            }
            if (htmlAttributes.IsNotNull())
            {
                textarea.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            textarea.AddCssClass("form-control");
            if (modelState.ContainsKey(name))
            {
                var value = Convert.ToString(modelState[name].Value, CultureInfo.InvariantCulture);
                if (value.IsNotEmpty())
                {
                    textarea.InnerHtml = HttpUtility.HtmlEncode(value);
                }
            }
            return MvcHtmlString.Create(textarea.ToString());
        }

        #endregion
    }
}