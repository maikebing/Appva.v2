// <copyright file="ICacheProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching.Providers
{
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
        /// <exception cref="System.ArgumentNullException">If key is null</exception>
        T Get<T>(object key);

        /// <summary>
        /// Returns the object from cache.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        /// <returns>The object or null</returns>
        /// <exception cref="System.ArgumentNullException">If key is null</exception>
        object Get(object key);

        /// <summary>
        /// Stores an object in the cache.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        /// <param name="value">The object to store</param>
        /// <returns>True if the new cache object was successfully stored</returns>
        /// <exception cref="System.ArgumentNullException">If key is null</exception>
        /// <exception cref="System.ArgumentNullException">If value is null</exception>
        bool Add(object key, object value);

        /// <summary>
        /// Stores an object in the cache if it doesn't exist otherwise updates the
        /// existing value.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        /// <param name="value">The object to store</param>
        /// <returns>True if the new cache object was successfully stored or updated</returns>
        /// <exception cref="System.ArgumentNullException">If key is null</exception>
        /// <exception cref="System.ArgumentNullException">If value is null</exception>
        bool AddOrUpdate(object key, object value);

        /// <summary>
        /// Updates a cache item if it exists.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        /// <param name="value">The object to store</param>
        /// <returns>True if the new cache object was successfully updated</returns>
        /// <exception cref="System.ArgumentNullException">If key is null</exception>
        /// <exception cref="System.ArgumentNullException">If value is null</exception>
        bool Update(object key, object value);

        /// <summary>
        /// Removes an object from the cache.
        /// </summary>
        /// <param name="key">The retrievable key for the object</param>
        /// <returns>True if the new cache object was successfully removed</returns>
        /// <exception cref="System.ArgumentNullException">If key is null</exception>
        bool Remove(object key);

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
