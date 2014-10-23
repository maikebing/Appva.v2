// <copyright file="SettingsService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using Newtonsoft.Json;

    #endregion

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
                if (! this.isInitiated)
                {
                    this.Instantiate();
                }
                else
                {
                    var setting = this.persistenceContext.QueryOver<Setting>()
                        .Where(x => x.Key == key).SingleOrDefault();
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
            return (TValue) this.cache.Get(key);
        }

        /// <inheritdoc />
        public bool Add(Setting setting)
        {
            var value = JsonConvert.DeserializeObject(setting.Value, setting.Type);
            return this.cache.Add(new CacheItem(setting.Key, value),
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