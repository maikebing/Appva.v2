// <copyright file="ISpellcheckHtmlAttribute.cs" company="Appva AB">
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
    /// Is an enumerated attribute defines whether the element may be checked for 
    /// spelling errors.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface ISpellcheckHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Setting the value of this attribute to true indicates that the element needs to 
        /// have its spelling and grammar checked. The value default indicates that the 
        /// element is to act according to a default behavior, possibly based on the parent 
        /// element's own spellcheck value. The value false indicates that the element 
        /// should not be checked.
        /// </summary>
        /// <param name="enabled">
        /// <c>true</c>, which indicates that the element should be, if possible, checked 
        /// for spelling errors;
        /// <c>false</c>, which indicates that the element should not be checked for 
        /// spelling errors.
        /// </param>
        /// <returns>The {T}.</returns>
        [Category("Experimental")]
        [Code("spellcheck", IsBool = true, Format = BoolFormat.TrueFalse)]
        T Spellcheck(bool enabled);
    }
}