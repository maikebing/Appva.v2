// <copyright file="IFormHtmlAttribute.cs" company="Appva AB">
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
    public interface IFormHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The form element that the input element is associated with (its form owner). The 
        /// value of the attribute must be an id of a form element in the same document. If 
        /// this attribute is not specified, this input element must be a descendant of a 
        /// form element. This attribute enables you to place input elements anywhere within 
        /// a document, not just as descendants of their form elements. An input can only be 
        /// associated with one form.
        /// </summary>
        /// <param name="id">The id of a form element in the same document.</param>
        /// <returns>The {T}.</returns>
        [Code("form")]
        T Form(string id);
    }
}