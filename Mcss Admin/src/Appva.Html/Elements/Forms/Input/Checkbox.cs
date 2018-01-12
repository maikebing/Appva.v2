// <copyright file="Checkbox.cs" company="Appva AB">
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
    /// A check box allowing single values to be selected/deselected.
    /// </summary>
    public interface ICheckbox : IHtmlElement<ICheckbox>, IInputElement<ICheckbox>, 
        ICheckedHtmlAttribute<ICheckbox>, IRequiredHtmlAttribute<ICheckbox>, IValueHtmlAttribute<ICheckbox>
    {
    }

    /// <summary>
    /// An <see cref="ICheckbox"/> implementation.
    /// </summary>
    internal sealed class Checkbox : AbstractInputElement<ICheckbox>, ICheckbox
    {
        #region Variables.

        //private readonly Hidden hidden;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Checkbox"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Checkbox(HtmlHelper htmlHelper, bool value)
            : base(InputType.Checkbox, htmlHelper)
        {
            //this.hidden = new Hidden(htmlHelper);
            //this.hidden.Value("false");
            //PropertyChanged += hidden.OnNameChangedListener;
            //this.AddElement(Position.After, hidden);
            //this.Value(value ? "true" : "false");
        }

        #endregion
    }
}