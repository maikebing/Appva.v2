// <copyright file="Time.cs" company="Appva AB">
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
    /// A control for entering a time value with no time zone.
    /// </summary>
    public interface ITime : IHtmlElement<ITime>, IInputElement<ITime>, IListHtmlAttribute<ITime>, IMinHtmlAttribute<ITime>, 
        IMaxHtmlAttribute<ITime>, IStepHtmlAttribute<ITime>, IReadonlyHtmlAttribute<ITime>, IRequiredHtmlAttribute<ITime>
    {
    }

    /// <summary>
    /// An <see cref="ITime"/> implementation.
    /// </summary>
    internal sealed class Time : Input<ITime>, ITime
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Time(HtmlHelper htmlHelper)
            : base(InputType.Time, htmlHelper)
        {
        }

        #endregion
    }
}