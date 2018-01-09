// <copyright file="IDataHtmlAttribute.cs" company="Appva AB">
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
    public interface IDataHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Forms a class of attributes, called custom data attributes, that allow 
        /// proprietary information to be exchanged between the HTML and its DOM 
        /// representation that may be used by scripts. All such custom data are available 
        /// via the HTMLElement interface of the element the attribute is set on. 
        /// The HTMLElement.dataset property gives access to them.
        /// </summary>
        /// <param name="key">
        /// The suffix key of the 'data' attribute (data-*).
        /// </param>
        /// <param name="value">
        /// The data value.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("data", IsPrefixed = true, PrefixSeparator = "-")]
        T Data(string key, string value);
    }
}