// <copyright file="Month.cs" company="Appva AB">
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
    /// A control for entering a month and year, with no time zone.
    /// </summary>
    public interface IMonth : IHtmlElement<IMonth>, IInputElement<IMonth>, IListHtmlAttribute<IMonth>,
        IReadonlyHtmlAttribute<IMonth>, IRequiredHtmlAttribute<IMonth>
    {
    }

    /// <summary>
    /// An <see cref="IMonth"/> implementation.
    /// </summary>
    internal sealed class Month : AbstractInputElement<IMonth>, IMonth   
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Month"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Month(HtmlHelper htmlHelper)
            : base(InputType.Month, htmlHelper)
        {
        }

        #endregion
    }
}