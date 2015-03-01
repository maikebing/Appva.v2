// <copyright file="DefaultPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System.Diagnostics;
    using Logging;
    using NHibernate;

    #endregion

    /// <summary>
    /// Default single database source persistence context resolver implementation.
    /// </summary>
    public sealed class DefaultPersistenceContextAwareResolver : PersistenceContextAwareResolver
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="Datasource"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<DefaultPersistenceContextAwareResolver>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPersistenceContextAwareResolver"/> class.
        /// </summary>
        /// <param name="datasource">The <see cref="IDefaultDatasource"/></param>
        public DefaultPersistenceContextAwareResolver(IDefaultDatasource datasource)
            : base(datasource)
        {
        }

        #endregion

        #region PersistenceContextAwareResolver Overrides.

        /// <inheritdoc />
        public override ISessionFactory Resolve()
        {
            Log.Debug("Resolving <ISessionFactory> from <DefaultPersistenceContextAwareResolver>");
            var datasource = this.Datasource as IDefaultDatasource;
            Debug.Assert(datasource != null, "datasource != null");
            return datasource.SessionFactory;
        }

        #endregion
    }
}