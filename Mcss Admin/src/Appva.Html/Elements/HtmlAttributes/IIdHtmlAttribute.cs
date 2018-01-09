// <copyright file="IIdHtmlAttribute.cs" company="Appva AB">
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
    public interface IIdHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Defines a unique identifier (ID) which must be unique in the whole document. Its 
        /// purpose is to identify the element when linking (using a fragment identifier), 
        /// scripting, or styling (with CSS).
        /// </summary>
        /// <param name="value">
        /// The html id.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("id")]
        T Id(string value);
    }
}