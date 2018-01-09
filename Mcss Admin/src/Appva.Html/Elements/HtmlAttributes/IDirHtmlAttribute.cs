// <copyright file="IDirHtmlAttribute.cs" company="Appva AB">
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
    public interface IDirHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The text direction.
        /// </summary>
        /// <param name="direction">
        /// <c>ltr</c>, which means left to right and is to be used for languages that are written 
        /// from the left to the right (like English);
        /// <c>rtl</c>, which means right to left and is to be used for languages that are written 
        /// from the right to the left (like Arabic);
        /// <c>auto</c>, which let the user agent decides. It uses a basic algorithm as it parses 
        /// the characters inside the element until it finds a character with a strong 
        /// directionality, then apply that directionality to the whole element.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("dir")]
        T Direction(TextDirection direction);
    }
}