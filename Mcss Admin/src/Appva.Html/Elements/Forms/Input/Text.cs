// <copyright file="Text.cs" company="Appva AB">
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
    using Appva.Html.Infrastructure;

    #endregion

    /// <summary>
    /// A single-line text field. Line-breaks are automatically removed from the input 
    /// value.
    /// </summary>
    public interface IText : IHtmlElement<IText>, IInputElement<IText>, IListHtmlAttribute<IText>,
        IMaxLengthHtmlAttribute<IText>, IMinLengthHtmlAttribute<IText>, IPatternHtmlAttribute<IText>,
        IReadonlyHtmlAttribute<IText>, IRequiredHtmlAttribute<IText>, ISizeHtmlAttribute<IText>
    {
    }

    /// <summary>
    /// An <see cref="IText"/> implementation.
    /// </summary>
    internal sealed class Text : AbstractInputElement<IText>, IText
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Text(HtmlHelper htmlHelper)
            : base(InputType.Text, htmlHelper)
        {
        }

        #endregion
    }
}