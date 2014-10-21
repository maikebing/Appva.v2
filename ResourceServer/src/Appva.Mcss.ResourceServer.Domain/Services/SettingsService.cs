// <copyright file="SettingsService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.Domain.Entities;
    using Appva.Persistence;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISettingsService : IService
    {
        /// <summary>
        /// Returns a setting by key. If not found in cache the default
        /// will be used instead (if not nullable).
        /// </summary>
        /// <typeparam name="TValue">The type to return</typeparam>
        /// <param name="key">The settings key</param>
        /// <param name="defaultValue">Optional default value</param>
        /// <returns>The {TValue}</returns>
        TValue Get<TValue>(string key, object defaultValue = null);

        /// <summary>
        /// Adds a setting to the cache.
        /// </summary>
        /// <param name="setting">The <see cref="Segging"/></param>
        /// <returns>True if the <see cref="Setting"/> is successfully cached</returns>
        bool Add(Setting setting);
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SettingsService : ISettingsService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        /// <summary>
        /// The cache.
        /// </summary>
        private readonly MemoryCache cache;

        /// <summary>
        /// Whether or not the cache has been initiated.
        /// </summary>
        private bool isInitiated;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public SettingsService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            this.cache = MemoryCache.Default;
        }

        #endregion

        #region ISettingsService Members

        /// <inheritdoc />
        public TValue Get<TValue>(string key, object defaultValue = null)
        {
            var value = this.cache.Get(key);
            if (value == null)
            {
                if (!this.isInitiated)
                {
                    this.Instantiate();
                }
                else
                {
                    var setting = this.persistenceContext.QueryOver<Setting>()
                        .Where(x => x.MachineName == key).SingleOrDefault();
                    if (setting == null)
                    {
                        if (defaultValue == null)
                        {
                            return default(TValue);
                        }
                        else
                        {
                            return (TValue) defaultValue;
                        }
                    }
                    else
                    {
                        this.Add(setting);
                    }
                }
            }
            if (! this.cache.Contains(key))
            {
                if (defaultValue == null)
                {
                    return default(TValue);
                }
                else
                {
                    return (TValue) defaultValue;
                }
            }
            else
            {
                return (TValue) this.cache.Get(key);
            }
        }

        /// <inheritdoc />
        public bool Add(Setting setting)
        {
            var settingValue = setting.Value;
            if (setting.Type.Equals(typeof(string)))
            {
                settingValue = string.Format("'{0}'", setting.Value);
            }
            var value = JsonConvert.DeserializeObject(settingValue, setting.Type);
            return this.cache.Add(
                new CacheItem(setting.MachineName, value),
                new CacheItemPolicy
                {
                    Priority = CacheItemPriority.NotRemovable
                });
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Initialize the settings cache.
        /// </summary>
        private void Instantiate()
        {
            var settings = this.persistenceContext.QueryOver<Setting>().List();
            foreach (var setting in settings)
            {
                this.Add(setting);
            }
            this.isInitiated = true;
        }

        #endregion
    }
}