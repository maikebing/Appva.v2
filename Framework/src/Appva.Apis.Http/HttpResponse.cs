// <copyright file="HttpResponse.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http
{
    #region Imports.

    using Appva.Core.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class HttpResponse : IHttpResponse
    {
        #region Fields

        /// <summary>
        /// The <see cref="HttpResponseMessage"/>
        /// </summary>
        private readonly HttpResponseMessage message;

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="HttpResponse"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<HttpResponse>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse"/> class.
        /// </summary>
        public HttpResponse(HttpResponseMessage message)
        {
            this.message = message;

            Log.Debug(string.Format(
                "Recived response from {0} with statuscode {1} and content: {2}", 
                this.message.RequestMessage.RequestUri.ToString(),
                this.message.StatusCode,
                this.message.Content.ToString()));
        }

        #endregion

        #region IHttpResponse members

        /// <inheritdoc />
        public async Task<T> ToResultAsync<T>()
        {
            var content = await this.ToResultAsString().ConfigureAwait(false);

            Log.Debug(string.Format("Content: {0}", content));

            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <inheritdoc />
        public async Task<string> ToResultAsString()
        {
            return await this.message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public HttpStatusCode GetStatusCode()
        {
            return this.message.StatusCode;
        }

        /// <inheritdoc />
        public bool IsSuccessStatusCode()
        {
            return this.message.IsSuccessStatusCode;
        }

        #endregion
    }
}