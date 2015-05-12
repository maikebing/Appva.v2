// <copyright file="MultiTenantPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System;
    using Core.Resources;
    using Core.Extensions;
    using Core.Logging;
    using Messages;
    using NHibernate;
    using Persistence;
    using Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MultiTenantPersistenceContextAwareResolver : PersistenceContextAwareResolver<IMultiTenantDatasource>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="MultiTenantPersistenceContextAwareResolver"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<MultiTenantPersistenceContextAwareResolver>();

        /// <summary>
        /// The <see cref="ITenantIdentificationStrategy"/>.
        /// </summary>
        private readonly ITenantIdentificationStrategy strategy;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantPersistenceContextAwareResolver"/> class.
        /// </summary>
        /// <param name="datasource">The <see cref="ITenantIdentificationStrategy"/></param>
        /// <param name="datasource">The <see cref="IMultiTenantDatasource"/></param>
        public MultiTenantPersistenceContextAwareResolver(ITenantIdentificationStrategy strategy, IMultiTenantDatasource datasource)
            : base(datasource)
        {
            this.strategy = strategy;
        }

        #endregion

        #region PersistenceContextAwareResolver Overrides.

        /// <inheritdoc />
        public override ISessionFactory Resolve()
        {
            ITenantIdentifier identifier = null;
            if (this.strategy.TryIdentifyTenant(out identifier))
            {
                Log.Debug(Debug.ResolveTenantIdentity, identifier.Value);
                return this.Datasource.Locate(CacheTypes.Persistence.FormatWith(identifier.Value));
            }
            throw new PersistenceContextAwareResolverException(Exceptions.FailedToResolveTenantIdentifier);
        }

        #endregion
    }
}