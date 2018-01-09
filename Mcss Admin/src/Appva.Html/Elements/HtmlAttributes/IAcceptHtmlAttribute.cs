// <copyright file="IAcceptHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Indicate the types of files that the server accepts.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IAcceptHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the value of the type attribute is file, then this attribute will indicate 
        /// the types of files that the server accepts, otherwise it will be ignored. 
        /// The  value must be a comma-separated list of unique content type specifiers:
        /// <list type="bullet">
        ///     <item>
        ///         <description>
        ///         A file extension starting with the STOP character (U+002E). (e.g. .jpg, 
        ///         .png, .doc).
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         A valid MIME type with no extensions.
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         audio/* representing sound files (HTML5). 
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         video/* representing video files (HTML5). 
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         image/* representing image files. (HTML5). 
        ///         </description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="value">The accept value.</param>
        /// <returns>The {T}.</returns>
        [Code("accept")]
        T Accept(string value);
    }
}