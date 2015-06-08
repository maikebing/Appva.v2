// <copyright file="DefaultPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using JetBrains.Annotations;
    using NHibernate;

    #endregion

    /// <summary>
    /// Default single database source persistence context resolver implementation.
    /// </summary>
    public sealed class DefaultPersistenceContextAwareResolver : PersistenceContextAwareResolver<IDefaultDatasource>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPersistenceContextAwareResolver"/> class.
        /// </summary>
        /// <param name="datasource">The <see cref="IDefaultDatasource"/></param>
        /// <param name="handler">The <see cref="IPersistenceExceptionHandler"/></param>
        public DefaultPersistenceContextAwareResolver([NotNull] IDefaultDatasource datasource, [NotNull] IPersistenceExceptionHandler handler)
            : base(datasource, handler)
        {
        }

        #endregion

        #region PersistenceContextAwareResolver Members.

        /// <inheritdoc />
        protected override ISessionFactory Resolve([NotNull] IDefaultDatasource datasource)
        {
            return datasource.SessionFactory;
        }

        #endregion
    }
}