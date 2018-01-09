// <copyright file="IWidthHtmlAttribute.cs" company="Appva AB">
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
    public interface IWidthHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the value of the type attribute is image, this attribute defines the width of 
        /// the image displayed for the button.
        /// </summary>
        /// <param name="value">The width of the element.</param>
        /// <returns>The {T}.</returns>
        [Code("width")]
        T Width(string value);
    }
}