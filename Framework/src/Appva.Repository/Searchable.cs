﻿namespace Appva.Repository
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
    /// <typeparam name="TEntity"></typeparam>
    public sealed class Searchable<TEntity> : PagingAndSortable<Searchable<TEntity>, TEntity> where TEntity : class
    {

        #region Private Fields.

        /// <summary>
        /// 
        /// </summary>
        private IList<Like<TEntity>> likes;

        #endregion

        #region Public Properties.

        /// <summary>
        /// 
        /// </summary>
        internal IList<Like<TEntity>> Likes { 
            get {
                return this.likes;
            }
        }

        #endregion

        #region Public Functions.

        #region Constructor.

        /// <summary>
        /// Default Constructor.
        /// </summary>
        private Searchable()
        {
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Searchable<TEntity> Over(Expression<Func<TEntity, object>> property = null, string value = null)
        {
            var instance = new Searchable<TEntity>();
            if (property != null && value != null) {
                if (instance.likes == null) {
                    instance.likes = new List<Like<TEntity>>();
                }
                instance.likes.Add(new Like<TEntity>() { Property = property, Value = value });
            }
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Searchable<TEntity> MatchBy(Expression<Func<TEntity, object>> property, string value)
        {
            if (this.likes == null) {
                this.likes = new List<Like<TEntity>>();
            }
            this.likes.Add(new Like<TEntity>() { Property = property, Value = value });
            return this;
        }

        #endregion

    }

}
