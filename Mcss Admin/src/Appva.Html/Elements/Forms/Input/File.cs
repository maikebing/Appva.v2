// <copyright file="File.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Html.Infrastructure;

    #endregion

    /// <summary>
    /// A control that lets the user select a file. Use the accept attribute to define 
    /// the types of files that the control can select.
    /// </summary>
    public interface IFile : IHtmlElement<IFile>, IInputElement<IFile>, ICaptureHtmlAttribute<IFile>, IAcceptHtmlAttribute<IFile>, IMultipleHtmlAttribute<IFile>, IRequiredHtmlAttribute<IFile>
    {
    }

    /// <summary>
    /// An <see cref="IFile"/> implementation.
    /// </summary>
    internal sealed class File : Input<IFile>, IFile
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public File(HtmlHelper htmlHelper)
            : base(InputType.File, htmlHelper)
        {
        }

        #endregion
    }
}