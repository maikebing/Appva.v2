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
    using Appva.Apis.TenantServer.Legacy.Transformers;
    using Appva.Apis.TenantServer.Legacy.Wcf;
    using Appva.Logging;
    using Appva.Tenant.Interoperability.Client;
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
        public ITenantDto Find(Guid id)
        {
            var result = this.service.Find(new FindRequest(id)).FindResult;
            return Transformer.Transform(result);
        }

        /// <inheritdoc />
        public ITenantDto FindByIdentifier(string id)
        {
            var result = this.service.FindByContext(new FindByContextRequest(id)).FindByContextResult;
            return Transformer.Transform(result);
        }

        /// <inheritdoc />
        public IList<ITenantDto> List()
        {
            var result = this.service.Load(new LoadRequest()).LoadResult;
            return Transformer.Transform(result);
        }

        /// <inheritdoc />
        public IClientDto FindClientByTenantId(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITenantClientAsync Members.

        /// <inheritdoc />
        public async Task<ITenantDto> FindAsync(Guid id)
        {
            var response = await this.service.FindAsync(new FindRequest(id)).ConfigureAwait(false);
            var result = response.FindResult;
            return Transformer.Transform(result);
        }

        /// <inheritdoc />
        public async Task<ITenantDto> FindByIdentifierAsync(string id)
        {
            var response = await this.service.FindByContextAsync(new FindByContextRequest(id)).ConfigureAwait(false);
            var result = response.FindByContextResult;
            return Transformer.Transform(result);
        }

        /// <inheritdoc />
        public async Task<IList<ITenantDto>> ListAsync()
        {
            var response = await this.service.LoadAsync(new LoadRequest()).ConfigureAwait(false);
            var result = response.LoadResult;
            return Transformer.Transform(result);
        }

        /// <inheritdoc />
        public Task<IClientDto> FindClientByTenantIdAsync(Guid id)
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
    }
}