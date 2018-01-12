﻿// <copyright file="TextAreaElement.cs" company="Appva AB">
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
    internal sealed class TextAreaElement : Block<ITextArea>, ITextArea
    {
        #region Variables.

        /// <summary>
        /// The tag.
        /// </summary>
        private static readonly Tag Tag = Tag.New("textarea");

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAreaElement"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public TextAreaElement(HtmlHelper htmlHelper, object text = null)
            : base(htmlHelper, Tag)
        {
            if (text == null)
            {
                return;
            }
            this.Builder.SetInnerText(text.ToString());
        }

        #endregion

        #region ITextArea Members.

        /// <inheritdoc />
        public ITextArea Label(ILabel label)
        {
            this.AddElement(Position.Before, label);
            return this;
        }

        /// <inheritdoc />
        public ITextArea DisableResize()
        {
            this.Builder.AddStyle("resize: none;");
            return this;
        }

        #endregion
    }
}