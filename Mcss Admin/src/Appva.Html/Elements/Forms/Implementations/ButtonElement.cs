﻿// <copyright file="ButtonElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ButtonElement : Block<IButtonElement>, IButtonElement
    {
        #region Variables.

        /// <summary>
        /// The tag.
        /// </summary>
        private static readonly Tag Tag = Tag.New("button");

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonElement"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public ButtonElement(HtmlHelper htmlHelper, object text = null)
            : base(htmlHelper, Tag)
        {
            if (text == null)
            {
                return;
            }
            this.Builder.SetInnerText(text.ToString());
        }

        #endregion

        #region IButtonElement Members.

        /// <inheritdoc />
        public IButtonElement Type(ButtonType value)
        {
            this.AddAttribute<ButtonElement>(x => x.Type(value), null, value);
            return this;
        }

        #endregion
    }
}