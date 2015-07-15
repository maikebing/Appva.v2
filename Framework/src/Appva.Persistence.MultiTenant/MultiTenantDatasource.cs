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
    using System.Linq;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Core.Extensions;
    using Appva.Core.Logging;
    using Appva.Core.Resources;
    using Appva.Persistence.MultiTenant.Messages;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using JetBrains.Annotations;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// A multi tenant <see cref="IDatasource"/>.
    /// </summary>
    public interface IMultiTenantDatasource : IDatasource
    {
        /// <summary>
        /// Looks up the tenant <see cref="ISessionFactory"/> by <see cref="ITenantIdentifier"/>.
        /// </summary>
        /// <param name="identifier">The tenant identifier</param>
        /// <returns>An <see cref="ISessionFactory"/> instance if found, else null</returns>
        ISessionFactory Locate(ITenantIdentifier identifier);
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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantDatasource"/> class.
        /// </summary>
        /// <param name="client">The <see cref="ITenantClient"/></param>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="configuration">The <see cref="IMultiTenantDatasourceConfiguration"/></param>
        public MultiTenantDatasource([NotNull] ITenantClient client, [NotNull] IRuntimeMemoryCache cache, [NotNull] IMultiTenantDatasourceConfiguration configuration)
        {
            Requires.NotNull(client, "client");
            Requires.NotNull(cache, "cache");
            Requires.NotNull(configuration, "configuration");
            this.client = client;
            this.cache = cache;
            this.configuration = configuration;
        }

        #endregion

        #region IMultiTenantDatasource Members.

        /// <inheritdoc />
        public ISessionFactory Locate(ITenantIdentifier identifier)
        {
            Log.Debug(Debug.LocateISessionFactoryForTenant, identifier);
            var cacheKey = CacheTypes.Persistence.FormatWith(identifier);
            if (! this.cache.Contains(cacheKey))
            {
                //// FIXME: Resource server uses ID as identifier but Admin 1.6 uses identifier.
                //// 1. If the client returns a tenant identity instead with a new call
                //// 2. OAuth server returns a client identity instead (this will effect all tickets!)
                //// In order for the multitenant to work on both web and resource then temporarily
                //// just check the config.
                var tenant = this.configuration.UseIdAsIdentifier ? 
                    this.client.Find(new Guid(identifier.Value)) : this.client.FindByIdentifier(identifier);
                if (tenant == null)
                {
                    throw new TenantNotFoundException(Exceptions.TenantNotFound.FormatWith(identifier));
                }
                this.cache.Upsert<ISessionFactory>(
                    cacheKey,
                    this.Build(PersistenceUnit.CreateNew(
                        this.configuration.UseIdAsIdentifier ? tenant.Id.ToString() : tenant.Identifier, 
                        tenant.ConnectionString, 
                        this.configuration.Assembly, 
                        this.configuration.Properties)),
                    RuntimeEvictionPolicy.NonRemovable);
            }
            return this.cache.Find<ISessionFactory>(cacheKey);
        }

        #endregion

        #region Datasource Implementation.

        /// <inheritdoc />
        public override IDatasourceResult Connect()
        {
            Log.Debug(Debug.DatasourceConnecting);
            var tenants = this.client.List();
            if (tenants.Count == 0)
            {
                throw new TenantsResultException(Exceptions.ZeroTenantsFound);
            }
            var units = tenants.Select(x => PersistenceUnit.CreateNew(
                    this.configuration.UseIdAsIdentifier ? x.Id.ToString() : x.Identifier, 
                    x.ConnectionString, 
                    this.configuration.Assembly, 
                    this.configuration.Properties))
                .ToList();
            var result = this.Build(units);
            foreach (var factory in result.SessionFactories)
            {
                this.cache.Upsert<ISessionFactory>(CacheTypes.Persistence.FormatWith(factory.Key), factory.Value, RuntimeEvictionPolicy.NonRemovable);
            }
            return result;
        }

        #endregion
    }
}