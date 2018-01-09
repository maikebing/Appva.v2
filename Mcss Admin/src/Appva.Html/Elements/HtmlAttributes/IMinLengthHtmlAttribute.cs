// <copyright file="IMinLengthHtmlAttribute.cs" company="Appva AB">
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
    public interface IMinLengthHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the value of the type attribute is text, email, search, password, tel, or 
        /// url, this attribute specifies the minimum number of characters (in Unicode code 
        /// points) that the user can enter. For other control types, it is ignored.
        /// </summary>
        /// <param name="value">The minimum number of characters the user can enter.</param>
        /// <returns>The {T}.</returns>
        [Code("minlength")]
        T MinLength(int value);
    }
}