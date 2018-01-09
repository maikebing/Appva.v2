// <copyright file="ISizeHtmlAttribute.cs" company="Appva AB">
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
    public interface ISizeHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The initial size of the control. This value is in pixels unless the value of the 
        /// type attribute is text or password, in which case it is an integer number of 
        /// characters. Starting in HTML5, this attribute applies only when the type 
        /// attribute is set to text, search, tel, url, email, or password, otherwise it is 
        /// ignored. In addition, the size must be greater than zero. If you do not specify 
        /// a size, a default value of 20 is used. HTML5 simply states "the user agent 
        /// should ensure that at least that many characters are visible", but different 
        /// characters can have different widths in certain fonts. In some browsers, a 
        /// certain string with x characters will not be entirely visible even if size is 
        /// defined to at least x.
        /// </summary>
        /// <param name="value">
        /// Optional size value; defaults to 20.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("size")]
        T Size(int value = 20);
    }
}