// <copyright file="Sort.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Repository
{
    #region Imports.

    using System;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// Sort option for queries. You have to provide at least a list of properties to sort for 
    /// that must not include null or empty strings. The direction defaults to descending.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity">The <code>TEntity</code></typeparam>
    internal class Sort<TEntity> where TEntity : class
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="{TEntity}" /> class.
        /// </summary>
        /// <param name="order">The order</param>
        /// <param name="direction">The direction</param>
        public Sort(Expression<Func<TEntity, object>> order = null, Direction direction = Direction.Desc)
        {
            this.Order = order;
            this.Direction = direction;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Property implements the pairing of an Order and a property. 
        /// It is used to provide input for Sort.
        /// </summary>
        public Expression<Func<TEntity, object>> Order 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Enumeration for sort directions.
        /// </summary>
        public Direction Direction 
        { 
            get; 
            set; 
        }

        #endregion
    }
}
