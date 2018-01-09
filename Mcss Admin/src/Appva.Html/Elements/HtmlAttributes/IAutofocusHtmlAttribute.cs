// <copyright file="IAutofocusHtmlAttribute.cs" company="Appva AB">
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
    public interface IAutofocusHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// This Boolean attribute lets you specify that a form control should have input 
        /// focus when the page loads, unless the user overrides it (e.g. by typing in a 
        /// different control). Only one form element in a document can have the autofocus 
        /// attribute, which is a Boolean. It cannot be applied if the type attribute is set 
        /// to hidden (that is, you cannot automatically set focus to a hidden control).
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("autofocus")]
        T Autofocus();
    }
}