// <copyright file="StatelessRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class StatelessRepository<T> : IRepository<T> where T : class, IAggregateRoot, IEntity
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IStatelessPersistenceContext"/>.
        /// </summary>
        private readonly IStatelessPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="StatelessRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IStatelessPersistenceContext"/>.
        /// </param>
        public StatelessRepository(IStatelessPersistenceContext context)
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
            return;
        }

        /// <inheritdoc />
        public void Save(T entity)
        {
            return;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the <see cref="IStatelessPersistenceContext"/>.
        /// </summary>
        protected IStatelessPersistenceContext Context
        {
            get
            {
                return this.context;
            }
        }

        #endregion
    }
}