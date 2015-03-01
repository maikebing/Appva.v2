// <copyright file="CacheProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching.Providers
{
    /// <summary>
    /// Abstract base implementation of <see cref="ICacheProvider"/>.
    /// </summary>
    public abstract class CacheProvider : ICacheProvider
    {
        #region ICacheProvider Members.

        /// <inheritdoc />
        public abstract T Get<T>(object key);

        /// <inheritdoc />
        public abstract object Get(object key);

        /// <inheritdoc />
        public abstract bool Add(object key, object value);

        /// <inheritdoc />
        public abstract bool AddOrUpdate(object key, object value);

        /// <inheritdoc />
        public abstract bool Update(object key, object value);

        /// <inheritdoc />
        public abstract bool Remove(object key);

        /// <inheritdoc />
        public abstract void RemoveAll();

        /// <inheritdoc />
        public abstract int Count();

        #endregion
    }
}
