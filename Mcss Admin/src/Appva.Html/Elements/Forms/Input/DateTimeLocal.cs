// <copyright file="DateTimeLocal.cs" company="Appva AB">
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
    /// A control for entering a date and time, with no time zone.
    /// </summary>
    public interface IDateTimeLocal : IHtmlElement<IDateTimeLocal>, IInputElement<IDateTimeLocal>, IListHtmlAttribute<IDateTimeLocal>, IMinHtmlAttribute<IDateTimeLocal>, IMaxHtmlAttribute<IDateTimeLocal>, IStepHtmlAttribute<IDateTimeLocal>, IReadonlyHtmlAttribute<IDateTimeLocal>, IRequiredHtmlAttribute<IDateTimeLocal>
    {
    }

    /// <summary>
    /// An <see cref="IDateTimeLocal"/> implementation.
    /// </summary>
    internal sealed class DateTimeLocal : Input<IDateTimeLocal>, IDateTimeLocal 
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeLocal"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public DateTimeLocal(HtmlHelper htmlHelper)
            : base(InputType.DateTimeLocal, htmlHelper)
        {
        }

        #endregion
    }
}