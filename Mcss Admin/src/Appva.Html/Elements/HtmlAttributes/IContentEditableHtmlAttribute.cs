// <copyright file="IContentEditableHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Represents the 'contenteditable' attribute, which is an enumerated attribute 
    /// indicating if the element should be editable by the user. If so, the browser 
    /// modifies its widget to allow editing. 
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IContentEditableHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Represents the 'contenteditable' attribute, which is an enumerated attribute 
        /// indicating if the element should be editable by the user. If so, the browser 
        /// modifies its widget to allow editing. 
        /// </summary>
        /// <param name="isContentEditable">
        /// <c>true</c> which indicates that the element must be editable;
        /// <c>false</c>, which indicates that the element must not be editable.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("contenteditable", IsBool = true, Format = BoolFormat.TrueFalse)]
        T IsEditable(bool isContentEditable);
    }
}