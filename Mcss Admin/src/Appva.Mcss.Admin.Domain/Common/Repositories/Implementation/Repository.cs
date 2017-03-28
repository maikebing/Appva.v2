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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot, IEntity
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IPersistenceContext"/>.
        /// </param>
        public Repository(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion
    
        #region IRepository<T> Members.

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

        /// <inheritdoc />
        public void Update(T entity)
        {
            entity.MarkAsUpdated();
            this.context.Update(entity);
            this.StoreEvents(entity);
        }

        /// <inheritdoc />
        public void Save(T entity)
        {
            if (! this.IsValid(entity))
            {
                return;
            }
            this.context.Save(entity);
            this.StoreEvents (entity);
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

        #region Protected Members.

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

        #region Private Members.

        /// <summary>
        /// Stores the events.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void StoreEvents(T entity)
        {
            if (entity.IsNot<IEventSourced>())
            {
                return;
            }
            var sourced = entity as EventSourced;
            var changes = sourced.UncommittedChanges();
            foreach (var change in changes)
            {
                this.context.Save(Provenance.New(entity.Id, entity.Version, change));
            }
            sourced.MarkChangesAsCommitted();
        }

        #endregion
    }
}