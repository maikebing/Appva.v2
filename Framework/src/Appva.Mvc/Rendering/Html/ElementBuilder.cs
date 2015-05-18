// <copyright file="ElementBuilder.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Rendering.Html
{
    #region Imports.

    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="TModel">The model type</typeparam>
    public abstract class ElementBuilder<TModel>
    {
        #region Variables.

        /// <summary>
        /// The html helper.
        /// </summary>
        private readonly HtmlHelper<TModel> htmlHelper;

        /// <summary>
        /// The list of elements.
        /// </summary>
        private readonly IList<IElement> elements;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper{TModel}"/></param>
        internal ElementBuilder(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.elements = new List<IElement>();
        }

        #endregion

        #region Internal Properties.

        /// <summary>
        /// Returns the <see cref="HtmlHelper{TModel}"/>.
        /// </summary>
        internal HtmlHelper<TModel> HtmlHelper
        {
            get
            {
                return this.htmlHelper;
            }
        }

        #endregion

        #region Public Abstract Methods.

        /// <summary>
        /// Builds a single element as HTML. 
        /// </summary>
        /// <returns>The element as an <see cref="MvcHtmlString"/></returns>
        public abstract MvcHtmlString Build();

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Adds an {T} element to the collection of HTML elements to be processed.
        /// </summary>
        /// <typeparam name="TElement">The TElement type</typeparam>
        /// <param name="element">The element to add</param>
        /// <returns>The added TElement instance</returns>
        protected TElement Add<TElement>(TElement element)
            where TElement : IElement
        {
            if (! this.elements.Contains(element))
            {
                this.elements.Add(element);
            }
            return element;
        }

        /// <summary>
        /// Builds the HTML string.
        /// </summary>
        /// <returns>An HTML string</returns>
        protected string ToHtml()
        {
            var builder = new StringBuilder();
            foreach (var element in this.elements)
            {
                builder.Append(element.Build().ToHtmlString());
            }
            return builder.ToString();
        }

        #endregion
    }
}