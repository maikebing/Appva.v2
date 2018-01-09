// <copyright file="IAccessKeyHtmlAttribute.cs" company="Appva AB">
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
    public interface IAccessKeyHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Provides a hint for generating a keyboard shortcut for the current element. 
        /// This attribute consists of a space-separated list of characters. The browser 
        /// should use the first one that exists on the computer keyboard layout.
        /// </summary>
        /// <param name="hint">The keyboard shortcut hint.</param>
        /// <returns>The {T}.</returns>
        [Code("accesskey")]
        T AccessKey(string hint);
    }
}