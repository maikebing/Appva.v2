// <copyright file="MultiTenantPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using Core.Extensions;
    using Core.Logging;
    using Core.Resources;
    using JetBrains.Annotations;
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
        /// <param name="strategy">The <see cref="ITenantIdentificationStrategy"/></param>
        /// <param name="datasource">The <see cref="IMultiTenantDatasource"/></param>
        /// <param name="handler">The <see cref="IPersistenceExceptionHandler"/></param>
        public MultiTenantPersistenceContextAwareResolver([NotNull] ITenantIdentificationStrategy strategy, [NotNull] IMultiTenantDatasource datasource, IPersistenceExceptionHandler handler)
            : base(datasource, handler)
        {
            this.strategy = strategy;
        }

        #endregion

        #region PersistenceContextAwareResolver Members.

        /// <inheritdoc />
        protected override ISessionFactory Resolve([NotNull] IMultiTenantDatasource datasource)
        {
            ITenantIdentifier identifier = null;
            if (this.strategy.TryIdentifyTenant(out identifier))
            {
                Log.Debug(Debug.ResolveTenantIdentity, identifier);
                return datasource.Locate(identifier);
            }
            throw new PersistenceContextAwareResolverException(Exceptions.FailedToResolveTenantIdentifier);
        }

        #endregion
    }
}