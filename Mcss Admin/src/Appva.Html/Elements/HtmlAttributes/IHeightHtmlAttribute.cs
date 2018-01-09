// <copyright file="IHeightHtmlAttribute.cs" company="Appva AB">
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
    public interface IHeightHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the value of the type attribute is image, this attribute defines the height 
        /// of the image displayed for the button.
        /// </summary>
        /// <param name="value">The height.</param>
        /// <returns>The {T}.</returns>
        [Code("height")]
        T Height(string value);
    }
}