namespace Appva.Repository
{

    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// Base class for paging and sorting.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TClass"></typeparam>
    public class PagingAndSortable<TClass, TEntity> where TClass : class where TEntity : class
    {

        #region Private Fields.

        /// <summary>
        /// The page number to be returned.
        /// </summary>
        private int _pageNumber = 1;

        /// <summary>
        /// The number of items to be returned.
        /// </summary>
        private int _pageSize = 10;

        /// <summary>
        /// The sorting parameters.
        /// </summary>
        private Sort<TEntity> _sort;

        /// <summary>
        /// The query filters.
        /// </summary>
        protected IList<Expression<Func<TEntity, bool>>> _filters;

        #endregion

        #region Public Properties.

        /// <summary>
        /// The page number to be returned.
        /// </summary>
        internal int PageNumber { 
            get {
                return this._pageNumber; 
            } 
        }

        /// <summary>
        /// The number of items to be returned.
        /// </summary>
        internal int PageSize
        {
            get {
                return this._pageSize;
            }
        }

        /// <summary>
        /// The sorting parameters.
        /// </summary>
        internal Sort<TEntity> PageSort
        {
            get {
                return this._sort;
            }
        }

        /// <summary>
        /// The query filters.
        /// </summary>
        internal IList<Expression<Func<TEntity, bool>>> Filters
        {
            get {
                return this._filters;
            }
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public TClass Page(int pageNumber)
        {
            this._pageNumber = pageNumber;
            return this as TClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public TClass Size(int pageSize)
        {
            this._pageSize = pageSize;
            return this as TClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TClass FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            CreateFiltersIfNull();
            this._filters.Add(filter);
            return this as TClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public TClass OrderBy(Expression<Func<TEntity, object>> order)
        {
            CreateSortIfNull();
            this._sort.Order = order;
            return this as TClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TClass Asc()
        {
            CreateSortIfNull();
            this._sort.Direction = Direction.Asc;
            return this as TClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public TClass Desc()
        {
            CreateSortIfNull();
            this._sort.Direction = Direction.Desc;
            return this as TClass;
        }

        #endregion

        #region Protected Helpers.

        /// <summary>
        /// 
        /// </summary>
        protected void CreateSortIfNull()
        {
            if (this._sort == null) {
                this._sort = new Sort<TEntity>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void CreateFiltersIfNull()
        {
            if (this._filters == null) {
                this._filters = new List<Expression<Func<TEntity, bool>>>();
            }
        }

        #endregion

    }

}
