// <copyright file="RestfulClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Http
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Extensions;
    using Logging;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class RestfulClient
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="TenantClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<RestfulClient>();

        /// <summary>
        /// The <c>Uri</c> to the resource server.
        /// </summary>
        private Uri baseAddress;

        /// <summary>
        /// The re-usable <see cref="HttpClient"/> instance.
        /// </summary>
        private readonly HttpClient httpClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// <param name="baseAddress">The <c>Uri</c> to the resource server</param>
        /// </summary>
        public RestfulClient(string baseAddress)
            : this(new Uri(baseAddress))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// <param name="baseAddress">The <c>Uri</c> to the resource server</param>
        /// </summary>
        public RestfulClient(Uri baseAddress)
        {
            Log.DebugFormat(Debug.Messages.ClassInitialization, baseAddress);
            this.httpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeMediaTypes.Json)); 
        }

        #endregion

        #region Protected Functions.

        /*public T CreateNewRequest<T>()
        {
            return this.CreateNewRequestAsync().Result;
        }*/

        /// <summary>
        /// Throws an exception if the status code is not successful.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<T> CreateNewGetAsync<T>(string format, params object[] parameters)
        {
            var uri = string.Format(format, parameters);
            if (Log.IsDebugEnabled())
            {
                //Log.DebugFormat(Debug.Messages.HttpRequestMessage, this.httpClient.BaseAddress + uri);
            }
            var response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            //Log.DebugFormat(Debug.Messages.HttpResponseMessage, data);
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected async Task<T> CreateNewPostAsync<T>(string uri, object value = null)
        {
            var request = (value != null) ? JsonConvert.SerializeObject(value) : null;
            this.DebugRequest(HttpMethod.Post, uri, request);
            var response = await this.httpClient.PostAsync(
                uri, 
                new StringContent(
                    request, 
                    Encoding.UTF8, 
                    MimeMediaTypes.Json))
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            this.DebugResponse(response, data);
            return JsonConvert.DeserializeObject<T>(data);
        }

        #endregion

        private void DebugRequest(HttpMethod method, string uri, string data)
        {
            if (! Log.IsDebugEnabled())
            {
                return;
            }
            if (method.Equals(HttpMethod.Get))
            {
                Log.DebugFormat(Debug.Messages.HttpRequestMessage, method);
            }
        }

        private void DebugResponse(HttpResponseMessage response, string data)
        {
            if (! Log.IsDebugEnabled() || response.IsNull())
            {
                return;
            }
            var content = new StringBuilder();
            content.Append(Debug.Messages.HttpResponseStatusMessage.FormatWith(response.Version, response.StatusCode, response.ReasonPhrase));
            foreach (var header in response.Headers)
            {
                content.Append(Debug.Messages.HttpResponseHeaderKeyPair.FormatWith(header.Key, header.Value));
            }
            content.Append(data);
            Log.Debug(content.ToString());
        }
    }

    internal static class MimeMediaTypes
    {
        /// <summary>
        /// JSON MIME media type.
        /// </summary>
        public const string Json = "application/json";

        /// <summary>
        /// XML MIME media type.
        /// </summary>
        public const string Xml  = "application/xml";
    }

    internal enum HttpMethod
    {
        Post,
        Get,
        Put,
        Patch,
        Delete
    }
}