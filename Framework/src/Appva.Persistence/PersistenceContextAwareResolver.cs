// <copyright file="PersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using Core.Logging;
    using NHibernate;
    using Validation;

    #endregion

    /// <summary>
    /// Persistence context (<see cref="ISession"/>) provider and resolver interface. 
    /// </summary>
    public interface IPersistenceContextAwareResolver
    {
        /// <summary>
        /// Returns the current <see cref="ISessionFactory"/>.
        /// </summary>
        /// <returns>An <see cref="ISessionFactory"/> instance</returns>
        ISessionFactory Resolve();

        /// <summary>
        /// Creates a new <see cref="IPersistenceContext"/> instance.
        /// </summary>
        /// <returns>A new <see cref="IPersistenceContext"/> instance</returns>
        IPersistenceContext CreateNew();
    }

    /// <summary>
    /// Abstract base <see cref="IPersistenceContextAwareResolver"/> implementation.
    /// </summary>
    /// <typeparam name="T">The data source type</typeparam>
    public abstract class PersistenceContextAwareResolver<T> : IPersistenceContextAwareResolver
        where T : IDatasource
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IDatasource"/> instance.
        /// </summary>
        private readonly T datasource;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContextAwareResolver"/> class.
        /// </summary>
        /// <param name="datasource">The <see cref="IDatasource"/></param>
        protected PersistenceContextAwareResolver(T datasource)
        {
            this.datasource = datasource;
            this.datasource.Connect();
        }

        #endregion

        #region IPersistenceContextProvider Members.

        /// <summary>
        /// Returns the <see cref="T"/>.
        /// </summary>
        public T Datasource
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
            return new PersistenceContext(this);
        }

        #endregion
    }
}