// <copyright file="TenantIdentityPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Application.Persistence
{
    #region Imports.

    using System.Web;
    using Appva.Logging;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Persistence;
    using Appva.Persistence.MultiTenant;
    using NHibernate;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantIdentityPersistenceContextAwareResolver : PersistenceContextAwareResolver
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="Datasource"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantIdentityPersistenceContextAwareResolver>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdentityPersistenceContextAwareResolver"/> class.
        /// </summary>
        /// <param name="datasource">The <see cref="IMultiTenantDatasource"/></param>
        public TenantIdentityPersistenceContextAwareResolver(IMultiTenantDatasource datasource)
            : base(datasource)
        {
        }

        #endregion

        #region PersistenceContextAwareResolver Overrides.

        /// <inheritdoc />
        public override ISessionFactory Resolve()
        {
            var datasource = this.Datasource as IMultiTenantDatasource;
            var id = HttpContext.Current.User.Identity.Tenant();
            var sessionFactory = datasource.Lookup(id.ToString());
            Log.DebugFormat("Resolving <ISessionFactory> from <DefaultPersistenceContextAwareResolver> for tenant id {0}", id);
            return sessionFactory;
        }

        #endregion
    }
}