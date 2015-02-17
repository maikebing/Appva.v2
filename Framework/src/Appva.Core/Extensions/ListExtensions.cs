// <copyright file="ListExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Extensions
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// <see cref="IList{T}"/> extension helper methods.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Checks whether or not the <see cref="IList{T}"/> is null.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="list">The list to be checked</param>
        /// <returns>True if the list is null</returns>
        public static bool IsNull<T>(this IList<T> list)
        {
            return list == null;
        }

        /// <summary>
        /// Checks whether or not the <see cref="IList{T}"/> is not null.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="list">The list to be checked</param>
        /// <returns>True if the list is not null</returns>
        public static bool IsNotNull<T>(this IList<T> list)
        {
            return ! list.IsNull();
        }

        /// <summary>
        /// Checks whether or not the <see cref="IList{T}"/> is null
        /// or contains zero elements.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="list">The list to be checked</param>
        /// <returns>True if the list is null or contains zero elements</returns>
        public static bool IsEmpty<T>(this IList<T> list)
        {
            return list.IsNull() || list.Count == 0;
        }

        /// <summary>
        /// Checks whether or not the <see cref="IList{T}"/> is not null
        /// or contains at least one element.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="list">The list to be checked</param>
        /// <returns>True if the list is not null and contains at least one element</returns>
        public static bool IsNotEmpty<T>(this IList<T> list)
        {
            return ! list.IsEmpty();
        }

        /// <summary>
        /// Iterates over the list and executes an action.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="list">The list to be iterated over</param>
        /// <param name="action">The action to be executed for each iteration</param>
        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            if (list.IsNotNull())
            {
                foreach (var item in list)
                {
                    action(item);
                }
            }
        }
    }
}