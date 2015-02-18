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
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Contracts;
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
        /// <param name="baseAddress">The tenant server base uri address</param>
        public TenantClient(Uri baseAddress)
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new tenant server client.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        /// <returns>A new <see cref="ITenantClient"/> instance</returns>
        public static ITenantClient CreateNew(Uri baseAddress)
        {
            return new TenantClient(baseAddress);
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

        /// <inheritdoc />
        public async Task<Tenant> GetAsync(object id)
        {
            return await this.GetRequestAsync<Tenant>(string.Format("tenant/{0}", id));
        }

        /// <inheritdoc />
        public async Task<IList<Tenant>> ListAllAsync()
        {
            return await this.GetRequestAsync<IList<Tenant>>("tenants");
        }

        /// <inheritdoc />
        public async Task<Client> GetClientByTenantIdAsync(object id)
        {
            return await this.GetRequestAsync<Client>(string.Format("tenant/{0}/client", id));
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
            if (!response.IsSuccessStatusCode)
            {
                return default(T);
            }
            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
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
            if (!response.IsSuccessStatusCode)
            {
                return default(T);
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        #endregion
    }
}