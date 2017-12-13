// <copyright file="FlowContent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
using System.Web.WebPages;
    using Appva.Html.Infrastructure.Internal;

    #endregion

    public interface IBlock<T> : IDisposable
    {
        MvcHtmlString Content(Func<T, HelperResult> content);
    }

    /// <summary>
    /// Represents an HTML component element in an MVC view.
    /// Represents flow content.
    /// Elements belonging to the flow content category typically contain text or embedded content
    /// </summary>
    public abstract class Block<T> : IBlock<T> where T : class
    {
        #region Variables.

        /// <summary>
        /// The <see cref="HtmlHelper"/>.
        /// </summary>
        private readonly HtmlHelper htmlHelper;

        /// <summary>
        /// The <see cref="TagBuilder"/>.
        /// </summary>
        private readonly TagBuilder builder;

        /// <summary>
        /// Whether or not the <see cref="Block"/> class has been disposed.
        /// </summary>
        private bool isDisposed;

        #endregion

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
        {
            Argument.Null(htmlHelper, "htmlHelper");
            Argument.Null(tag,        "tag");
            this.htmlHelper = htmlHelper;
            this.builder    = new TagBuilder(tag.Name);
        }

        #endregion

        protected virtual void Initialize()
        {
            this.OnBeforeBegin();
            this.Html.ViewContext.Writer.Write(this.Builder.ToString(TagRenderMode.StartTag));
            this.OnBegin();
        }

        protected virtual void Finalize()
        {
            this.OnBeforeEnd();
            this.Html.ViewContext.Writer.Write(this.builder.ToString(TagRenderMode.EndTag));
            this.OnEnd();
        }

        // INITIALIZE 
        // FINALIZE
        // Open
        // Close

        #region Properties.

        /// <summary>
        /// 
        /// </summary>
        protected HtmlHelper Html
        {
            get
            {
                return this.htmlHelper;
            }
        }

        protected TagBuilder Builder
        {
            get
            {
                return this.builder;
            }
        }

        #endregion

        /// <inheritdoc />
        public MvcHtmlString Content(Func<T, HelperResult> content)
        {
            this.Html.ViewContext.Writer.Write(content(null).ToHtmlString());
            return MvcHtmlString.Empty;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }
            this.isDisposed = true;
            this.Finalize();
        }

        protected virtual void OnBeforeBegin()
        {
        }

        protected virtual void OnBegin()
        {
        }

        protected virtual void OnBeforeEnd()
        {
        }

        protected virtual void OnEnd()
        {
        }
    }
}