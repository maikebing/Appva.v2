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
    /// <typeparam name="TEntity"></typeparam>
    public sealed class Pageable<TEntity> : PagingAndSortable<Pageable<TEntity>, TEntity> where TEntity : class
    {

        #region Constructor.

        /// <summary>
        /// Default Constructor.
        /// </summary>
        private Pageable()
        {
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Pageable<TEntity> Over(Expression<Func<TEntity, bool>> filter = null)
        {
            var instance = new Pageable<TEntity>();
            if (filter != null) {
                if (instance._filters == null) {
                    instance._filters = new List<Expression<Func<TEntity, bool>>>();
                }
                instance._filters.Add(filter);
            }
            return instance;
        }

        #endregion

    }

}
