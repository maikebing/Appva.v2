// <copyright file="Url.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using Appva.Html.Infrastructure;

    #endregion

    /// <summary>
    /// A field for entering a URL.
    /// </summary>
    public interface IUrl : IHtmlElement<IUrl>, IInputElement<IUrl>, IListHtmlAttribute<IUrl>,
        IMaxLengthHtmlAttribute<IUrl>, IMinLengthHtmlAttribute<IUrl>, IPatternHtmlAttribute<IUrl>,
        IReadonlyHtmlAttribute<IUrl>, IRequiredHtmlAttribute<IUrl>, ISizeHtmlAttribute<IUrl>
    {
    }

    /// <summary>
    /// An <see cref="IUrl"/> implementation.
    /// </summary>
    internal sealed class Url : AbstractInputElement<IUrl>, IUrl
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Url(HtmlHelper htmlHelper)
            : base(InputType.Url, htmlHelper)
        {
        }

        #endregion
    }
}