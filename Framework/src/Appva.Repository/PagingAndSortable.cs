// <copyright file="PagingAndSortable.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
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
    /// <typeparam name="TClass">The <code>TClass</code></typeparam>
    /// <typeparam name="TEntity">The <code>TEntity</code></typeparam>
    public class PagingAndSortable<TClass, TEntity> where TClass : class where TEntity : class
    {
        #region Private Fields.

        /// <summary>
        /// The query filters.
        /// </summary>
        private IList<Expression<Func<TEntity, bool>>> filters;

        /// <summary>
        /// The page number to be returned.
        /// </summary>
        private int pageNumber = 1;

        /// <summary>
        /// The number of items to be returned.
        /// </summary>
        private int pageSize = 10;

        /// <summary>
        /// The sorting parameters.
        /// </summary>
        private Sort<TEntity> sort;

        #endregion

        #region Public Properties.

        /// <summary>
        /// The page number to be returned.
        /// </summary>
        internal int PageNumber 
        { 
            get 
            {
                return this.pageNumber; 
            } 
        }

        /// <summary>
        /// The number of items to be returned.
        /// </summary>
        internal int PageSize
        {
            get 
            {
                return this.pageSize;
            }
        }

        /// <summary>
        /// The sorting parameters.
        /// </summary>
        internal Sort<TEntity> PageSort
        {
            get 
            {
                return this.sort;
            }
        }

        /// <summary>
        /// The query filters.
        /// </summary>
        protected internal IList<Expression<Func<TEntity, bool>>> Filters
        {
            get 
            {
                return this.filters;
            }

            set
            {
            }
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Returns the page with the given pagenumber
        /// </summary>
        /// <param name="pageNumber">The pagenumber</param>
        /// <returns>The page</returns>
        public TClass Page(int pageNumber)
        {
            this.pageNumber = pageNumber;
            return this as TClass;
        }

        /// <summary>
        /// Creates a TClass whit given pagesize
        /// </summary>
        /// <param name="pageSize">The size of the page</param>
        /// <returns>The TClass</returns>
        public TClass Size(int pageSize)
        {
            this.pageSize = pageSize;
            return this as TClass;
        }

        /// <summary>
        /// Creates a TClass with given filter
        /// </summary>
        /// <param name="filter">The filter expression</param>
        /// <returns>The TClass</returns>
        public TClass FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            this.CreateFiltersIfNull();
            this.filters.Add(filter);
            return this as TClass;
        }

        /// <summary>
        /// Creates a TClass with given order
        /// </summary>
        /// <param name="order">The order expression</param>
        /// <returns>The TClass</returns>
        public TClass OrderBy(Expression<Func<TEntity, object>> order)
        {
            this.CreateSortIfNull();
            this.sort.Order = order;
            return this as TClass;
        }

        /// <summary>
        /// Creates a TClass with ascending direction
        /// </summary>
        /// <returns>The TClass</returns>
        public TClass Asc()
        {
            this.CreateSortIfNull();
            this.sort.Direction = Direction.Asc;
            return this as TClass;
        }

        /// <summary>
        /// Creates a TClass with descending direction
        /// </summary>
        /// <returns>The TClass</returns>
        public TClass Desc()
        {
            this.CreateSortIfNull();
            this.sort.Direction = Direction.Desc;
            return this as TClass;
        }

        #endregion

        #region Protected Helpers.

        /// <summary>
        /// Creates a sort when null
        /// </summary>
        protected void CreateSortIfNull()
        {
            if (this.sort == null) 
            {
                this.sort = new Sort<TEntity>();
            }
        }

        /// <summary>
        /// Creates filter when null
        /// </summary>
        protected void CreateFiltersIfNull()
        {
            if (this.filters == null) 
            {
                this.filters = new List<Expression<Func<TEntity, bool>>>();
            }
        }

        #endregion
    }
}