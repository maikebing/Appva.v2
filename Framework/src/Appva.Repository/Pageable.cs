// <copyright file="Pageable.cs" company="Appva AB">
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
    /// Class for pagination information.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity">Defines the entity</typeparam>
    public sealed class Pageable<TEntity> : PagingAndSortable<Pageable<TEntity>, TEntity> where TEntity : class
    {
        #region Constructor.

        /// <summary>
        /// Prevents a default instance of the <see cref="Pageable{TEntity}" /> class from being created.
        /// </summary>
        private Pageable()
        {
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Creates a Pageable for current entity with given filter
        /// </summary>
        /// <param name="filter">The filter expression</param>
        /// <returns>The <see cref="Pageable"/></returns>
        public static Pageable<TEntity> Over(Expression<Func<TEntity, bool>> filter = null)
        {
            var instance = new Pageable<TEntity>();
            if (filter != null) 
            {
                if (instance.Filters == null) 
                {
                    instance.Filters = new List<Expression<Func<TEntity, bool>>>();
                }
                instance.Filters.Add(filter);
            }
            return instance;
        }

        #endregion
    }
}
