// <copyright file="IRowsHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// The number of visible text lines for the control.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IRowsHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The number of visible text lines for the control.
        /// </summary>
        /// <param name="value">The number of visible text lines for the control.</param>
        /// <returns>The {T}.</returns>
        [Code("rows")]
        T Rows(int value);
    }
}