// <copyright file="IFormNoValidateHtmlAttribute.cs" company="Appva AB">
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
    public interface IFormNoValidateHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the input element is a submit button or image, this Boolean attribute 
        /// specifies that the form is not to be validated when it is submitted. If this 
        /// attribute is specified, it overrides the novalidate attribute of the element's 
        /// form owner.
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("formnovalidate")]
        T FormDisableValidation();
    }
}