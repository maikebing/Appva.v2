// <copyright file="ITextArea.cs" company="Appva AB">
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
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The HTML <c>textarea</c> element represents a multi-line plain-text editing 
    /// control.
    /// </summary>
    public interface ITextArea : IHtmlElement<ITextArea>, IBlock<ITextArea>, IAutocompleteHtmlAttribute<ITextArea>, IAutofocusHtmlAttribute<ITextArea>,
        IColsHtmlAttribute<ITextArea>, IDisabledHtmlAttribute<ITextArea>, IFormHtmlAttribute<ITextArea>, IMaxLengthHtmlAttribute<ITextArea>,
        IMinLengthHtmlAttribute<ITextArea>, INameHtmlAttribute<ITextArea>, IPlaceholderHtmlAttribute<ITextArea>,
        IReadonlyHtmlAttribute<ITextArea>, IRequiredHtmlAttribute<ITextArea>, IRowsHtmlAttribute<ITextArea>, IWrapHtmlAttribute<ITextArea>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        ITextArea Label(ILabel label);

        /// <summary>
        /// Gecko 2.0 introduced support for resizable textareas. This is controlled by the 
        /// resize CSS property. Resizing of textareas is enabled by default, but you can 
        /// explicitly disable it by including the following.
        /// </summary>
        /// <returns>The text area.</returns>
        ITextArea DisableResize();
    }
}