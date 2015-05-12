// <copyright file="MultiTenantDatasource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Core.Exceptions;
    using Appva.Core.Extensions;
    using Appva.Core.Logging;
    using Appva.Core.Resources;
    using Appva.Persistence.MultiTenant.Messages;
    using Appva.Tenant.Interoperability.Client;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// A multi tenant <see cref="IDatasource"/>.
    /// </summary>
    public interface IMultiTenantDatasource : IDatasource
    {
        /// <summary>
        /// Looks up the value for the key provided in the <see cref="ISessionFactory"/>
        /// key value store.
        /// </summary>
        /// <param name="key">The key stored</param>
        /// <returns>An <see cref="ISessionFactory"/> instance if found, else null</returns>
        ISessionFactory Locate(string key);
    }

    /// <summary>
    /// A <see cref="IMultiTenantDatasource"/> implementation.
    /// </summary>
    public sealed class MultiTenantDatasource : Datasource, IMultiTenantDatasource
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="MultiTenantDatasource"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<MultiTenantDatasource>();

        /// <summary>
        /// The <see cref="ITenantClient"/>.
        /// </summary>
        private readonly ITenantClient client;

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        /// <summary>
        /// The <see cref="IMultiTenantDatasourceConfiguration"/>.
        /// </summary>
        private readonly IMultiTenantDatasourceConfiguration configuration;

        /// <summary>
        /// The <see cref="IExceptionHandler"/>.
        /// </summary>
        private readonly IExceptionHandler exceptionHandler;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantDatasource"/> class.
        /// </summary>
        /// <param name="client">The <see cref="ITenantClient"/></param>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="configuration">The <see cref="IMultiTenantDatasourceConfiguration"/></param>
        /// <param name="exceptionHandler">The <see cref="IExceptionHandler"/></param>
        public MultiTenantDatasource(
            ITenantClient client,
            IRuntimeMemoryCache cache,
            IMultiTenantDatasourceConfiguration configuration,
            IExceptionHandler exceptionHandler)
        {
            Requires.NotNull(client, "client");
            Requires.NotNull(cache, "cache");
            Requires.NotNull(configuration, "configuration");
            Requires.NotNull(exceptionHandler, "exceptionHandler");
            this.client = client;
            this.cache = cache;
            this.configuration = configuration;
            this.exceptionHandler = exceptionHandler;
        }

        #endregion

        #region IMultiTenantDatasource Members.

        /// <inheritdoc />
        /// <remarks>The key is a cache key, e.g. {cache}.{tenantId}</remarks>
        public ISessionFactory Locate(string key)
        {
            Log.Debug(Debug.LocateISessionFactoryForTenant, key);
            if (! this.cache.Contains(key))
            {
                try
                {
                    var tenant = this.client.FindByIdentifier(key);
                    if (tenant.IsNotNull())
                    {
                        this.cache.Upsert<ISessionFactory>(
                            key,
                            this.Build(PersistenceUnit.CreateNew(
                                tenant.Identifier, 
                                tenant.ConnectionString, 
                                this.configuration.Assembly, 
                                this.configuration.Properties)), 
                            RuntimeEvictionPolicy.NonRemovable);
                    }
                }
                catch (Exception ex)
                {
                    this.exceptionHandler.Handle(ex);
                }
            }
            return this.cache.Find<ISessionFactory>(key);
        }

        #endregion

        #region Datasource Implementation.

        /// <inheritdoc />
        public override void Connect()
        {
            Log.Debug(Debug.DatasourceConnecting);
            var tenants = this.client.List();
            if (tenants.Count == 0)
            {
                this.exceptionHandler.Handle(new ZeroTenantsException(Exceptions.ZeroTenantsFound));
            }
            try
            {
                var units = tenants.Select(x => PersistenceUnit.CreateNew(x.Identifier, x.ConnectionString, this.configuration.Assembly, this.configuration.Properties)).ToList();
                var result = this.Build(units);
                foreach (var factory in result.SessionFactories)
                {
                    this.cache.Upsert<ISessionFactory>(CacheTypes.Persistence.FormatWith(factory.Key), factory.Value, RuntimeEvictionPolicy.NonRemovable);
                }
                if (result.Exceptions.Count > 0)
                {
                    this.exceptionHandler.Handle(new AggregateException(result.Exceptions));
                }
            }
            catch (Exception ex)
            {
                this.exceptionHandler.Handle(ex);
            }
        }

        #endregion
    }
}