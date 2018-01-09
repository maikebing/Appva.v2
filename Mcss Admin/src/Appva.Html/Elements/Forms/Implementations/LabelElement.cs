// <copyright file="LabelElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.ComponentModel;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// An implementation of <see cref="ILabel"/>.
    /// </summary>
    internal sealed class LabelElement : HtmlElement<ILabel>, ILabel
    {
        #region Variables.

        /// <summary>
        /// The tag.
        /// </summary>
        private static readonly Tag Tag = Tag.New("label");

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelElement"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <param name="text">The label text.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public LabelElement(HtmlHelper htmlHelper, string text)
            : base(htmlHelper, Tag)
        {
            this.Builder.SetInnerText(text);
        }

        #endregion

        #region IForHtmlAttribute<ILabelElement> Members.

        public ILabel For(string id)
        {
            this.AddAttribute<IForHtmlAttribute<ILabel>>(x => x.For(id), null, id);
            return this;
        }

        #endregion

        #region Internal Members.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        internal void OnIdChangedListener(object sender, PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName;
            this.AddAttribute<IForHtmlAttribute<ILabel>>(x => x.For(null), null, (sender as IHtmlElement).UniqueId);
        }

        #endregion
    }
}