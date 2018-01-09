// <copyright file="Submit.cs" company="Appva AB">
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
    /// A button that submits the form.
    /// </summary>
    public interface ISubmit : IHtmlElement<ISubmit>, IInputElement<ISubmit>, IFormActionHtmlAttribute<ISubmit>,
        IFormEncTypeHtmlAttribute<ISubmit>, IFormMethodHtmlAttribute<ISubmit>, IFormNoValidateHtmlAttribute<ISubmit>,
        IFormTargetHtmlAttribute<ISubmit>, IListHtmlAttribute<ISubmit>
    {
    }

    /// <summary>
    /// An <see cref="ISubmit"/> implementation.
    /// </summary>
    internal sealed class Submit : Input<ISubmit>, ISubmit
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Submit"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Submit(HtmlHelper htmlHelper)
            : base(InputType.Submit, htmlHelper)
        {
        }

        #endregion
    }
}