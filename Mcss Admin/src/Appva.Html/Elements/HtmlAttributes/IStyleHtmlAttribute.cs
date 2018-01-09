// <copyright file="IStyleHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Contains CSS styling declarations to be applied to the element.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IStyleHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Contains CSS styling declarations to be applied to the element. Note that it is 
        /// recommended for styles to be defined in a separate file or files. This attribute 
        /// and the style element have mainly the purpose of allowing for quick styling, for 
        /// example for testing purposes.
        /// </summary>
        /// <param name="value">
        /// The css styling.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("style")]
        T Style(string value);
    }
}