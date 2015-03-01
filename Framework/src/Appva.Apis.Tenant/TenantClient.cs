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
    using System.Configuration;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Contracts;
    using Logging;
    using Newtonsoft.Json;
    using Resources;

    #endregion

    /// <summary>
    /// The tenant server client <see cref="ITenantClient"/> implementation.
    /// </summary>
    /// <example>
    /// The preferred usage is to use the client as a singleton and not dispose after 
    /// each call. 
    /// <code language="cs" title="Not Preferred Example #1">
    ///     using (var client = TenantClient.CreateNew("https://example.com"))
    ///     {
    ///         client.Get("7893b0f0-4680-4440-a348-380da187e1e8");
    ///     }
    /// </code>
    /// <code language="cs" title="Not Preferred Example #2">
    ///     using (var client = new TenantClient())
    ///     {
    ///         client.Get("7893b0f0-4680-4440-a348-380da187e1e8");
    ///     }
    /// </code>
    /// <code language="cs" title="Preferred Example with IoC #1">
    ///     var builder = new ContainerBuilder();
    ///     var client = TenantClient.CreateNew("https://example.com");
    ///     builder.Register(x => client).As{ITenantClient}().SingleInstance();
    /// </code>
    /// <code language="cs" title="Preferred Example with IoC #2">
    ///     var builder = new ContainerBuilder();
    ///     builder.RegisterType{TenantClient}().As{ITenantClient}().SingleInstance();
    /// </code>
    /// </example>
    public sealed class TenantClient : ITenantClient
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="TenantClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantClient>();

        /// <summary>
        /// The re-usable <see cref="HttpClient"/> instance.
        /// </summary>
        private readonly HttpClient httpClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantClient"/> class.
        /// </summary>
        public TenantClient()
            : this(ConfigurationManager.AppSettings.Get(Constants.AppSettingsKey))
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        private TenantClient(string baseAddress) : this(new Uri(baseAddress))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        private TenantClient(Uri baseAddress)
        {
            Log.DebugFormat(Debug.Messages.ClassInitialization, baseAddress);
            this.httpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.MediaType)); 
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new tenant server client.
        /// </summary>
        /// <returns>A new <see cref="ITenantClient"/> instance</returns>
        public static ITenantClient CreateNew()
        {
            return new TenantClient();
        }

        /// <summary>
        /// Creates a new tenant server client.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        /// <returns>A new <see cref="ITenantClient"/> instance</returns>
        public static ITenantClient CreateNew(Uri baseAddress)
        {
            return new TenantClient(baseAddress);
        }

        /// <summary>
        /// Creates a new tenant server client.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        /// <returns>A new <see cref="ITenantClient"/> instance</returns>
        public static ITenantClient CreateNew(string baseAddress)
        {
            return new TenantClient(baseAddress);
        }

        #endregion

        #region ITenantService Members.

        /// <inheritdoc />
        public Tenant Get(object id)
        {
            return this.GetRequest<Tenant>(Operation.Tenant, id);
        }

        /// <inheritdoc />
        public IList<Tenant> ListAll()
        {
            return this.GetRequest<IList<Tenant>>(Operation.TenantList);
        }

        /// <inheritdoc />
        public Client GetClientByTenantId(object id)
        {
            return this.GetRequest<Client>(Operation.Client, id);
        }

        /// <inheritdoc />
        public async Task<Tenant> GetAsync(object id)
        {
            return await this.GetRequestAsync<Tenant>(Operation.Tenant, id);
        }

        /// <inheritdoc />
        public async Task<IList<Tenant>> ListAllAsync()
        {
            return await this.GetRequestAsync<IList<Tenant>>(Operation.TenantList);
        }

        /// <inheritdoc />
        public async Task<Client> GetClientByTenantIdAsync(object id)
        {
            return await this.GetRequestAsync<Client>(Operation.Client, id);
        }

        #endregion

        #region IDisposable Members.

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.httpClient != null)
            {
                this.httpClient.Dispose();
            }
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Creates a syncronous HTTP request and deserialize the response to the {T} type.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="format">The operation uri or uri format</param>
        /// <param name="parameters">Optional format parameters</param>
        /// <returns>An instance of {T}</returns>
        private T GetRequest<T>(string format, params object[] parameters)
        {
            return this.GetRequestAsync<T>(format, parameters).Result;
        }

        /// <summary>
        /// Creates a asyncronous HTTP request and deserialize the response to the Task{T} 
        /// type.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="format">The operation uri or uri format</param>
        /// <param name="parameters">Optional format parameters</param>
        /// <returns>An instance of Task{T}</returns>
        private async Task<T> GetRequestAsync<T>(string format, params object[] parameters)
        {
            var uri = string.Format(format, parameters);
            if (Log.IsDebugEnabled())
            {
                Log.DebugFormat(Debug.Messages.HttpRequestMessage, this.httpClient.BaseAddress + uri);
            }
            var response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);
            if (! response.IsSuccessStatusCode)
            {
                return default(T);
            }
            var data = await response.Content.ReadAsStringAsync();
            Log.DebugFormat(Debug.Messages.HttpResponseMessage, data);
            return JsonConvert.DeserializeObject<T>(data);
        }

        #endregion
    }
}
