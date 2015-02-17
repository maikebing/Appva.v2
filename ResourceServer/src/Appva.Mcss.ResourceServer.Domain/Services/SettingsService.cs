// <copyright file="SettingsService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using Appva.Core.Extensions;
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
        /// Adds a regionized setting to the cache.
        /// </summary>
        /// <param name="setting">The <see cref="Setting"/></param>
        /// <param name="region">Optional region</param>
        /// <returns>True if the <see cref="Setting"/> is successfully cached</returns>
        bool Add(Setting setting, string region = null);
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SettingsService : ISettingsService
    {
        #region Variables.

        /// <summary>
        /// The cache.
        /// </summary>
        private static readonly MemoryCache Cache = MemoryCache.Default;

        /// <summary>
        /// Whether or not the region cache has been initialized.
        /// </summary>
        private static readonly IList<string> InitializedTenants = new List<string>();

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public SettingsService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region ISettingsService Members

        /// <inheritdoc />
        public TValue Get<TValue>(string key, object defaultValue = null)
        {
            var region = this.GetRegion();
            if (! InitializedTenants.Contains(region))
            {
                this.Initialize(region);
            }
            key = region + "." + key;
            var value = Cache.Get(key);
            if (value == null)
            {
                var setting = this.persistenceContext.QueryOver<Setting>()
                    .Where(x => x.MachineName == key).SingleOrDefault();
                if (setting == null)
                {
                    return (defaultValue == null) ? default(TValue) : (TValue) defaultValue;
                }
                else
                {
                    this.Add(setting, region);
                    value = Cache.Get(key);
                }
            }
            return (TValue) value;
        }

        /// <inheritdoc />
        public bool Add(Setting setting, string region = null)
        {
            var regionName = region ?? this.GetRegion();
            var settingValue = setting.Value;
            if (setting.Type.Equals(typeof(string)))
            {
                settingValue = string.Format("'{0}'", setting.Value);
            }
            var value = JsonConvert.DeserializeObject(settingValue, setting.Type);
            return Cache.Add(
                new CacheItem(regionName + "." + setting.MachineName, value),
                new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1),
                    Priority = CacheItemPriority.Default
                });
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Returns the region.
        /// FIXME: This should not be here. Fix this somehow without breaking namespaces and 
        /// creating a mess in the code.
        /// </summary>
        /// <returns>The region</returns>
        private string GetRegion()
        {
            IIdentity identity = HttpContext.Current.IsNull() ? Thread.CurrentPrincipal.Identity : HttpContext.Current.User.Identity;
            if (identity.IsNotNull())
            {
                var claimsIdentity = identity as ClaimsIdentity;
                var tenant = claimsIdentity.Claims.Where(x => x.Type == "https://schemas.appva.se/identity/claims/tenant").SingleOrDefault();
                if (tenant.IsNotNull())
                {
                    return tenant.Value;
                }
            }
            return "default";
        }

        /// <summary>
        /// Initialize the settings region cache.
        /// </summary>
        /// <param name="region">The region</param>
        private void Initialize(string region)
        {
            var settings = this.persistenceContext.QueryOver<Setting>().List();
            foreach (var setting in settings)
            {
                this.Add(setting, region);
            }
            InitializedTenants.Add(region);
        }

        #endregion
    }
}