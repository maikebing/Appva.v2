// <copyright file="IDropzoneHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.ComponentModel;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IDropzoneHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Is an enumerated attribute indicating what types of content can be dropped on an 
        /// element, using the Drag and Drop API.
        /// </summary>
        /// <param name="type">
        /// <c>copy</c>, which indicates that dropping will create a copy of the element 
        /// that was dragged.
        /// <c>move</c>, which indicates that the element that was dragged will be moved to 
        /// this new location.
        /// <c>link</c>, will create a link to the dragged data.
        /// </param>
        /// <returns>The {T}.</returns>
        [Category("Experimental")]
        [Code("dropzone")]
        T Dropzone(DropzoneType type);
    }
}