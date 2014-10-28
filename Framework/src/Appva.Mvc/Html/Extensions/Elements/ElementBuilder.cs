// <copyright file="ElementBuilder.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Html.Extensions.Elements
{
    #region Imports.

    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
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

        #region Protected Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
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
        /// 
        /// </summary>
        /// <returns></returns>
        protected string ToHtml()
        {
            var builder = new StringBuilder();
            foreach(var element in this.elements)
            {
                builder.Append(element.Build().ToHtmlString());
            }
            return builder.ToString();
        }

        #endregion

        #region Public Abstract Methods.

        /// <inheritdoc />
        public abstract MvcHtmlString Build();

        #endregion
    }
}