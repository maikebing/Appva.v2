// <copyright file="IContextMenuHtmlAttribute.cs" company="Appva AB">
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
    public interface IContextMenuHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Sets the id of html menu to use as the contextual menu for this element.
        /// </summary>
        /// <param name="id">
        /// The id of an html <c>menu</c>.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("contextmenu")]
        T ContextMenu(string id);
    }
}