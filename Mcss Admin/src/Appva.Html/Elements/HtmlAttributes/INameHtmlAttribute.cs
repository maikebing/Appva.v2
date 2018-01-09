// <copyright file="INameHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// The name of the control.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface INameHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The name of the control.
        /// </summary>
        /// <param name="value">The name of the input or form element.</param>
        /// <returns>The {T}.</returns>
        /// <note type="note">
        /// In HTML 4, its use for forms is deprecated (id should be used instead). It must 
        /// be unique  among the forms in a document and not just an empty string in HTML 5.
        /// </note>
        [Code("name")]
        T Name(string value);
    }
}