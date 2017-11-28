// <copyright file="IEnumerableExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Makes a deep copy of a collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>A new collection of {T}.</returns>
        public static IEnumerable<T> DeepCopy<T>(this IEnumerable<T> source) where T : IDeepCopyable<T>
        {
            return source.Select(x => x != null ? x.DeepCopy() : default(T));
        }
    }
}