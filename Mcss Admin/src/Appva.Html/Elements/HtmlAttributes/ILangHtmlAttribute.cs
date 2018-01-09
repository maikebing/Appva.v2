// <copyright file="ILangHtmlAttribute.cs" company="Appva AB">
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
    public interface ILangHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Participates in defining the language of the element, the language that 
        /// non-editable elements are written in or the language that editable elements 
        /// should be written in. The tag contains one single entry value in the format 
        /// defined in the Tags for Identifying Languages (BCP47) IETF document. xml:lang 
        /// has priority over it.
        /// <externalLink>
        ///     <linkText>Tags for Identifying Languages (BCP47)</linkText>
        ///     <linkUri>
        ///         https://www.ietf.org/rfc/bcp/bcp47.txt
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <param name="value">
        /// The language code as defined in Tags for Identifying Languages (BCP47).
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("lang")]
        T Language(string value);
    }
}