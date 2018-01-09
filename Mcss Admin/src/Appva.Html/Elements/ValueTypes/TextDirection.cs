// <copyright file="TextDirection.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Text direction values for <see cref="IHtmlElement{T}.Direction"/>.
    /// </summary>
    public enum TextDirection
    {
        /// <summary>
        /// <c>ltr</c>, which means left to right and is to be used for languages that are 
        /// written from the left to the right (like English)
        /// </summary>
        [Code("ltr")]
        LeftToRight,

        /// <summary>
        /// <c>rtl</c>, which means right to left and is to be used for languages that are 
        /// written  from the right to the left (like Arabic);
        /// </summary>
        [Code("rtl")]
        RightToLeft,

        /// <summary>
        /// <c>auto</c>, which let the user agent decides. It uses a basic algorithm as it 
        /// parses the characters inside the element until it finds a character with a 
        /// strong directionality, then apply that directionality to the whole element.
        /// </summary>
        [Code("auto")]
        Auto
    }
}