// <copyright file="IHtmlElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.Web;

    #endregion

    /// <summary>
    /// Marker interface.
    /// </summary>
    public interface IHtmlElement : IHtmlString
    {
        /// <summary>
        /// The unique html ID.
        /// </summary>
        string UniqueId
        {
            get;
        }

        string NameOf
        {
            get;
        }
    }

    /// <summary>
    /// Represents an 'any' HTML element with global attributes.
    /// Global attributes may be specified on all HTML elements, even those not 
    /// specified in the standard. 
    /// <externalLink>
    ///     <linkText>Global attributes</linkText>
    ///     <linkUri>
    ///         https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IHtmlElement<T> : IHtmlElement, IRoleHtmlAttribute<T>, IAriaHtmlElement<T>, IOnEventHtmlAttribute<T>, 
        IAccessKeyHtmlAttribute<T>, IClassHtmlAttribute<T>, IContentEditableHtmlAttribute<T>, IContextMenuHtmlAttribute<T>, 
        IDataHtmlAttribute<T>, IDirHtmlAttribute<T>, IDraggableHtmlAttribute<T>, IDropzoneHtmlAttribute<T>, 
        IHiddenHtmlAttribute<T>, IIdHtmlAttribute<T>, ILangHtmlAttribute<T>, ISpellcheckHtmlAttribute<T>, 
        IStyleHtmlAttribute<T>, ITabIndexHtmlAttribute<T>, ITitleHtmlAttribute<T>, ITranslateHtmlAttribute<T>
    {
    }
}