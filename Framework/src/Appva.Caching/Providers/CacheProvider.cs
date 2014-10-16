// <copyright file="CacheProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Providers
{
    #region Imports.

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Common.Logging;
    using Validation;

    #endregion

    /// <summary>
    /// Abstract base implementation of <see cref="ICacheProvider"/>.
    /// </summary>
    public abstract class CacheProvider : ICacheProvider
    {
        #region Private Variables.

        /// <summary>
        /// Logging.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(CacheProvider));

        /// <summary>
        /// The tracked providers.
        /// </summary>
        private static readonly IDictionary<object, object> Tracked = new Dictionary<object, object>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheProvider"/> class.
        /// </summary>
        public CacheProvider()
        {
            Track(this);
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Amount of tracked providers.
        /// </summary>
        /// <returns>The count of tracked providers</returns>
        public static int ProviderCount
        {
            get
            {
                return Tracked.Count;
            }
        }

        /// <summary>
        /// Returns the enumerator.
        /// </summary>
        /// <returns>An IEnumerator</returns>
        public static IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return Tracked.GetEnumerator();
        }

        #endregion

        #region Implementation.

        /// <inheritdoc />
        public abstract T Get<T>(object key);

        /// <inheritdoc />
        public abstract object Get(object key);

        /// <inheritdoc />
        public abstract void Add(object key, object value);

        /// <inheritdoc />
        public abstract void Remove(object key);

        /// <inheritdoc />
        public abstract void RemoveAll();

        /// <inheritdoc />
        public abstract int Count();

        #endregion

        #region Internal Methods.

        /// <summary>
        /// Starts tracking a provider.
        /// </summary>
        /// <param name="provider">An implementation of <see cref="ICacheProvider"/></param>
        internal static void Track(ICacheProvider provider)
        {
            Requires.Argument(provider == null, "provider", "ICacheProvider cannot be null");
            var type = provider.GetType();
            var key = string.Format("{0}.{1}.{2}", type.Name, Guid.NewGuid(), Tracked.Count);
            Log.Trace(x => x("Adding cache provider {0} as trackable.", key));
            Tracked.Add(key, provider);
        }

        #endregion
    }
}