// <copyright file="HttpRequestClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http
{
    #region Imports.

    using Appva.Cryptography.X509;
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

        public HttpRequestClient(string url)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(url);
        }

        public HttpRequestClient(string url, string clientCertificatePath, string clientCertificatePassword)
        {
            var handler = new WebRequestHandler();
            handler.ClientCertificates.Add(CertificateUtils.LoadCertificateFromDisk(clientCertificatePath, clientCertificatePassword));

            this.httpClient = new HttpClient(handler);
            this.httpClient.BaseAddress = new Uri(url);
        }

        #endregion

        #region IHttpRequestClient Members

        public IHttpRequest Get(string url)
        {
            return new HttpRequest(HttpMethod.Get, new Uri(url), this.httpClient);
        }

        public IHttpRequest Put(string url)
        {
            return new HttpRequest(HttpMethod.Put, new Uri(url), this.httpClient);
        }

        public IHttpRequest Post(string url)
        {
            return new HttpRequest(HttpMethod.Post, new Uri(url), this.httpClient);
        }

        #endregion
    }
}