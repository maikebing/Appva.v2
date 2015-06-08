// <copyright file="HttpRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence.Autofac
{
    #region Imports.

    using System;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class HttpRequest
    {
        #region Variables.

        /// <summary>
        /// Child action data token key.
        /// </summary>
        private const string ParentActionViewContextToken = "ParentActionViewContext";

        /// <summary>
        /// Model state data token key.
        /// </summary>
        private const string ModelStateContextToken = "ModelStateContext";

        /// <summary>
        /// The HTTP context.
        /// </summary>
        private readonly HttpContextBase context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Prevents a default instance of the <see cref="HttpRequest" /> class 
        /// from being created.
        /// </summary>
        private HttpRequest()
        {
            if (HttpContext.Current != null)
            {
                this.context = new HttpContextWrapper(HttpContext.Current);
            }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Returns the current HTTP request.
        /// </summary>
        /// <returns>The current <see cref="HttpRequest"/></returns>
        public static HttpRequest Current()
        {
            return new HttpRequest();
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Returns whether or not an exception occured during this
        /// HTTP request.
        /// </summary>
        /// <returns>True if any exceptions occured</returns>
        public bool IsContainingExceptions()
        {
            return this.context == null ? true : (this.context.AllErrors != null && this.context.AllErrors.Length > 0);
        }

        /// <summary>
        /// Returns whether or not an exception did not occur during this
        /// HTTP request.
        /// </summary>
        /// <returns>True if there are no exceptions present</returns>
        public bool IsWithoutExceptions()
        {
            return ! this.IsContainingExceptions();
        }

        /// <summary>
        /// Whether or not the HTTP request is a POST.
        /// </summary>
        /// <returns>True if POST</returns>
        public bool IsPost()
        {
            return this.context.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// Whether or not the HTTP request is a GET.
        /// </summary>
        /// <returns>True if GET</returns>
        public bool IsGet()
        {
            return this.context.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// Whether or not the HTTP request is not a GET.
        /// </summary>
        /// <returns>True if not GET</returns>
        public bool IsNotGet()
        {
            return ! this.IsGet();
        }

        /// <summary>
        /// Whether or not the model state is valid.
        /// </summary>
        /// <returns>True if the model state is valid</returns>
        public bool IsValidModelState()
        {
            if (this.context.Items[ModelStateContextToken] == null)
            {
                return false;
            }
            return Convert.ToBoolean(this.context.Items[ModelStateContextToken]);
        }

        /// <summary>
        /// Returns whether or not the HTTP request is a child action request.
        /// </summary>
        /// <returns>True if the HTTP request is a child request</returns>
        public bool IsChildAction()
        {
            return this.context == null || this.context.Request.RequestContext.RouteData.DataTokens.ContainsKey(ParentActionViewContextToken);
        }
        #endregion
    }
}