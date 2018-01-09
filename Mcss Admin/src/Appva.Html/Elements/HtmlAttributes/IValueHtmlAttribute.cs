// <copyright file="IValueHtmlAttribute.cs" company="Appva AB">
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
    public interface IValueHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The initial value of the control. This attribute is optional except when the 
        /// value of the type attribute is radio or checkbox.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The {T}.</returns>
        [Code("value")]
        T Value(string value);
    }
}