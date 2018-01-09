// <copyright file="IRoleHtmlAttribute.cs" company="Appva AB">
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
    public interface IRoleHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// ARIA allows developers to declare a semantic role for an element that otherwise 
        /// offers incorrect or no semantics.
        /// </summary>
        /// <param name="value">
        /// Defines what the general type of object is (such as an article, alert, or 
        /// slider).
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("role")]
        T Role(string value);
    }
}