// <copyright file="IClassHtmlAttribute.cs" company="Appva AB">
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
    public interface IClassHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Is a space-separated list of the classes of the element. Classes allows CSS and 
        /// JavaScript to select and access specific elements via the class selectors or 
        /// functions like the method Document.getElementsByClassName().
        /// </summary>
        /// <param name="classes">The classes of the element.</param>
        /// <returns>The {T}.</returns>
        [Code("class", IsMany = true, ManySeparator = " ")]
        T Class(params string[] classes);
    }
}