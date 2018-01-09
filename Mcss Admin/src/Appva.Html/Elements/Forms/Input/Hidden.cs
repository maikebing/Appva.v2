// <copyright file="Hidden.cs" company="Appva AB">
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
    /// A control that is not displayed but whose value is submitted to the server.
    /// </summary>
    public interface IHidden : IHtmlElement<IHidden>, IInputElement<IHidden> 
    {
    }

    /// <summary>
    /// An <see cref="IHidden"/> implementation.
    /// </summary>
    internal sealed class Hidden : Input<IHidden>, IHidden
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Hidden"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Hidden(HtmlHelper htmlHelper)
            : base(InputType.Hidden, htmlHelper)
        {
        }

        #endregion
    }
}