// <copyright file="PersistenceContext.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence
{
    #region Imports.

    using System;
    using System.Data;
    using System.Linq.Expressions;
    using NHibernate;
    using NHibernate.Context;
    using Validation;

    #endregion

    /// <summary>
    /// Persistence context interface. An NHibernate <see cref="ISession"/> wrapper.
    /// </summary>
    public interface IPersistenceContext : IDisposable
    {
        /// <summary>
        /// Returns the underlying NHibernate <see cref="ISession"/>.
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Creates a database connection and open an <see cref="ISession"/> on it.
        /// </summary>
        /// <returns>The <see cref="IPersistenceContext"/></returns>
        IPersistenceContext Open();

        /// <summary>
        /// Binds the <see cref="ISession"/> to the current context.
        /// </summary>
        /// <returns>The <see cref="IPersistenceContext"/></returns>
        IPersistenceContext Bind();

        /// <summary>
        /// Unbinds the <see cref="ISessionFactory"/> and resets the <see cref="ISession"/>
        /// for the <see cref="IPersistenceContext"/> to be commited and closed.
        /// </summary>
        /// <returns>The <see cref="IPersistenceContext"/></returns>
        IPersistenceContext Unbind();

        /// <summary>
        /// Begins a unit of work.
        /// </summary>
        /// <returns>The <see cref="IPersistenceContext"/></returns>
        IPersistenceContext BeginTransaction();

        /// <summary>
        /// Begins a unit of work.
        /// </summary>
        /// <param name="isolationLevel">Transaction locking behavior for the connection</param>
        /// <returns>The <see cref="IPersistenceContext"/></returns>
        IPersistenceContext BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Flushes the associated <see cref="ISession"/> and end the unit of work.
        /// </summary>
        /// <param name="commit">Whether or not to commit</param>
        void Commit(bool commit = true);

        /// <summary>
        /// Returns the entity {T} by id.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The id</param>
        /// <returns>The {T} by id or null</returns>
        T Get<T>(object id) where T : class;

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The generated id as an object</returns>
        object Save<T>(T entity) where T : class;

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity</param>
        void Update<T>(T entity) where T : class;

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity</param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Returns a LINQ <see cref="IQueryOver"/>.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="alias">The expression</param>
        /// <returns>An <see cref="IQueryOver{T,T}"/></returns>
        IQueryOver<T, T> QueryOver<T>(Expression<Func<T>> alias) where T : class;

        /// <summary>
        /// Returns a LINQ <see cref="IQueryOver"/>.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>An <see cref="IQueryOver{T,T}"/></returns>
        IQueryOver<T, T> QueryOver<T>() where T : class;
    }

    /// <summary>
    /// Implementation of <see cref="IPersistenceContext"/>.
    /// </summary>
    internal class PersistenceContext : IPersistenceContext
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContextFactory"/>. Manages and resolves  
        /// NHibernate ISessionFactories.
        /// </summary>
        private readonly IPersistenceContextFactory persistenceFactory;

        /// <summary>
        /// The underlying NHibernate <see cref="ISession"/>.
        /// </summary>
        private ISession session;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContext"/> class.
        /// </summary>
        /// <param name="persistenceFactory">The <see cref="IPersistenceContextFactory"/></param>
        public PersistenceContext(IPersistenceContextFactory persistenceFactory) 
        {
            this.persistenceFactory = persistenceFactory;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the underlying NHibernate <see cref="ISession"/>.
        /// </summary>
        public ISession Session
        {
            get
            {
                return this.session;
            }
        }

        #endregion

        #region IPersistenceContext Members.

        /// <inheritdoc />
        public IPersistenceContext Open()
        {
            this.session = this.persistenceFactory.SessionFactory.OpenSession();
            return this;
        }

        /// <inheritdoc />
        public IPersistenceContext Bind()
        {
            Requires.ValidState(this.session != null, "The session must first be opened before bound!");
            if (! CurrentSessionContext.HasBind(this.persistenceFactory.SessionFactory))
            {
                CurrentSessionContext.Bind(this.session);
            }
            return this;
        }

        /// <inheritdoc />
        public IPersistenceContext Unbind()
        {
            Requires.ValidState(this.session != null, "The session can not be null!");
            CurrentSessionContext.Unbind(this.persistenceFactory.SessionFactory);
            return this;
        }

        /// <inheritdoc />
        public IPersistenceContext BeginTransaction()
        {
            this.session.BeginTransaction(IsolationLevel.Unspecified);
            return this;
        }

        /// <inheritdoc />
        public IPersistenceContext BeginTransaction(IsolationLevel isolationLevel)
        {
            this.session.BeginTransaction(isolationLevel);
            return this;
        }

        /// <inheritdoc />
        public void Commit(bool commit = true)
        {
            Requires.ValidState(this.session != null, "No open session!");
            Requires.ValidState(this.session.Transaction != null, "No transaction started!");
            using (this.session)
            using (var transaction = this.session.Transaction)
            {
                if (! transaction.IsActive || transaction.WasRolledBack)
                {
                    return;
                }
                if (commit)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
        }

        /// <inheritdoc />
        public T Get<T>(object id) where T : class
        {
            return this.session.Get<T>(id);
        }

        /// <inheritdoc />
        public object Save<T>(T entity) where T : class
        {
            return this.session.Save(entity);
        }

        /// <inheritdoc />
        public void Update<T>(T entity) where T : class
        {
            this.session.Update(entity);
        }

        /// <inheritdoc />
        public void Delete<T>(T entity) where T : class
        {
            this.session.Delete(entity);
        }

        /// <inheritdoc />
        public IQueryOver<T, T> QueryOver<T>(Expression<Func<T>> alias) where T : class
        {
            return this.session.QueryOver<T>(alias);
        }

        /// <inheritdoc />
        public IQueryOver<T, T> QueryOver<T>() where T : class
        {
            return this.session.QueryOver<T>();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.session != null)
            {
                this.session.Dispose();
                this.session = null;
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}