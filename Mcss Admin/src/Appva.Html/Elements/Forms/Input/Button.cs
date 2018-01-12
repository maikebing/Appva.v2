// <copyright file="Button.cs" company="Appva AB">
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
    /// A push button with no default behavior.
    /// </summary>
    public interface IButton : IHtmlElement<IButton>, IInputElement<IButton>, IListHtmlAttribute<IButton>
    {
    }
 
    /// <summary>
    /// An <see cref="IButton"/> implementation.
    /// </summary>
    internal sealed class Button : AbstractInputElement<IButton>, IButton
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Button(HtmlHelper htmlHelper)
            : base(InputType.Button, htmlHelper)
        {
        }

        #endregion
    }
}