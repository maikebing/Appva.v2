// <copyright file="IPatternHtmlAttribute.cs" company="Appva AB">
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
    public interface IPatternHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// A regular expression that the control's value is checked against. The pattern 
        /// must match the entire value, not just some subset. Use the title attribute to 
        /// describe the pattern to help the user. This attribute applies when the value of 
        /// the type attribute is text, search, tel, url, email, or password, otherwise it 
        /// is ignored. The regular expression language is the same as JavaScript RegExp 
        /// algorithm, with the 'u' parameter that makes it treat the pattern as a sequence 
        /// of unicode code points. The pattern is not surrounded by forward slashes.
        /// </summary>
        /// <param name="value">A regular expression.</param>
        /// <returns>The {T}.</returns>
        [Code("pattern")]
        T Pattern(string value);
    }
}