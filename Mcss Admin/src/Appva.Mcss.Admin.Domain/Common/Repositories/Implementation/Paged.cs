// <copyright file="Paged.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Paged<T> : IPaged<T> where T : class
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Paged{T}"/> class.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <param name="pageQuery">The page query parameters.</param>
        /// <param name="items">The collection of items in the result set.</param>
        /// <param name="count">The count in the collection of items; i.e. not the page size.</param>
        /// <param name="totalCount">The total count in the set.</param>
        protected Paged(IPageQuery pageQuery, IList<T> items, long count, long totalCount)
        {
            this.PageQuery  = pageQuery;
            this.Items      = items;
            this.Count      = count;
            this.TotalCount = totalCount;
        }

        #endregion

        #region IPaged<T> Members.

        /// <inheritdoc />
        public IPageQuery PageQuery
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public IEnumerable<T> Items
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public long Count
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public long TotalCount
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool HasResults
        {
            get
            {
                return this.Count > 0;
            }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="Paged{T}"/> class.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <param name="pageQuery">The page query parameters.</param>
        /// <param name="items">The collection of items in the result set.</param>
        /// <param name="totalCount">The total count in the set.</param>
        /// <returns>A new <see cref="Paged{T}"/> instance.</returns>
        public static Paged<TEntity> New<TEntity>(IPageQuery pageQuery, IList<TEntity> items, long totalCount)
            where TEntity : class
        {
            return new Paged<TEntity>(pageQuery, items, items.Count, totalCount);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Paged{T}"/> class.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <param name="pageQuery">The page query parameters.</param>
        /// <param name="items">The collection of items in the result set.</param>
        /// <param name="count">The count in the collection of items; i.e. not the page size.</param>
        /// <param name="totalCount">The total count in the set.</param>
        /// <returns>A new <see cref="Paged{T}"/> instance.</returns>
        public static Paged<TEntity> New<TEntity>(IPageQuery pageQuery, IList<TEntity> items, long count, long totalCount)
            where TEntity : class
        {
            return new Paged<TEntity>(pageQuery, items, count, totalCount);
        }

        #endregion
    }
}