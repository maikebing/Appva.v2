// <copyright file="Like.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Repository
{
    #region Imports.

    using System;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// Enumertation for operators
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// Defines the AND operator
        /// </summary>
        And,

        /// <summary>
        /// Defines the OR operator
        /// </summary>
        Or
    }

    /// <summary>
    /// Defines a like-operator for OR and AND
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public class Like<TEntity> where TEntity : class
    {
        /// <summary>
        /// The property
        /// </summary>
        public Expression<Func<TEntity, object>> Property { get; set; }

        /// <summary>
        /// The value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Defines if this like is AND or OR
        /// </summary>
        public Operator Operator { get; set; }
    }
}
