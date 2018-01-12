// <copyright file="IButtonElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// The HTML <button> element represents a clickable button, which can be used in 
    /// forms, or anywhere in a document that needs simple, standard button 
    /// functionality.
    /// </summary>
    public interface IButtonElement : IHtmlElement<IButtonElement>, IBlock<IButtonElement>, IAutofocusHtmlAttribute<IButtonElement>,
        IDisabledHtmlAttribute<IButtonElement>, IFormHtmlAttribute<IButtonElement>, IFormActionHtmlAttribute<IButtonElement>,
        IFormEncTypeHtmlAttribute<IButtonElement>, IFormMethodHtmlAttribute<IButtonElement>, IFormNoValidateHtmlAttribute<IButtonElement>,
        IFormTargetHtmlAttribute<IButtonElement>, INameHtmlAttribute<IButtonElement>, ITypeHtmlAttribute<IButtonElement>,
        IValueHtmlAttribute<IButtonElement> 
    {
    }

    public interface ITypeHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The type of the button.
        /// </summary>
        /// <param name="value">The type value.</param>
        /// <returns>The button element.</returns>
        [Code("type")]
        T Type(ButtonType value);
    }

    /// <summary>
    /// The type of the button.
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// The button submits the form data to the server. This is the default if the 
        /// attribute is not specified, or if the attribute is dynamically changed to an 
        /// empty or invalid value.
        /// </summary>
        [Code("submit")]
        Submit,

        /// <summary>
        /// The button resets all the controls to their initial values.
        /// </summary>
        [Code("reset")]
        Reset,

        /// <summary>
        /// The button has no default behavior. It can have client-side scripts associated 
        /// with the element's events, which are triggered when the events occur.
        /// </summary>
        [Code("button")]
        Button
    }
}