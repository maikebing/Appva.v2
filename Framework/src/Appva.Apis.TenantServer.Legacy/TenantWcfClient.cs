// <copyright file="TenantWcfClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer.Legacy
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Appva.Apis.TenantServer.Contracts;
    using Appva.Apis.TenantServer.Legacy.Wcf;
    using Appva.Logging;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// The tenant server wcf client <see cref="ITenantClient"/> implementation.
    /// </summary>
    /// <example>
    /// The preferred usage is to use the client as a singleton and not dispose after 
    /// each call. 
    /// <code language="cs" title="Not Preferred Example #1">
    ///     using (var client = TenantWcfClient.CreateNew())
    ///     {
    ///         client.Get("7893b0f0-4680-4440-a348-380da187e1e8");
    ///     }
    /// </code>
    /// <code language="cs" title="Not Preferred Example #2">
    ///     using (var client = new TenantWcfClient())
    ///     {
    ///         client.Get("7893b0f0-4680-4440-a348-380da187e1e8");
    ///     }
    /// </code>
    /// <code language="cs" title="Preferred Example with IoC #1">
    ///     var builder = new ContainerBuilder();
    ///     var client = TenantWcfClient.CreateNew();
    ///     builder.Register(x => client).As{ITenantClient}().SingleInstance();
    /// </code>
    /// <code language="cs" title="Preferred Example with IoC #2">
    ///     var builder = new ContainerBuilder();
    ///     builder.RegisterType{TenantWcfClient}().As{ITenantClient}().SingleInstance();
    /// </code>
    /// </example>
    public sealed class TenantWcfClient : ITenantClient
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="TenantClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantWcfClient>();

        /// <summary>
        /// The tenant wcf service client.
        /// </summary>
        private readonly ITenantService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantWcfClient"/> class.
        /// </summary>
        public TenantWcfClient()
        {
            if (Log.IsDebugEnabled())
            {
                Log.Debug(Debug.Messages.ClassInitialization);
            }
            this.service = new TenantServiceClient();
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new tenant wcf server client.
        /// </summary>
        /// <returns>A <see cref="ITenantClient"/></returns>
        public static ITenantClient CreateNew()
        {
            return new TenantWcfClient();
        }

        #endregion

        #region ITenantClient Members.

        /// <inheritdoc />
        public Tenant Get(object id)
        {
            var context = id as string;
            var response = this.service.FindByContext(new FindByContextRequest(context));
            var result = response.FindByContextResult;
            this.DebugResult(result);
            return this.FromDtoToTenant(result);
        }

        /// <inheritdoc />
        public async Task<Tenant> GetAsync(object id)
        {
            var context = id as string;
            var response = await this.service.FindByContextAsync(new FindByContextRequest(context)).ConfigureAwait(false);
            var result = response.FindByContextResult;
            this.DebugResult(result);
            return this.FromDtoToTenant(result);
        }

        /// <inheritdoc />
        public IList<Tenant> ListAll()
        {
            var response = this.service.Load(new LoadRequest());
            var result = response.LoadResult;
            this.DebugResult(result);
            return this.FromDtoListToTenantList(result);
        }

        /// <inheritdoc />
        public async Task<IList<Tenant>> ListAllAsync()
        {
            var response = await this.service.LoadAsync(new LoadRequest()).ConfigureAwait(false);
            var result = response.LoadResult;
            this.DebugResult(result);
            return this.FromDtoListToTenantList(result);
        }

        /// <inheritdoc />
        public Client GetClientByTenantId(object id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<Client> GetClientByTenantIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            //// No ops!
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: move json serialization to Log.
        /// </summary>
        /// <param name="result">The result to be logged</param>
        private void DebugResult(object result)
        {
            if (Log.IsDebugEnabled())
            {
                Log.Debug(JsonConvert.SerializeObject(result));
            }
        }

        /// <summary>
        /// Maps a collection of <see cref="TenantDto"/> to its equivalent part.
        /// </summary>
        /// <param name="dtos">The collection to be mapped</param>
        /// <returns>A collection of <see cref="Tenant"/></returns>
        private IList<Tenant> FromDtoListToTenantList(IList<TenantDto> dtos)
        {
            return dtos == null ? new List<Tenant>() : dtos.Select(x => this.FromDtoToTenant(x)).ToList();
        }

        /// <summary>
        /// Maps a single <see cref="TenantDto"/> to its equivalent part.
        /// </summary>
        /// <param name="dto">The entry to be mapped</param>
        /// <returns>A <see cref="Tenant"/> or null</returns>
        private Tenant FromDtoToTenant(TenantDto dto)
        {
            return dto == null ? null : new Tenant
                {
                    Id = dto.Identifier,
                    Identifier = dto.Identifier,
                    ConnectionString = this.FromDtoConnectionToConnectionString(dto),
                    Name = dto.Name,
                    HostName = dto.Hostname
                };
        }

        /// <summary>
        /// Creates a connection string from the <see cref="TenantDto"/>.
        /// </summary>
        /// <param name="dto">The entry to extract the connection string from</param>
        /// <returns>A connection string</returns>
        private string FromDtoConnectionToConnectionString(TenantDto dto)
        {
            if (dto.Connection == null)
            {
                return string.Empty;
            }
            if (! string.IsNullOrEmpty(dto.Connection.FailOverPartner))
            {
                dto.Connection.FailOverPartner = string.Format(
                    "Failover Partner={0};", 
                    dto.Connection.FailOverPartner);
            }
            return string.Format(
                    @"Server={0};{1}Database={2};Trusted_Connection={3};User ID={4};Password={5}",
                    dto.Connection.Server,
                    dto.Connection.FailOverPartner,
                    dto.Connection.Database,
                    dto.Connection.TrustedConnection,
                    dto.Connection.User,
                    dto.Connection.Password);
        }

        #endregion
    }
}