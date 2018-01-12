// <copyright file="Block.cs" company="Appva AB">
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
    using System.Web.WebPages;
    using Appva.Html.Infrastructure;

    #endregion

    public interface IBlock<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        MvcHtmlString Content(Func<T, HelperResult> content);

        MvcHtmlString Content(object value);
    }

    /// <summary>
    /// Represents an HTML component element in an MVC view.
    /// Represents flow content.
    /// Elements belonging to the flow content category typically contain text or embedded content
    /// </summary>
    [Category("Flow-content")]
    public abstract class Block<T> : HtmlElement<T>, IBlock<T> where T : class, IHtmlElement<T>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FlowContent"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <param name="tag">The <see cref="Tag"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="tag"/> is null.
        /// </exception>
        protected Block(HtmlHelper htmlHelper, Tag tag)
            : base(htmlHelper, tag)
        {
        }

        #endregion

        /// <inheritdoc />
        public MvcHtmlString Content(Func<T, HelperResult> content)
        {
            if (IsAuthorized())
            {
                this.OnBeforeBegin();
                this.Write(this.Render(TagRenderMode.StartTag));
                this.OnBegin();
                this.Write(content(null).ToHtmlString());
                this.OnBeforeEnd();
                this.Write(this.Render(TagRenderMode.EndTag));
                this.OnEnd();
            }
            return MvcHtmlString.Empty;
        }

        /// <inheritdoc />
        public MvcHtmlString Content(object value)
        {
            if (IsAuthorized())
            {
                this.OnBeforeBegin();
                this.Write(this.Render(TagRenderMode.StartTag));
                this.OnBegin();
                this.Write(value.ToString());
                this.OnBeforeEnd();
                this.Write(this.Render(TagRenderMode.EndTag));
                this.OnEnd();
            }
            return MvcHtmlString.Empty;
        }

        protected virtual bool IsAuthorized()
        {
            return true;
        }

        protected virtual void OnBeforeBegin()
        {
            //// no op!
        }

        protected virtual void OnBegin()
        {
            //// no op!
        }

        protected virtual void OnBeforeEnd()
        {
            //// no op!
        }

        protected virtual void OnEnd()
        {
            //// no op!
        }
    }
}