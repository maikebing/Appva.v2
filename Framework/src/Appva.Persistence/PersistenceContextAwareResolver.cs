// <copyright file="PersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using Logging;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// Abstract base <see cref="IPersistenceContextAwareResolver"/> implementation.
    /// </summary>
    public abstract class PersistenceContextAwareResolver : IPersistenceContextAwareResolver
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="Datasource"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<PersistenceContextAwareResolver>();

        /// <summary>
        /// The <see cref="IDatasource"/> instance.
        /// </summary>
        private readonly IDatasource datasource;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContextAwareResolver"/> class.
        /// </summary>
        /// <param name="datasource">The <see cref="IDatasource"/></param>
        protected PersistenceContextAwareResolver(IDatasource datasource)
        {
            Requires.NotNull(datasource, "datasource");
            this.datasource = datasource;
            this.datasource.Connect();
        }

        #endregion

        #region IPersistenceContextProvider Members.

        /// <inheritdoc />
        public IDatasource Datasource
        {
            get
            {
                return this.datasource;
            }
        }

        /// <inheritdoc />
        public abstract ISessionFactory Resolve();

        /// <inheritdoc />
        public virtual IPersistenceContext CreateNew()
        {
            Log.Debug("Created new <IPersistenceContext> from abstract base <PersistenceContextAwareResolver>");
            return new PersistenceContext(this);
        }

        #endregion
    }
}