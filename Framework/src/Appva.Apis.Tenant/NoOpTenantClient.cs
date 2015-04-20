// <copyright file="NoOpTenantClient.cs" company="Appva AB">
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
    using System.Threading.Tasks;
    using Appva.Apis.TenantServer.Configuration;
    using Appva.Tenant.Interoperability.Client;
    using Contracts;
    using Logging;
    using Resources;

    #endregion

    /// <summary>
    /// A no op <see cref="ITenantClient"/> implementation.
    /// </summary>
    public sealed class NoOpTenantClient : ITenantClient
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="NoOpTenantClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<NoOpTenantClient>();

        /// <summary>
        /// The Http tenant server base address.
        /// </summary>
        private readonly Uri baseAddress;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpTenantClient"/> class.
        /// </summary>
        public NoOpTenantClient()
            : this(ConfigurationManager.AppSettings.Get(Constants.AppSettingsKey))
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpTenantClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        private NoOpTenantClient(string baseAddress) : this(new Uri(baseAddress))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpTenantClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        private NoOpTenantClient(Uri baseAddress)
        {
            this.baseAddress = baseAddress;
            Log.DebugFormat(Debug.Messages.ClassInitialization, this.baseAddress);
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new tenant server no operation client.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        /// <returns>A new <see cref="ITenantClient"/> instance</returns>
        public static ITenantClient CreateNew(Uri baseAddress)
        {
            return new NoOpTenantClient(baseAddress);
        }

        /// <summary>
        /// Creates a new tenant server no operation client.
        /// </summary>
        /// <param name="baseAddress">The tenant server base uri address</param>
        /// <returns>A new <see cref="ITenantClient"/> instance</returns>
        public static ITenantClient CreateNew(string baseAddress)
        {
            return new NoOpTenantClient(baseAddress);
        }

        #endregion

        #region ITenantClient Members.

        /// <inheritdoc />
        public ITenantDto Find(Guid id)
        {
            return this.CreateNewGetRequest<Tenant>(Operation.Tenant, id);
        }

        /// <inheritdoc />
        public ITenantDto FindByIdentifier(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<ITenantDto> List()
        {
            return this.CreateNewGetRequest<IList<ITenantDto>>(Operation.TenantList);
        }

        /// <inheritdoc />
        public IClientDto FindClientByTenantId(Guid id)
        {
            return this.CreateNewGetRequest<Client>(Operation.Client, id);
        }

        #endregion

        #region ITenantClientAsync Members.

        /// <inheritdoc />
        public async Task<ITenantDto> FindAsync(Guid id)
        {
            return await this.CreateNewGetRequestAsync<Tenant>(Operation.Tenant, id);
        }

        /// <inheritdoc />
        public Task<ITenantDto> FindByIdentifierAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IList<ITenantDto>> ListAsync()
        {
            return await this.CreateNewGetRequestAsync<IList<ITenantDto>>(Operation.TenantList);
        }

        /// <inheritdoc />
        public async Task<IClientDto> FindClientByTenantIdAsync(Guid id)
        {
            return await this.CreateNewGetRequestAsync<Client>(Operation.Client, id);
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            //// No op.
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Mocks an HTTP request and returns null.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="format">The operation uri or uri format</param>
        /// <param name="parameters">Optional format parameters</param>
        /// <returns>Null</returns>
        private T CreateNewGetRequest<T>(string format, params object[] parameters)
        {
            return this.CreateNewGetRequestAsync<T>(format, parameters).Result;
        }

        /// <summary>
        /// Mocks an HTTP async request and returns null.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="format">The operation uri or uri format</param>
        /// <param name="parameters">Optional format parameters</param>
        /// <returns>An instance of Task{null}</returns>
        private async Task<T> CreateNewGetRequestAsync<T>(string format, params object[] parameters)
        {
            var uri = string.Format(format, parameters);
            if (Log.IsDebugEnabled())
            {
                Log.DebugFormat(Debug.Messages.HttpRequestMessage, this.baseAddress + uri);
            }
            Log.DebugFormat(Debug.Messages.HttpResponseMessage, string.Empty);
            return await Task.FromResult(default(T));
        }

        #endregion
    }
}