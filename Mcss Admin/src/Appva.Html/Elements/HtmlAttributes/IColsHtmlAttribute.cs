// <copyright file="IColsHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// The visible width of the text control.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IColsHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The visible width of the text control, in average character widths. If it is 
        /// specified, it must be a positive integer. If it is not specified, the default 
        /// value is 20.
        /// </summary>
        /// <param name="value">The width of the text control</param>
        /// <returns>The {T}.</returns>
        [Code("cols")]
        T Columns(int value);
    }
}