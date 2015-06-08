// <copyright file="AutoMapper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Office
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The mapping type</typeparam>
    internal static class AutoMapper<T>
    {
        /// <summary>
        /// The compiled cached expressions.
        /// </summary>
        private static readonly IDictionary<Type, Func<T, object>> Expressions = new Dictionary<Type, Func<T, object>>();

        /// <summary>
        /// Add mappings to the cache by type.
        /// </summary>
        /// <param name="expression">The mapping expression</param>
        public static void Map(Expression<Func<T, object>> expression)
        {
            if (! Expressions.ContainsKey(typeof(T)))
            {
                Expressions.Add(typeof(T), expression.Compile());
            }
        }

        /// <summary>
        /// Maps {T} to {TResult}.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="obj">The object to be mapped</param>
        /// <returns>Returns the mapped {TResult}</returns>
        public static TResult To<TResult>(T obj)
        {
            return Expressions.ContainsKey(typeof(T)) ? (TResult) Expressions[typeof(T)](obj) : default(TResult);
        }
    }
}