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
    using System.Web;
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
        #region Properties.

        /// <summary>
        /// Returns the claims principal.
        /// </summary>
        public ClaimsPrincipal Principal
        {
            get
            {
                return (ClaimsPrincipal) this.User;
            }
        }

        /// <summary>
        /// Returns the tenant name (from owin pipeline).
        /// </summary>
        public string Tenant
        {
            get
            {
                return this.Request.GetOwinContext().TenantName();
            }
        }

        /// <summary>
        /// Returns the page title.
        /// </summary>
        /// <returns>The page title</returns>
        public string Title
        {
            get
            {
                return this.ViewBag.Title;
            }
        }

        /// <summary>
        /// Returns the body class.
        /// </summary>
        /// <returns>The body class</returns>
        public string BodyCssClass
        {
            get
            {
                return this.ViewBag.BodyCssClass == null ? string.Empty : this.ViewBag.BodyCssClass;
            }
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Sets the page title.
        /// </summary>
        /// <param name="pageTitle">The page title</param>
        public void SetTitle(string pageTitle)
        {
            this.ViewBag.Title = pageTitle;
        }

        /// <summary>
        /// Sets the page title.
        /// </summary>
        /// <param name="format">The page title format</param>
        /// <param name="args">The page format arguments</param>
        public void SetTitle(string format, params string[] args)
        {
            this.ViewBag.Title = string.Format(format, args);
        }

        /// <summary>
        /// Sets the body class.
        /// </summary>
        /// <param name="bodyCssClass">The body class</param>
        public void SetBodyCssClass(string bodyCssClass)
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
            if (this.ViewBag.Meta.IsNull())
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
            var builder  = new StringBuilder();
            var metaTags = this.ViewBag.Meta as Dictionary<string, string>;
            if (metaTags.IsNotNull())
            {
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

        #endregion
    }

    /// <summary>
    /// Custom <see cref="WebViewPage"/>.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public abstract class ApplicationWebView : WebViewPage<dynamic>
    {
    }
}