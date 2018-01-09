// <copyright file="ITranslateHtmlAttribute.cs" company="Appva AB">
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
    public interface ITranslateHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Is an enumerated attribute that is used to specify whether an element's 
        /// attribute values and the values of its Text node children are to be translated 
        /// when the page is localized, or whether to leave them unchanged.
        /// </summary>
        /// <param name="translate">
        /// <c>true</c>, which indicates that the element will be translated.
        /// <c>false</c>, which indicates that the element will not be translated.
        /// </param>
        /// <returns>The {T}.</returns>
        [Category("Experimental")]
        [Code("translate", IsBool = true, Format = BoolFormat.YesNo)]
        T Translate(bool translate);
    }
}