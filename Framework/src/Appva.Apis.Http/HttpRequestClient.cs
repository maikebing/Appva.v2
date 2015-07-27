// <copyright file="HttpRequestClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HttpRequestClient : IHttpRequestClient
    {
        #region Private fields

        /// <summary>
        /// The <see cref="HttpClient"/>
        /// </summary>
        private HttpClient httpClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestClient"/> class.
        /// </summary>
        public HttpRequestClient()
        {
            this.httpClient = new HttpClient();

        }

        #endregion

        #region IHttpRequestClient Members

        public IHttpRequest Get(HttpMethod method, string url)
        {
            return new HttpRequest(method, new Uri(url), this.httpClient);
        }

        public IHttpRequest Put(HttpMethod method, string url)
        {
            return new HttpRequest(method, new Uri(url), this.httpClient);
        }

        public IHttpRequest Post(HttpMethod method, string url)
        {
            return new HttpRequest(method, new Uri(url), this.httpClient);
        }

        #endregion
    }
}