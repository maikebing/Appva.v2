// <copyright file="Reset.cs" company="Appva AB">
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
    /// A button that resets the contents of the form to default values.
    /// </summary>
    public interface IReset : IHtmlElement<IReset>, IInputElement<IReset>, IListHtmlAttribute<IReset>
    {
    }

    /// <summary>
    /// An <see cref="IReset"/> implementation.
    /// </summary>
    internal sealed class Reset : AbstractInputElement<IReset>, IReset
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Reset"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Reset(HtmlHelper htmlHelper)
            : base(InputType.Reset, htmlHelper)
        {
        }

        #endregion
    }
}