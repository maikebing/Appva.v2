// <copyright file="HttpRequest.cs" company="Appva AB">
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
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HttpRequest : IHttpRequest
    {
        #region Private fields

        /// <summary>
        /// The message
        /// </summary>
        private HttpRequestMessage message;

        /// <summary>
        /// The <see cref="HttpClient"/>
        /// </summary>
        private HttpClient httpClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequest"/> class.
        /// </summary>
        public HttpRequest(HttpMethod method, Uri uri, HttpClient httpClient)
        {
            this.message = new HttpRequestMessage(method, uri);
            this.httpClient = httpClient;
        }

        #endregion

        #region IHttpRequest members

        /// <inheritdoc />
        public HttpRequest Headers(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                if (!this.message.Properties.ContainsKey(header.Key))
                {
                    this.message.Properties.Add(header.Key, header.Value);
                }
                else
                {
                    this.message.Properties[header.Key] = header.Value;
                }
            }
            return this;
        }

        /// <inheritdoc />
        public HttpRequest Body(string body)
        {
            this.Body(body, "text/plain");
            return this;
        }

        /// <inheritdoc />
        public HttpRequest Body(string body, string mediaType)
        {
            this.message.Content = new StringContent(body, Encoding.UTF8, mediaType);
            return this;
        }

        /// <inheritdoc />
        public HttpRequest Body(IDictionary<string,string> body)
        {
            this.message.Content = new FormUrlEncodedContent(body);
            return this;
        }

        /// <inheritdoc />
        public string GetContentAsString()
        {
            return GetContent().Result;
        }

        #endregion

        #region Private

        /// <summary>
        /// Reads the content of the response
        /// </summary>
        /// <returns></returns>
        private async Task<String> GetContent()
        {
            var response = await this.httpClient.SendAsync(this.message);

            return await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
        }

        #endregion
    }
}