// <copyright file="IMaxHtmlAttribute.cs" company="Appva AB">
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
    public interface IMaxHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The maximum (numeric or date-time) value for this item, which must not be less 
        /// than its minimum (min attribute) value.
        /// </summary>
        /// <param name="value">The maximum value.</param>
        /// <returns>The {T}.</returns>
        [Code("max")]
        T Max(object value);
    }
}