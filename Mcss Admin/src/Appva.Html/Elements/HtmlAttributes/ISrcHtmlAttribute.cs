// <copyright file="ISrcHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface ISrcHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the value of the type attribute is image, this attribute specifies a URI for 
        /// the location of an image to display on the graphical submit button, otherwise it 
        /// is ignored.
        /// </summary>
        /// <param name="location">The URI location.</param>
        /// <returns>The {T}.</returns>
        [Code("src")]
        T Source(Uri location);
    }
}