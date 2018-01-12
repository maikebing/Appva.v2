// <copyright file="Week.cs" company="Appva AB">
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
    /// A control for entering a date consisting of a week-year number and a week number 
    /// with no time zone.
    /// </summary>
    public interface IWeek : IHtmlElement<IWeek>, IInputElement<IWeek>, IListHtmlAttribute<IWeek>, IMinHtmlAttribute<IWeek>, 
        IMaxHtmlAttribute<IWeek>, IStepHtmlAttribute<IWeek>, IReadonlyHtmlAttribute<IWeek>, IRequiredHtmlAttribute<IWeek>
    {
    }

    /// <summary>
    /// An <see cref="IWeek"/> implementation.
    /// </summary>
    internal sealed class Week : AbstractInputElement<IWeek>, IWeek
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Week"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Week(HtmlHelper htmlHelper)
            : base(InputType.Week, htmlHelper)
        {
        }

        #endregion
    }
}