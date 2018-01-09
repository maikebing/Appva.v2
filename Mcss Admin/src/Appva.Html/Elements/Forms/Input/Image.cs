// <copyright file="Image.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.ComponentModel;
    using System.Web.Mvc;
    using Appva.Html.Infrastructure;

    #endregion

    /// <summary>
    /// A graphical submit button. You must use the src attribute to define the source 
    /// of the image and the alt attribute to define alternative text. You can use the 
    /// height and width attributes to define the size of the image in pixels.
    /// </summary>
    public interface IImage : IHtmlElement<IImage>, IInputElement<IImage>, IAltHtmlAttribute<IImage>,
        IFormActionHtmlAttribute<IImage>, IFormEncTypeHtmlAttribute<IImage>, IFormMethodHtmlAttribute<IImage>,
        IFormNoValidateHtmlAttribute<IImage>, IFormTargetHtmlAttribute<IImage>, IHeightHtmlAttribute<IImage>,
        IListHtmlAttribute<IImage>, IReadonlyHtmlAttribute<IImage>, ISrcHtmlAttribute<IImage>, IWidthHtmlAttribute<IImage> 
    {
    }

    /// <summary>
    /// An <see cref="IImage"/> implementation.
    /// </summary>
    internal sealed class Image : Input<IImage>, IImage    
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="location">The URI location.</param>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="location"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Image(Uri location, HtmlHelper htmlHelper)
            : base(InputType.Image, htmlHelper)
        {
            Argument.Guard.NotNull("location", location);
            this.AddAttribute<Image>(x => x.Source(null), location);
        }

        #endregion
    }
}