// <copyright file="Repository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The aggregate root type.</typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot, IEntity
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IPersistenceContext"/>.
        /// </param>
        public Repository(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the <see cref="IPersistenceContext"/>.
        /// </summary>
        protected IPersistenceContext Context
        {
            get
            {
                return this.context;
            }
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public T Get(object id)
        {
            return this.context.Get<T>(id);
        }

        /// <inheritdoc />
        public T Load(object id)
        {
            return this.context.Session.Load<T>(id);
        }

        /// <summary>
        /// Returns a simple paged collection of {T}.
        /// </summary>
        /// <param name="pageQuery">The page query parameters.</param>
        /// <returns>A <see cref="IPaged{T}"/>.</returns>
        public IPaged<T> Paged(IPageQuery pageQuery)
        {
            var query = this.Context.QueryOver<T>().Skip(pageQuery.Skip).Take(pageQuery.PageSize).Future<T>();
            var count = this.Context.QueryOver<T>().Select(Projections.Count(Projections.Id())).FutureValue<int>().Value;
            var items = query.ToList();
            return Paged<T>.New(pageQuery, items, count);
        }

        /// <summary>
        /// Updates an entity to the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity)
        {
            entity.MarkAsUpdated();
            this.context.Update(entity);
            //// UNRESOLVED: This will be introduced as event listeners.
            //// this.StoreEvents(entity, entity.Version + 1);
        }

        /// <summary>
        /// Saves an entity to the database.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        public void Save(T entity)
        {
            if (! this.IsValid(entity))
            {
                return;
            }
            this.context.Save(entity);
            //// UNRESOLVED: This will be introduced as event listeners.
            //// this.StoreEvents (entity);
        }

        /// <inheritdoc />
        public void Save(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                this.Save(entity);
            }
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Returns whether or not the entity should be saved.
        /// </summary>
        /// <param name="entity">The entity to e saved.</param>
        /// <returns>True if the entity should be saved; false otherwise.</returns>
        /// <remarks>
        /// If returning false the entity will not be saved. This is a convenient method
        /// for simple checks, e.g. if a setting or permission already is saved we do 
        /// not want duplicates.
        /// </remarks>
        protected virtual bool IsValid(T entity)
        {
            return true;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Stores the events.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="version">The version.</param>
        private void StoreEvents(T entity, int version)
        {
            //// UNRESOLVED: This will be introduced as event listeners.
            //// if (entity.IsNot<IEventSourced>())
            //// {
            ////     return;
            //// }
            //// var sourced = entity as EventSourced;
            //// var changes = sourced.UncommittedChanges();
            //// foreach (var change in changes)
            //// {
            ////     this.context.Save(Provenance.New(entity.Id, version, change));
            //// }
            //// sourced.MarkChangesAsCommitted();
        }

        /// <summary>
        /// Stores the events.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void StoreEvents(T entity)
        {
            //// UNRESOLVED: This will be introduced as event listeners.
            //// this.StoreEvents(entity, entity.Version);
        } 

        #endregion
    }
}