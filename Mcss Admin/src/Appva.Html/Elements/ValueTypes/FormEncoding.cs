// <copyright file="FormEncoding.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Represents a value for <see cref="IFormHtmlAttributes{T}.Encoding"/>.
    /// </summary>
    public enum FormEncoding
    {
        /// <summary>
        /// The default value if the attribute is not specified.
        /// </summary>
        [Code("application/x-www-form-urlencoded")]
        FormUrlEncoded,

        /// <summary>
        /// The value used for an <input> element with the type attribute set to "file".
        /// </summary>
        [Code("multipart/form-data")]
        FormData,

        /// <summary>
        /// HTML5 : text/plain.
        /// </summary>
        [Code("text/plain")]
        PlainText
    }
}