// <copyright file="IAcceptCharsetHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// A  list of character encodings that the server accepts.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IAcceptCharsetHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// A  list of character encodings that the server accepts. The browser uses them in 
        /// the order in which they are listed.
        /// </summary>
        /// <param name="charsets">
        /// A space- or comma-delimited list of character encodings.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("accept-charset", IsMany = true, ManySeparator = ";")]
        T Charset(params FormCharset[] charsets);
    }
}