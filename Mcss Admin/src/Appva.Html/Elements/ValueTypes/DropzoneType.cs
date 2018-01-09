// <copyright file="DropzoneType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Drop zone values for <see cref="IHtmlElement{T}.Dropzone"/>.
    /// </summary>
    public enum DropzoneType
    {
        /// <summary>
        /// <c>copy</c>, which indicates that dropping will create a copy of the element 
        /// that was dragged.
        /// </summary>
        [Code("copy")]
        Copy,

        /// <summary>
        /// <c>move</c>, which indicates that the element that was dragged will be moved to 
        /// this new location.
        /// </summary>
        [Code("move")]
        Move,

        /// <summary>
        /// c>link</c>, will create a link to the dragged data.
        /// </summary>
        [Code("link")]
        Link
    }
}