// <copyright file="Searchable.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Repository
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// A search query.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity">The <code>TEntity</code></typeparam>
    public sealed class Searchable<TEntity> : PagingAndSortable<Searchable<TEntity>, TEntity> where TEntity : class
    {
        #region Private Fields.

        /// <summary>
        /// Collection of like operators
        /// </summary>
        private IList<Like<TEntity>> likes;

        #endregion

        #region Constructor.

        /// <summary>
        /// Prevents a default instance of the <see cref="Searchable{TEntity}" /> class from being created.
        /// </summary>
        private Searchable()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Collection of like operators
        /// </summary>
        internal IList<Like<TEntity>> Likes
        { 
            get
            {
                return this.likes;
            }
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Creates a <see cref="Searchable"/> with given property
        /// </summary>
        /// <param name="property">The property expression</param>
        /// <param name="value">The value</param>
        /// <returns>The <see cref="Searchable"/></returns>
        public static Searchable<TEntity> Over(Expression<Func<TEntity, object>> property = null, string value = null)
        {
            var instance = new Searchable<TEntity>();
            if (property != null && value != null) 
            {
                if (instance.likes == null)
                {
                    instance.likes = new List<Like<TEntity>>();
                }
                instance.likes.Add(new Like<TEntity>() { Property = property, Value = value });
            }
            return instance;
        }

        /// <summary>
        /// Adds like operators to current <see cref="Searchable"/>
        /// </summary>
        /// <param name="property">The property expression</param>
        /// <param name="value">The value</param>
        /// <returns><see cref="Searchable"/></returns>
        public Searchable<TEntity> MatchBy(Expression<Func<TEntity, object>> property, string value)
        {
            if (this.likes == null)
            {
                this.likes = new List<Like<TEntity>>();
            }
            this.likes.Add(new Like<TEntity>() { Property = property, Value = value });
            return this;
        }

        #endregion
    }
}
