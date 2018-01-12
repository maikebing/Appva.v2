// <copyright file="IRequiredHtmlAttribute.cs" company="Appva AB">
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
    public interface IRequiredHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// This attribute specifies that the user must fill in a value before submitting a 
        /// form. It cannot be used when the type attribute is hidden, image, or a button 
        /// type (submit, reset, or button). The :optional and :required CSS pseudo-classes 
        /// will be applied to the field as appropriate.
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("required", IsNoValue = true)]
        T Required();
    }
}