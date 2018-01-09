// <copyright file="IOnEventHtmlAttribtue.cs" company="Appva AB">
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
    public interface IOnEventHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The event handler attributes.
        /// </summary>
        /// <param name="key">
        /// The event.
        /// </param>
        /// <param name="value">
        /// The event handler attribute value.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("on", IsPrefixed = true)]
        T On(OnEventHandler key, string value);
    }
}