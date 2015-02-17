// <copyright file="IPersistenceContext.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// Persistence context interface. An NHibernate <see cref="ISession"/> wrapper.
    /// </summary>
    public interface IPersistenceContext : IDisposable
    {
        /// <summary>
        /// Returns the underlying NHibernate <see cref="ISession"/>.
        /// </summary>
        ISession Session
        {
            get;
        }

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
}