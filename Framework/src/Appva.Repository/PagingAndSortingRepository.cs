﻿namespace Appva.Repository
{

    #region Imports.

    using NHibernate.Criterion;
    using System.Linq;
    using Persistence;

    #endregion

    /// <summary>
    /// Implementation of <see cref="IRepository{T}"/>
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity"></typeparam>
    public class PagingAndSortingRepository<TEntity> : Repository<TEntity>, IPagingAndSortingRepository<TEntity> where TEntity : class
    {

        #region Constructor.

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        public PagingAndSortingRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Returns a collection of <code>TEntity</code>.
        /// </summary>
        /// <param name="pageable"></param>
        /// <returns></returns>
        public PageableSet<TEntity> List(Pageable<TEntity> pageable)
        {
            var query = this.PersistenceContext.QueryOver<TEntity>();
            var pageIndex = pageable.PageNumber == 0 ? 1 : pageable.PageNumber;
            if (pageable.PageSort != null) {
                var ordered = query.OrderBy(pageable.PageSort.Order);
                query = (pageable.PageSort.Direction.Equals(Direction.Asc)) ? ordered.Asc : ordered.Desc;
            }
            if (pageable.Filters != null) {
                foreach (var expression in pageable.Filters) {
                    query.Where(expression);
                }
            }
            var items = query.Skip((pageIndex - 1) * pageable.PageSize).Take(pageable.PageSize).Future<TEntity>().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            return new PageableSet<TEntity> {
                CurrentPage = pageIndex,
                TotalCount = totalCount.Value,
                Entities = items,
                PageSize = pageable.PageSize
            };
        }

        /// <summary>
        /// Returns a collection of <code>TEntity</code>.
        /// </summary>
        /// <param name="searchable"></param>
        /// <returns></returns>
        public PageableSet<TEntity> Search(Searchable<TEntity> searchable)
        {
            var query = this.PersistenceContext.QueryOver<TEntity>();
            if (searchable.Likes != null) {
                var disjunction = new Disjunction();
                foreach (var like in searchable.Likes) {
                    disjunction.Add(Restrictions.On<TEntity>(like.Property).IsLike(like.Value, MatchMode.Anywhere));
                }
                query.Where(disjunction);
            }
            if (searchable.Filters != null) {
                foreach (var expression in searchable.Filters) {
                    query.Where(expression);
                }
            }
            var pageIndex = searchable.PageNumber == 0 ? 1 : searchable.PageNumber;
            if (searchable.PageSort != null) {
                var ordered = query.OrderBy(searchable.PageSort.Order);
                query = (searchable.PageSort.Direction.Equals(Direction.Asc)) ? ordered.Asc : ordered.Desc;
            }
            var items = query.Skip((pageIndex - 1) * searchable.PageSize).Take(searchable.PageSize).Future<TEntity>().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            return new PageableSet<TEntity> {
                CurrentPage = pageIndex,
                TotalCount = totalCount.Value,
                Entities = items,
                PageSize = searchable.PageSize
            };
        }

        #endregion

    }

}
