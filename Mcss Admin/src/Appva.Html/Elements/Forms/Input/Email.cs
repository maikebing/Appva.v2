// <copyright file="Email.cs" company="Appva AB">
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
    /// A field for editing an e-mail address.
    /// </summary>
    public interface IEmail : IHtmlElement<IEmail>, IInputElement<IEmail>, IListHtmlAttribute<IEmail>, IMaxLengthHtmlAttribute<IEmail>, IMinLengthHtmlAttribute<IEmail>, IMultipleHtmlAttribute<IEmail>, IPatternHtmlAttribute<IEmail>, IReadonlyHtmlAttribute<IEmail>, ISizeHtmlAttribute<IEmail>
    {
    }

    /// <summary>
    /// An <see cref="IEmail"/> implementation.
    /// </summary>
    internal sealed class Email : AbstractInputElement<IEmail>, IEmail   
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Email(HtmlHelper htmlHelper)
            : base(InputType.Email, htmlHelper)
        {
        }

        #endregion
    }
}