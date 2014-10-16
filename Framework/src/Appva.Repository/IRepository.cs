// <copyright file="IRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Repository
{
    #region Imports.

    using System.Threading.Tasks;
    using Persistence;

    #endregion

    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="T">A persistent entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Returns the entity {T} by primary id.
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>The {T}</returns>
        T Get(object id);

        /// <summary>
        /// Saves an entity {T}.
        /// </summary>
        /// <param name="entity">The entity to be saved</param>
        /// <returns>Returns the generated id as an object</returns>
        object Save(T entity);

        /// <summary>
        /// Updates an entity {T}.
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity {T}.
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        void Delete(T entity);

        /// <summary>
        /// Returns the entity {T} by primary id async.
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>The {T}</returns>
        Task<T> GetAsync(object id);

        /// <summary>
        /// Saves an entity {T} async.
        /// </summary>
        /// <param name="entity">The entity to be saved</param>
        /// <returns>Returns the generated id as an object</returns>
        Task<object> SaveAsync(T entity);

        /// <summary>
        /// Returns the current <see cref="IPersistenceContext"/>.
        /// </summary>
        /// <returns>A <see cref="IPersistenceContext"/></returns>
        IPersistenceContext PersistenceContext
        {
            get;
        }
    }
}