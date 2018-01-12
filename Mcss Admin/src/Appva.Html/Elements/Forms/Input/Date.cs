// <copyright file="Date.cs" company="Appva AB">
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
    /// A control for entering a date (year, month, and day, with no time).
    /// </summary>
    public interface IDate : IHtmlElement<IDate>, IInputElement<IDate>, IListHtmlAttribute<IDate>, IMinHtmlAttribute<IDate>, IMaxHtmlAttribute<IDate>, IStepHtmlAttribute<IDate>, IReadonlyHtmlAttribute<IDate>, IRequiredHtmlAttribute<IDate>
    {
    }

    /// <summary>
    /// An <see cref="IDate"/> implementation.
    /// </summary>
    public sealed class Date : AbstractInputElement<IDate>, IDate
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Date"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Date(HtmlHelper htmlHelper)
            : base(InputType.Date, htmlHelper)
        {
        }

        #endregion
    }
}