// <copyright file="IAltHtmlAttribute.cs" company="Appva AB">
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
    public interface IAltHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The image alternative text.
        /// </summary>
        /// <param name="value">The alternative text.</param>
        /// <returns>The {T}.</returns>
        [Code("alt")]
        T AlternativeText(string value);
    }
}