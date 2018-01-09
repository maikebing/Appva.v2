// <copyright file="Color.cs" company="Appva AB">
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
    /// A control for specifying a color. A color picker's UI has no required features 
    /// other than accepting simple colors as text.
    /// </summary>
    public interface IColor : IHtmlElement<IColor>, IInputElement<IColor>, IListHtmlAttribute<IColor>, IRequiredHtmlAttribute<IColor>
    {
    }

    /// <summary>
    /// An <see cref="IColor"/> implementation.
    /// </summary>
    internal sealed class Color : Input<IColor>, IColor
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Color(HtmlHelper htmlHelper)
            : base(InputType.Color, htmlHelper)
        {
        }

        #endregion
    }
}