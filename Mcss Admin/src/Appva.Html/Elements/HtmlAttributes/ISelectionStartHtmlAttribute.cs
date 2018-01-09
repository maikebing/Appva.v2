// <copyright file="ISelectionStartHtmlAttribute.cs" company="Appva AB">
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
    public interface ISelectionStartHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The offset into the element's text content of the first selected character. If 
        /// there's no selection, this value indicates the offset to the character following 
        /// the current text input cursor position (that is, the position the next character 
        /// typed would occupy).
        /// </summary>
        /// <param name="start">
        /// The start index of the selected text.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("selectionStart")]
        T SelectionStart(int start);
    }
}