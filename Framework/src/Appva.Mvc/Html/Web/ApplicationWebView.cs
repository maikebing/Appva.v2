// <copyright file="ApplicationWebView.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>

using Appva.Core.Extensions;

namespace Appva.Mvc.Html
{
    #region Imports.

    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Web.Mvc;

    #endregion
    
    /// <summary>
    /// Custom <see cref="WebViewPage{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    public abstract class ApplicationWebView<T> : WebViewPage<T>
    {
        /// <summary>
        /// Returns the claims principal.
        /// </summary>
        /// <returns>The <see cref="ClaimsPrincipal"/></returns>
        public ClaimsPrincipal Principal()
        {
            return (ClaimsPrincipal) this.User;
        }

        /// <summary>
        /// Returns the page title.
        /// </summary>
        /// <returns>The page title</returns>
        public string Title()
        {
            return this.ViewBag.Title;
        }

        /// <summary>
        /// Sets the page title.
        /// </summary>
        /// <param name="pageTitle">The page title</param>
        public void Title(string pageTitle)
        {
            this.ViewBag.Title = pageTitle;
        }

        /// <summary>
        /// Returns the body class.
        /// </summary>
        /// <returns>The body class</returns>
        public string BodyCssClass()
        {
            if (this.ViewBag.BodyCssClass == null)
            {
                return string.Empty;
            }
            return this.ViewBag.BodyCssClass;
        }

        /// <summary>
        /// Sets the body class.
        /// </summary>
        /// <param name="bodyCssClass">The body class</param>
        public void BodyCssClass(string bodyCssClass)
        {
            this.ViewBag.BodyCssClass = bodyCssClass;
        }

        /// <summary>
        /// Adds an HTML meta attribute.
        /// </summary>
        /// <param name="name">The meta name</param>
        /// <param name="content">The meta content</param>
        public void AddMeta(string name, string content)
        {
            if (this.ViewBag.Meta == null)
            {
                this.ViewBag.Meta = new Dictionary<string, string>();
            }
            ((Dictionary<string, string>) this.ViewBag.Meta).Add(name, content);
        }

        /// <summary>
        /// Renders the HTML meta attributes.
        /// </summary>
        /// <returns>The meta data attributes</returns>
        public MvcHtmlString RenderMeta()
        {
            if (this.ViewBag.Meta == null)
            {
                return MvcHtmlString.Empty;
            }
            var builder = new StringBuilder();
            var metaTags = this.ViewBag.Meta as Dictionary<string, string>;
            if (metaTags.IsNotNull())
            {
                foreach (var kv in metaTags)
                {
                    var metaTag = new TagBuilder("meta");
                    metaTag.Attributes.Add("name", kv.Key);
                    metaTag.Attributes.Add("content", kv.Value);
                    builder.Append(metaTag.ToString(TagRenderMode.SelfClosing));
                }
            }
            return MvcHtmlString.Create(builder.ToString());
        }
    }
    public abstract class ApplicationWebView : WebViewPage<dynamic> { }
}