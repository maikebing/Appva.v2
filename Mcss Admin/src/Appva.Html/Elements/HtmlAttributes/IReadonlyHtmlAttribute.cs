// <copyright file="IReadonlyHtmlAttribute.cs" company="Appva AB">
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
    public interface IReadonlyHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// This attribute indicates that the user cannot modify the value of the control. 
        /// The value of the attribute is irrelevant. If you need read-write access to the 
        /// input value, do not add the "readonly" attribute. It is ignored if the value of 
        /// the type attribute is hidden, range, color, checkbox, radio, file, or a button 
        /// type (such as button or submit).
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("readonly", IsNoValue = true)]
        T Readonly();
    }
}