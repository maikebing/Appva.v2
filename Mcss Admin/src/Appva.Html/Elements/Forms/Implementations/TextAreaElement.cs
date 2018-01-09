// <copyright file="TextAreaElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using System.Web.WebPages;

    #endregion

    /// <summary>
    /// Implementation of an <see cref="ITextArea"/>.
    /// </summary>
    internal sealed class TextAreaElement : Input<ITextArea>, ITextArea
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public TextAreaElement(HtmlHelper htmlHelper)
            : base(InputType.Color, htmlHelper)
        {
        }

        #endregion

        #region ITextArea Members.

        /// <inheritdoc />
        public ITextArea DisableResize()
        {
            this.Builder.AddStyle("resize: none;");
            return this;
        }

        /// <inheritdoc />
        public MvcHtmlString Content(Func<ITextArea, HelperResult> content)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}