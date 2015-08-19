// <copyright file="HttpRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http
{
    #region Imports.

    using Appva.Apis.Http.Converters;
    using Appva.Core.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http.Hosting;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HttpRequest : HttpRequestMessage,IHttpRequest
    {
        #region Private fields

        /// <summary>
        /// The <see cref="HttpClient"/>
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="DemoHipClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<HttpRequest>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequest"/> class.
        /// </summary>
        public HttpRequest(HttpMethod method, Uri uri, HttpClient httpClient, X509Certificate2 certificate = null)
            : base(method, new Uri(httpClient.BaseAddress, uri))
        {
            
            this.httpClient = httpClient;

            if(certificate != null)
            {
                this.Properties.Add(HttpPropertyKeys.ClientCertificateKey, certificate);
            }
        }

        #endregion

        #region IHttpRequest members

        /// <inheritdoc />
        public IHttpRequest WithHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                if (!this.Headers.Contains(header.Key))
                {
                    this.Headers.Add(header.Key, header.Value);
                }
            }
            return this;
        }

        /// <inheritdoc />
        public IHttpRequest WithBody(string body)
        {
            this.WithBody(body, MimeType.PlainText.Description());
            return this;
        }

        /// <inheritdoc />
        public IHttpRequest WithBody(string body, string mediaType)
        {
            this.Content = new StringContent(body, Encoding.UTF8, mediaType);
            return this;
        }

        /// <inheritdoc />
        public IHttpRequest WithFormUrlEncodedBody(IDictionary<string, string> body)
        {
            this.Content = new FormUrlEncodedContent(body);
            return this;
        }

        /// <inheritdoc />
        public IHttpRequest WithFormUrlEncodedBody<T>(object body) where T : class
        {
            this.Content = FormUrlEncodedSerializer.Serialize<T>(body);
            return this;
        }

        /// <inheritdoc />
        public IHttpRequest WithJsonEncodedBody(object body)
        {
            return this.WithBody(JsonConvert.SerializeObject(body), MimeType.Json.Description());
        }

        /// <inheritdoc />
        public async Task<T> ToResultAsync<T>()
        {
            return JsonConvert.DeserializeObject<T>(await this.ToResultAsString().ConfigureAwait(false));
        }

        /// <inheritdoc />
        public async Task<string> ToResultAsString()
        {
            return await this.GetContent().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public HttpStatusCode GetResponseStatusCode()
        {
            return this.httpClient.SendAsync(this).Result.StatusCode;
        }

        #endregion

        #region Private

        /// <summary>
        /// Reads the content of the response
        /// </summary>
        /// <returns></returns>
        private async Task<String> GetContent()
        {
            try
            {
                Log.Debug("Sending message to {0}", this.ToString());
                var response = await this.httpClient.SendAsync(this).ConfigureAwait(false);
                Log.Debug("Recived answer: {0}", response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                return null;
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return null;
            }
        }

        #endregion


        
    }
}