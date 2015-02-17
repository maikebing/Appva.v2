// <copyright file="TenantClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Appva.Apis.TenantServer.Contracts;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantClient : ITenantClient
    {
        #region Variables.

        /// <summary>
        /// The re-usable <see cref="HttpClient"/> instance.
        /// </summary>
        private readonly HttpClient httpClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantClient"/> class.
        /// </summary>
        public TenantClient(ITenantServerConfiguration configuration)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = configuration.Uri;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }

        #endregion

        #region ITenantService Members.

        /// <inheritdoc />
        public Tenant Get(object id)
        {
            return this.GetRequest<Tenant>(string.Format("tenant/{0}", id));
        }

        /// <inheritdoc />
        public IList<Tenant> ListAll()
        {
            return this.GetRequest<IList<Tenant>>("tenants");
        }

        /// <inheritdoc />
        public Client GetClientByTenantId(object id)
        {
            return this.GetRequest<Client>(string.Format("tenant/{0}/client", id));
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Creates a syncronous HTTP request and deserialize the response
        /// to the {T} type.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="uri">The operation uri</param>
        /// <returns>An instance of {T}</returns>
        private T GetRequest<T>(string uri)
        {
            var response = this.httpClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(content);
            }
            return default(T);
        }

        /// <summary>
        /// Creates a asyncronous HTTP request and deserialize the response
        /// to the Task{T} type.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="uri">The operation uri</param>
        /// <returns>An instance of Task{T}</returns>
        private async Task<T> GetRequestAsync<T>(string uri)
        {
            var response = await this.httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return default(T);
        }

        #endregion
    }
}