﻿// <copyright file="TenantClient.cs" company="Appva AB">
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
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Appva.Tenant.Identity;
    using Configuration;
    using Core.Logging;
    using Newtonsoft.Json;
    using Resources;
    using Tenant.Interoperability.Client;

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
            Log.Debug(Debug.Messages.ClassInitialization, baseAddress);
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

        #region ITenantClient Members.

        /// <inheritdoc />
        public ITenantDto Find(Guid id)
        {
            return this.CreateNewRequest<Contracts.Tenant>(Operation.Tenant, id);
        }

        /// <inheritdoc />
        /// <remarks>Currently not supported</remarks>
        public ITenantDto FindByIdentifier(ITenantIdentifier id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<ITenantDto> List()
        {
            var tenants = this.CreateNewRequest<IList<Contracts.Tenant>>(Operation.TenantList);
            return tenants.Cast<ITenantDto>().ToList();
        }

        /// <inheritdoc />
        public IClientDto FindClientByTenantId(Guid id)
        {
            return this.CreateNewRequest<IClientDto>(Operation.Client, id);
        }

        #endregion

        #region ITenantClientAsync Members.

        /// <inheritdoc />
        public async Task<ITenantDto> FindAsync(Guid id)
        {
            return await this.CreateNewRequestAsync<Contracts.Tenant>(Operation.Tenant, id);
        }

        /// <inheritdoc />
        /// <remarks>Currently not supported</remarks>
        public Task<ITenantDto> FindByIdentifierAsync(ITenantIdentifier id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IList<ITenantDto>> ListAsync()
        {
            var tenants = await this.CreateNewRequestAsync<IList<Contracts.Tenant>>(Operation.TenantList);
            return tenants.Cast<ITenantDto>().ToList();
        }

        /// <inheritdoc />
        public async Task<IClientDto> FindClientByTenantIdAsync(Guid id)
        {
            return await this.CreateNewRequestAsync<Contracts.Client>(Operation.Client, id);
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
        private T CreateNewRequest<T>(string format, params object[] parameters)
        {
            return this.CreateNewRequestAsync<T>(format, parameters).Result;
        }

        /// <summary>
        /// Creates a asyncronous HTTP request and deserialize the response to the Task{T} 
        /// type.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="format">The operation uri or uri format</param>
        /// <param name="parameters">Optional format parameters</param>
        /// <returns>An instance of Task{T}</returns>
        private async Task<T> CreateNewRequestAsync<T>(string format, params object[] parameters)
        {
            var uri = string.Format(format, parameters);
            Log.Debug(Debug.Messages.HttpRequestMessage, this.httpClient.BaseAddress + uri);
            var response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);
            if (! response.IsSuccessStatusCode)
            {
                return default(T);
            }
            var data = await response.Content.ReadAsStringAsync();
            Log.Debug(Debug.Messages.HttpResponseMessage, data);
            return JsonConvert.DeserializeObject<T>(data);
        }

        #endregion
    }
}
