// <copyright file="PersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System;
    using JetBrains.Annotations;
    using NHibernate;

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
        /// The <see cref="IDatasource"/>.
        /// </summary>
        private readonly T datasource;

        /// <summary>
        /// The <see cref="IPersistenceExceptionHandler"/>.
        /// </summary>
        private readonly IPersistenceExceptionHandler handler;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContextAwareResolver{T}" /> class.
        /// </summary>
        /// <param name="datasource">The <see cref="IDatasource"/></param>
        /// <param name="handler">The <see cref="IPersistenceExceptionHandler"/></param>
        protected PersistenceContextAwareResolver([NotNull] T datasource, [NotNull] IPersistenceExceptionHandler handler)
        {
            this.datasource = datasource;
            this.handler = handler;
            this.TryConnect();
        }

        #endregion

        #region IPersistenceContextProvider Members.

        /// <inheritdoc />
        public ISessionFactory Resolve()
        {
            try
            {
                return this.Resolve(this.datasource);
            }
            catch (Exception ex)
            {
                this.handler.Handle(ex);
            }
            return null;
        }

        /// <inheritdoc />
        public virtual IPersistenceContext CreateNew()
        {
            return new PersistenceContext(this);
        }

        #endregion

        #region Protected Abstract Methods.

        /// <summary>
        /// Resolves the <see cref="ISessionFactory"/>.
        /// </summary>
        /// <param name="datasource">The {T} data source</param>
        /// <returns>A new instance of <see cref="ISessionFactory"/></returns>
        protected abstract ISessionFactory Resolve(T datasource);

        #endregion

        #region Private Methods.

        /// <summary>
        /// Attempts to connect to the data source and dispatch any exceptions
        /// to the exception handler.
        /// </summary>
        private void TryConnect()
        {
            try
            {
                var result = this.datasource.Connect();
                if (result == null || result.Exceptions == null || result.Exceptions.Count == 0)
                {
                    return;
                }
                foreach (var exception in result.Exceptions)
                {
                    this.handler.Handle(exception);
                }
            }
            catch (Exception exception)
            {
                this.handler.Handle(exception);
            }
        }

        #endregion
    }
}