// <copyright file="INoValidateHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// This Boolean attribute indicates that the form is not to be validated when 
    /// submitted.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface INoValidateHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// This Boolean attribute indicates that the form is not to be validated when 
        /// submitted.
        /// </summary>
        /// <returns>The {T}.</returns>
        /// <remarks>
        /// <note type="note">
        /// If this attribute is not specified (and therefore the form is 
        /// validated), this default setting can be overridden by a formnovalidate attribute 
        /// on a button or input element belonging to the form.
        /// </note>
        /// </remarks>
        [Code("novalidate")]
        T EnableValidation();

        /// <summary>
        /// This Boolean attribute indicates that the form is not to be validated when 
        /// submitted.
        /// </summary>
        /// <returns>The {T}.</returns>
        /// <remarks>
        /// <note type="note">
        /// If this attribute is not specified (and therefore the form is 
        /// validated), this default setting can be overridden by a formnovalidate attribute 
        /// on a button or input element belonging to the form.
        /// </note>
        /// </remarks>
        [Code("novalidate")]
        T DisableValidation();
    }
}