// <copyright file="IAriaHtmlElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IAriaHtmlElement<T> : IHtmlAttribute
    {
        /// <summary>
        /// The multiple aria-* attributes, used for improving accessibility.
        /// </summary>
        /// <param name="key">
        /// The suffix of the aria attribute, e.g. disabled, which will be translated to 
        /// 'aria-disabled'.
        /// </param>
        /// <param name="value">
        /// The aria attribute value.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("aria", IsPrefixed = true, PrefixSeparator = "-")]
        T Aria(string key, string value);
    }
}