// <copyright file="Radio.cs" company="Appva AB">
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
    /// A radio button, allowing a single value to be selected out of multiple choices.
    /// </summary>
    public interface IRadio : IHtmlElement<IRadio>, IInputElement<IRadio>, ICheckedHtmlAttribute<IRadio>, IRequiredHtmlAttribute<IRadio>
    {
    }

    /// <summary>
    /// An <see cref="IRadio"/> implementation.
    /// </summary>
    internal sealed class Radio : AbstractInputElement<IRadio>, IRadio
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Radio"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Radio(HtmlHelper htmlHelper)
            : base(InputType.Radio, htmlHelper)
        {
        }

        #endregion
    }
}