// <copyright file="ICacheProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Providers
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Cache provider interface.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Returns the object from cache.
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="key">The retrievable key for the object</param>
        /// <returns>The T or default(T)</returns>
        T Get<T>(object key);

        /// <summary>
        /// Returns the object from cache.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        /// <returns>The object or null</returns>
        object Get(object key);

        /// <summary>
        /// Stores an object in the cache.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        /// <param name="value">The object to store</param>
        void Add(object key, object value);

        /// <summary>
        /// Removes an object from the cache.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        void Remove(object key);

        /// <summary>
        /// Removes all items in the cache.
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Returns the size of the cache.
        /// </summary>
        /// <returns>The size of the cache</returns>
        int Count();
    }
}