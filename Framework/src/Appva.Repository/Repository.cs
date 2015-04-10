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
    using NHibernate;
    using NHibernate.Criterion;
    using Persistence;

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
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="{T}" /> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public Repository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
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
                return this.persistenceContext;
            }
        }

        #endregion

        #region IRepository<T> Members

        /// <inheritdoc />
        public T Get(object id)
        {
            return this.persistenceContext.Get<T>(id);
        }

        /// <inheritdoc />
        public object Save(T entity)
        {
            return this.persistenceContext.Save(entity);
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            this.persistenceContext.Update(entity);
        }

        /// <inheritdoc />
        public void Delete(T entity)
        {
            this.persistenceContext.Delete(entity);
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
        /// <param name="expression">The <see cref="Expression"/></param>
        /// <returns><see cref="IQueryOver"/></returns>
        protected IQueryOver<T, T> Where(Expression<Func<T, bool>> expression)
        {
            return this.persistenceContext.QueryOver<T>().Where(expression);
        }

        /// <summary>
        /// Builds a matching (<code>LIKE</code>) clause.
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/></param>
        /// <param name="query">The query string</param>
        /// <param name="matchMode">The <see cref="MatchMode"/></param>
        /// <returns><see cref="IQueryOver"/></returns>
        protected IQueryOver<T, T> IsLike(Expression<Func<T, object>> expression, string query, MatchMode matchMode = null)
        {
            if (matchMode == null)
            {
                matchMode = MatchMode.Anywhere;
            }
            return this.persistenceContext.QueryOver<T>()
                .Where(Restrictions.On(expression).IsLike(query, matchMode));
        }

        /// <summary>
        /// Builds a filtering <code>QueryOver</code> clause and adds an alias
        /// </summary>
        /// <param name="alias">The alisa expression</param>
        /// <returns><see cref="IQueryOver"/></returns>
        protected IQueryOver<T, T> Alias(Expression<Func<T>> alias)
        {
            return this.persistenceContext.QueryOver<T>(alias);
        }

        #endregion
    }
}