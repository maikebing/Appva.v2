// <copyright file="IMaxLengthHtmlAttribute.cs" company="Appva AB">
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
    public interface IMaxLengthHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the value of the type attribute is text, email, search, password, tel, or 
        /// url, this attribute specifies the maximum number of characters (in UTF-16 code 
        /// units) that the user can enter. For other control types, it is ignored. It can 
        /// exceed the value of the size attribute. If it is not specified, the user can 
        /// enter an unlimited number of characters. Specifying a negative number results in 
        /// the default behavior (i.e. the user can enter an unlimited number of 
        /// characters). The constraint is evaluated only when the value of the attribute 
        /// has been changed.
        /// </summary>
        /// <param name="value">The maximum number of characters the user can enter.</param>
        /// <returns>The {T}.</returns>
        [Code("maxlength")]
        T MaxLength(int value);
    }
}