// <copyright file="IListHtmlAttribute.cs" company="Appva AB">
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
    public interface IListHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Identifies a list of pre-defined options to suggest to the user. The value must 
        /// be the id of a datalist element in the same document. The browser displays only 
        /// options that are valid values for this input element. This attribute is ignored 
        /// when the type attribute's value is hidden, checkbox, radio, file, or a button 
        /// type.
        /// </summary>
        /// <param name="id">The id of a datalist element.</param>
        /// <returns>The {T}.</returns>
        [Code("list")]
        T List(string id);
    }
}