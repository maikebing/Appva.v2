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
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
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
        /// The key <see cref="ISessionFactory"/> map instance.
        /// </summary>
        IDictionary<string, ISessionFactory> SessionFactories
        {
            get;
        }

        /// <summary>
        /// Looks up the value for the key provided in the <see cref="ISessionFactory"/>
        /// key value store.
        /// </summary>
        /// <param name="key">The key stored</param>
        /// <returns>An <see cref="ISessionFactory"/> instance if found, else null</returns>
        ISessionFactory Lookup(string key);
    }

    /// <summary>
    /// A <see cref="IMultiTenantDatasource"/> implementation.
    /// </summary>
    public sealed class MultiTenantDatasource : Datasource, IMultiTenantDatasource
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantClient"/> instance.
        /// </summary>
        private readonly ITenantClient client;

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/> instance.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        /// <summary>
        /// The <see cref="IMultiTenantDatasourceConfiguration"/> instance.
        /// </summary>
        private readonly IMultiTenantDatasourceConfiguration configuration;

        /// <summary>
        /// The <see cref="IDatasourceExceptionHandler"/> instance.
        /// </summary>
        private readonly IDatasourceExceptionHandler exceptionHandler;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantDatasource"/> class.
        /// </summary>
        /// <param name="client">The <see cref="ITenantClient"/></param>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="configuration">The <see cref="IMultiTenantDatasourceConfiguration"/></param>
        /// <param name="exceptionHandler">The <see cref="IDatasourceExceptionHandler"/></param>
        public MultiTenantDatasource(
            ITenantClient client,
            IRuntimeMemoryCache cache,
            IMultiTenantDatasourceConfiguration configuration,
            IDatasourceExceptionHandler exceptionHandler)
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
        public IDictionary<string, ISessionFactory> SessionFactories
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public ISessionFactory Lookup(string key)
        {
            var cacheKey = CacheTypes.Persistence.FormatWith(key);
            if (! this.cache.Contains(cacheKey))
            {
                try
                {
                    var tenant = this.client.FindByIdentifier(key);
                    if (tenant != null)
                    {
                        var factory = this.Build(new PersistenceUnit(tenant.ConnectionString, this.configuration.Assembly, this.configuration.Properties, tenant.Identifier));
                        this.cache.Add<ISessionFactory>(cacheKey, factory, new RuntimeEvictionPolicy
                        {
                            Priority = CacheItemPriority.NotRemovable
                        });
                    }
                }
                catch (Exception ex)
                {
                    this.exceptionHandler.Handle(ex);
                }
                return null;
            }
            return this.cache.Find<ISessionFactory>(cacheKey);
        }

        #endregion

        #region Datasource Implementation.

        /// <inheritdoc />
        public override void Connect()
        {
            try
            {
                var tenants = this.client.List();
                Requires.ValidState(tenants.Count > 0, "No Tenants found!");
                var units = tenants.Select(x => new PersistenceUnit(x.ConnectionString, this.configuration.Assembly, this.configuration.Properties, x.Identifier)).ToList();
                var factories = this.Build(units);
                foreach (var factory in factories)
                {
                    var cacheKey = CacheTypes.Persistence.FormatWith(factory.Key);
                    this.cache.Upsert<ISessionFactory>(cacheKey, factory.Value, new RuntimeEvictionPolicy
                    {
                        Priority = CacheItemPriority.NotRemovable
                    });
                }
            } 
            catch (AggregateException ex)
            {
                this.exceptionHandler.Handle(ex);
            }
        }

        #endregion
    }
}