// <copyright file="ContextRequestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mvc
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class ContextRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Fields

        /// <summary>
        /// The <see cref="HttpContextBase"/>
        /// </summary>
        private readonly HttpContextBase context;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor with HttpContext
        /// </summary>
        /// <param name="context"></param>
        public ContextRequestHandler(HttpContextBase context)
        {
            this.context = context;
        }

        #endregion

        #region Members

        /// <summary>
        /// Redirects to given action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        protected void Redirect(string action, string controller)
        {
            var urlHelper = new UrlHelper(this.context.Request.RequestContext);
            var url = urlHelper.Action(action, controller);
            this.context.Response.Redirect(url);
        }

        /// <summary>
        /// Redirects to given action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        protected void Redirect(string action, string controller, object routeValues)
        {
            var urlHelper = new UrlHelper(this.context.Request.RequestContext);
            var url = urlHelper.Action(action, controller, routeValues);
            this.context.Response.Redirect(url);
        }

        /// <summary>
        /// Redirects to given url
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        protected void Redirect(string url)
        {
            this.context.Response.ClearHeaders();
            this.context.Response.Redirect(url);
        }

        protected string CurrentUrl()
        {
            return this.context.Request.Url.ToString();
        }

        #endregion
    }
}