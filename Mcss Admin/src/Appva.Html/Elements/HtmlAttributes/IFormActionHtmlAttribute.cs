// <copyright file="IFormActionHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IFormActionHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The URI of a program that processes the information submitted by the input 
        /// element, if it is a submit button or image. If specified, it overrides the 
        /// action attribute of the element's form owner.
        /// </summary>
        /// <param name="action">The action URI.</param>
        /// <returns>The {T}.</returns>
        [Code("formaction")]
        T FormAction(Uri action);
    }
}