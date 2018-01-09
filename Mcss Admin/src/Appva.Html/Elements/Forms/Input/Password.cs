// <copyright file="Password.cs" company="Appva AB">
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
    /// A single-line text field whose value is obscured.
    /// </summary>
    public interface IPassword : IHtmlElement<IPassword>, IInputElement<IPassword>, IListHtmlAttribute<IPassword>,
        IMaxLengthHtmlAttribute<IPassword>, IMinLengthHtmlAttribute<IPassword>, IPatternHtmlAttribute<IPassword>,
        IReadonlyHtmlAttribute<IPassword>, IRequiredHtmlAttribute<IPassword>, ISizeHtmlAttribute<IPassword>
    {
    }

    /// <summary>
    /// An <see cref="IPassword"/> implementation.
    /// </summary>
    internal sealed class Password : Input<IPassword>, IPassword
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Password(HtmlHelper htmlHelper)
            : base(InputType.Password, htmlHelper)
        {
        }

        #endregion
    }
}