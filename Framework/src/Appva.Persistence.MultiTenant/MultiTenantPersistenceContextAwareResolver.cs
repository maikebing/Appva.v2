// <copyright file="MultiTenantPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Persistence
{
    #region Imports.

    using System;
    using Appva.Logging;
    using Appva.Persistence;
    using Appva.Persistence.MultiTenant;
    using Appva.Tenant.Identity;
    using NHibernate;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MultiTenantPersistenceContextAwareResolver : PersistenceContextAwareResolver
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
                if (Log.IsDebugEnabled())
                {
                    Log.DebugFormat("Attemping to resolve <ISessionFactory> for <ITenantIdentifier> {0}", identifier.Value);
                }
                var datasource = this.Datasource as IMultiTenantDatasource;
                return datasource.Lookup(identifier.Value);
            }
            Log.Error("Unable to resolve <ITenantIdentifier>");
            throw new Exception("Unable to resolve tenant identifer");
        }

        #endregion
    }
}