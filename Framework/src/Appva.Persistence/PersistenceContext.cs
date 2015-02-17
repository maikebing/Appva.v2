// <copyright file="PersistenceContext.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
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
    /// Implementation of <see cref="IPersistenceContext"/>.
    /// </summary>
    internal class PersistenceContext : IPersistenceContext
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContextFactory"/>. Manages and resolves  
        /// NHibernate ISessionFactories.
        /// </summary>
        private readonly IPersistenceContextAwareResolver contextAwareResolver;

        /// <summary>
        /// The underlying NHibernate <see cref="ISession"/>.
        /// </summary>
        private ISession session;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceContext"/> class.
        /// </summary>
        /// <param name="contextAwareResolver">The <see cref="IPersistenceContextAwareResolver"/></param>
        public PersistenceContext(IPersistenceContextAwareResolver contextAwareResolver) 
        {
            this.contextAwareResolver = contextAwareResolver;
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
            this.session = this.contextAwareResolver.Resolve().OpenSession();
            return this;
        }

        /// <inheritdoc />
        public IPersistenceContext Bind()
        {
            Requires.ValidState(this.session != null, "The session must first be opened before bound!");
            if (! CurrentSessionContext.HasBind(this.contextAwareResolver.Resolve()))
            {
                CurrentSessionContext.Bind(this.session);
            }
            return this;
        }

        /// <inheritdoc />
        public IPersistenceContext Unbind()
        {
            Requires.ValidState(this.session != null, "The session can not be null!");
            CurrentSessionContext.Unbind(this.contextAwareResolver.Resolve());
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