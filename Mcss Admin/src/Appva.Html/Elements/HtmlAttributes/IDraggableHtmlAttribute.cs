// <copyright file="IDraggableHtmlAttribute.cs" company="Appva AB">
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
    public interface IDraggableHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Is an enumerated attribute indicating whether the element can be dragged, using 
        /// the Drag and Drop API.
        /// </summary>
        /// <param name="isDraggable">
        /// <c>true</c>, which indicates that the element may be dragged; or
        /// <c>false</c>, which indicates that the element may not be dragged.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("draggable", IsBool = true, Format = BoolFormat.TrueFalse)]
        T Draggable(bool isDraggable);
    }
}