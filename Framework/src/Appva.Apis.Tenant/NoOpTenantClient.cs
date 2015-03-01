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
        private T GetRequest<T>(string format, params object[] parameters)
        {
            return this.GetRequestAsync<T>(format, parameters).Result;
        }

        /// <summary>
        /// Mocks an HTTP async request and returns null.
        /// </summary>
        /// <typeparam name="T">The contract type to deserialize</typeparam>
        /// <param name="format">The operation uri or uri format</param>
        /// <param name="parameters">Optional format parameters</param>
        /// <returns>An instance of Task{null}</returns>
        private async Task<T> GetRequestAsync<T>(string format, params object[] parameters)
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