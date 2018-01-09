// <copyright file="FormCharset.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Represents a value for <see cref="IFormHtmlAttributes{T}.Charset"/>.
    /// </summary>
    public enum FormCharset
    {
        /// <summary>
        /// The reserved string "UNKNOWN", indicates the same encoding as that of the 
        /// document containing the form element.
        /// </summary>
        [Code("UNKNOWN")]
        Unknown,

        /// <summary>
        /// Represents UTF-8.
        /// </summary>
        [Code("utf-8")]
        Utf8
    }
}