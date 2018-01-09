// <copyright file="IWrapHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Indicates how the control wraps text.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IWrapHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Indicates how the control wraps text.
        /// </summary>
        /// <param name="value">How the control wraps text.</param>
        /// <returns>The {T}.</returns>
        [Code("wrap")]
        T Wrap(TextWrapType value);
    }
}