// <copyright file="ApplicationWebView.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Text;
    using System.Web.Mvc;
    using Core.Extensions;
    using Resources;

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
            return this.ViewBag.BodyCssClass == null ? string.Empty : this.ViewBag.BodyCssClass;
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
                Debug.Assert(metaTags != null, "metaTags != null");
                foreach (var kv in metaTags)
                {
                    var metaTag = new TagBuilder(Tags.Meta);
                    metaTag.Attributes.Add(TagAttributes.Name, kv.Key);
                    metaTag.Attributes.Add(TagAttributes.Content, kv.Value);
                    builder.Append(metaTag.ToString(TagRenderMode.SelfClosing));
                }
            }
            return MvcHtmlString.Create(builder.ToString());
        }
    }

    /// <summary>
    /// Custom <see cref="WebViewPage"/>.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public abstract class ApplicationWebView : WebViewPage<dynamic>
    {
    }
}