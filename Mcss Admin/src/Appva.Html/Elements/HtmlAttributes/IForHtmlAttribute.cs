// <copyright file="IForHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// The ID of a labelable form-related element in the same document as the label 
    /// element. The first such element in the document with an ID matching the value of 
    /// the for attribute is the labeled control for this label element.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IForHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The ID of a labelable form-related element in the same document as the label 
        /// element. The first such element in the document with an ID matching the value of 
        /// the for attribute is the labeled control for this label element.
        /// </summary>
        /// <param name="id">The id of the related element.</param>
        /// <returns>The {T}.</returns>
        [Code("for")]
        T For(string id);
    }
}