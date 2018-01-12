// <copyright file="Tel.cs" company="Appva AB">
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
    /// A control for entering a telephone number.
    /// </summary>
    public interface ITel : IHtmlElement<ITel>, IInputElement<ITel>, IListHtmlAttribute<ITel>,
        IMaxLengthHtmlAttribute<ITel>, IMinLengthHtmlAttribute<ITel>, IPatternHtmlAttribute<ITel>,
        IReadonlyHtmlAttribute<ITel>, IRequiredHtmlAttribute<ITel>, ISizeHtmlAttribute<ITel>
    {
    }

    /// <summary>
    /// An <see cref="ITel"/> implementation.
    /// </summary>
    internal sealed class Tel : AbstractInputElement<ITel>, ITel
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Tel"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Tel(HtmlHelper htmlHelper)
            : base(InputType.Tel, htmlHelper)
        {
        }

        #endregion
    }
}