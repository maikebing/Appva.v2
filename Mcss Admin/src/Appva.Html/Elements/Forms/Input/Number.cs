// <copyright file="Number.cs" company="Appva AB">
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
    /// A control for entering a number.
    /// </summary>
    public interface INumber : IHtmlElement<INumber>, IInputElement<INumber>, IListHtmlAttribute<INumber>, IMinHtmlAttribute<INumber>, 
        IMaxHtmlAttribute<INumber>, IStepHtmlAttribute<INumber>, IReadonlyHtmlAttribute<INumber>, IRequiredHtmlAttribute<INumber>
    {
    }

    /// <summary>
    /// An <see cref="INumber"/> implementation.
    /// </summary>
    internal sealed class Number : AbstractInputElement<INumber>, INumber
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Number(HtmlHelper htmlHelper)
            : base(InputType.Number, htmlHelper)
        {
        }

        #endregion
    }
}