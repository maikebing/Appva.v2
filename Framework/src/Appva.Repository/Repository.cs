// <copyright file="Repository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Repository
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Persistence;
    using NHibernate;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// Implementation of <see cref="IRepository{T}"/>
    /// </summary>
    /// <typeparam name="T">A persistent entity type</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Private Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        public Repository(IPersistenceContext persistenceContext)
        {
            this._persistenceContext = persistenceContext;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the current <see cref="IPersistenceContext"/>.
        /// </summary>
        public IPersistenceContext PersistenceContext
        {
            get
            {
                return this._persistenceContext;
            }
        }

        #endregion

        #region IRepository<T> Members

        /// <inheritdoc />
        public T Get(object id)
        {
            return this._persistenceContext.Get<T>(id);
        }

        /// <inheritdoc />
        public object Save(T entity)
        {
            return this._persistenceContext.Save(entity);
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            this._persistenceContext.Update(entity);
        }

        /// <inheritdoc />
        public void Delete(T entity)
        {
            this._persistenceContext.Delete(entity);
        }

        /// <inheritdoc />
        public async Task<T> GetAsync(object id)
        {
            return await Task<T>.Factory.StartNew(() => this.Get(id));
        }

        /// <inheritdoc />
        public async Task<object> SaveAsync(T entity)
        {
            return await Task<object>.Factory.StartNew(() => this.Save(entity));
        }

        #endregion

        #region Inherited Methods.

        /// <summary>
        /// Builds a filtering <code>WHERE</code> clause.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns><see cref="IQueryOver"/></returns>
        protected IQueryOver<T, T> Where(Expression<Func<T, bool>> expression)
        {
            return this._persistenceContext.QueryOver<T>().Where(expression);
        }

        /// <summary>
        /// Builds a matching (<code>LIKE</code>) clause.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="query"></param>
        /// <param name="matchMode"></param>
        /// <returns><see cref="IQueryOver"/></returns>
        protected IQueryOver<T, T> IsLike(Expression<Func<T, object>> expression, string query, MatchMode matchMode = null)
        {
            if (matchMode == null)
            {
                matchMode = MatchMode.Anywhere;
            }
            return this._persistenceContext.QueryOver<T>()
                .Where(Restrictions.On(expression).IsLike(query, matchMode));
        }

        #endregion
    }
}