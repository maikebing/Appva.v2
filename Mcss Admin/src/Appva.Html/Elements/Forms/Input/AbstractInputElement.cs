// <copyright file="AbstractInputElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Appva.Html.Infrastructure;
using Appva.Html.Infrastructure.Internal;

    #endregion
    public enum Position
    {
        Before,
        After,
        Wrap
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInputElement<T> :
        IHtmlElement<T>, 
        IAutocompleteHtmlAttribute<T>,
        IAutofocusHtmlAttribute<T>, /* not hidden */
        IDisabledHtmlAttribute<T>,
        IFormHtmlAttribute<T>,
        INameHtmlAttribute<T>,
        IPlaceholderHtmlAttribute<T>,
        IReadonlyHtmlAttribute<T>,
        IRequiredHtmlAttribute<T>,
        ISelectionDirectionHtmlAttribute<T>,
        ISelectionEndHtmlAttribute<T>,
        ISelectionStartHtmlAttribute<T>,
        ISizeHtmlAttribute<T>, /* not correct to be on all elements - but its used like this :( */
        IValueHtmlAttribute<T>
    {
        T Label(string text);
        T Label(ILabel label, Position position = Position.Before);
        /*T Label(Position position = Position.Before);

        T Label(string text, Position position = Position.Before);

        T Description(Position position = Position.After);

        T Description(string text, Position position = Position.After);*/

        /// <summary> 
        /// Constructs the element.
        /// </summary>
        /// <returns>An mvc html string representation of the element.</returns>
        MvcHtmlString Build();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AbstractInputElement<T> : HtmlElement<T>, IInputElement<T> where T : class, IHtmlElement<T>
    {
        #region Variables.

        /// <summary>
        /// 
        /// </summary>
        private static readonly Tag Tag = Tag.New("input");

        /// <summary>
        /// The input type.
        /// </summary>
        private readonly InputType type;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractInputElement"/> class.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        protected AbstractInputElement(InputType type, HtmlHelper htmlHelper)
            : base(htmlHelper, Tag)
        {
            this.type   = type;
            base.AddAttribute("type", type.ToString().ToLower());
        }
       
        #endregion

        #region IInputElement<T> Members.

        /// <inheritdoc />
        public T Label(string text)
        {
            var label = new LabelElement(this.Html, text);
            PropertyChanged += label.OnIdChangedListener;
            this.AddElement(Position.Before, label.For(this.UniqueId));
            return this as T;
        }

        /// <inheritdoc />
        public T Label(ILabel label, Position position = Position.Before)
        {
            PropertyChanged += ((LabelElement) label).OnIdChangedListener;
            this.AddElement(position, label.For(this.UniqueId));
            return this as T;
        }

        /// <inheritdoc />
        public MvcHtmlString Build()
        {
            return MvcHtmlString.Create(base.Builder.ToString(TagRenderMode.SelfClosing));
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return base.Builder.ToString(TagRenderMode.SelfClosing);
        }
    }
}