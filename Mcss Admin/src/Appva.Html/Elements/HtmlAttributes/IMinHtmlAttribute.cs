// <copyright file="IMinHtmlAttribute.cs" company="Appva AB">
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
    public interface IMinHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The minimum (numeric or date-time) value for this item, which must not be 
        /// greater than its maximum (max attribute) value.
        /// </summary>
        /// <param name="value">The minimum value.</param>
        /// <returns>The {T}.</returns>
        [Code("min")]
        T Min(object value);
    }
}