// <copyright file="SettingsService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services.Settings
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Logging;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISettingsService : IService
    {
        /// <summary>
        /// Returns the <c>Setting</c> by key.
        /// </summary>
        /// <typeparam name="T">The type to convert to</typeparam>
        /// <param name="key">The unique key</param>
        /// <param name="defaultValue">The default value if value does not exist</param>
        /// <returns>Returns the <c>Setting</c> or null if not found</returns>
        T Find<T>(ApplicationSettingIdentity key, object defaultValue = null);

        /// <summary>
        /// Returns a collection of tenant <see cref="Setting"/>.
        /// </summary>
        /// <returns>A collection of tenant <see cref="Setting"/></returns>
        IEnumerable<Setting> List();

        /// <summary>
        /// Saves a new setting or updates it if a reference is found.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="value">The value to be added</param>
        void Upsert(ApplicationSettingIdentity key, object value);

        ///////// OLD !!!

        bool HasSeniorAlert();
        bool HasOrderRefill();
        bool HasPatientTag();
        int GetCalendarColorQuantity();
        Dictionary<string, object> GetCalendarSettings();

        bool HasCalendarOverview();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SettingsService : ISettingsService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> logging instance.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<SettingsService>();

        /// <summary>
        /// The lock.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// The implemented <see cref="IRuntimeMemoryCache"/> instance.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        /// <summary>
        /// The implemented <see cref="ISettingsRepository"/> instance.
        /// </summary>
        private readonly ISettingsRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/> instance to use</param>
        /// <param name="repository">The <see cref="ISettingsRepository"/> instance</param>
        public SettingsService(IRuntimeMemoryCache cache, ISettingsRepository repository)
        {
            this.cache = cache;
            this.repository = repository;
        }

        #endregion

        #region ISettingsService Members.

        /// <inheritdoc />
        public T Find<T>(ApplicationSettingIdentity key, object defaultValue = null)
        {
            return this.ReturnCached<T>(key, defaultValue);
        }

        /// <inheritdoc />
        public IEnumerable<Setting> List()
        {
            return this.repository.List();
        }

        /// <inheritdoc />
        public void Upsert(ApplicationSettingIdentity key, object value)
        {
            var item = this.repository.Find(key.Key);
            if (item == null)
            {
                this.repository.Save(Setting.CreateNew(
                    key.Key, 
                    key.Namespace, 
                    key.Name, 
                    key.Description, 
                    JsonConvert.SerializeObject(value), 
                    value.GetType())
                    .Activate());
            }
            else
            {
                item.Update(key.Key, key.Namespace, key.Name, key.Description, JsonConvert.SerializeObject(value));
            }
        }

        //////////////////////////////////////// FROM OLD

        public bool HasSeniorAlert()
        {
            try
            {
                var setting = this.repository.Find("MCSS.SeniorAlert.IsActive");
                if (setting != null)
                {
                    return JsonConvert.DeserializeObject<bool>(setting.Value);
                }
                //// Fallback in case there are other that uses this type.
                //// var settings = Filter(x => x.Active && x.Name == "IsActive" && x.Namespace == "MCSS.SeniorAlert").List();
                // if (settings.Count == 1)
                // {
                //    return JsonConvert.DeserializeObject<bool>(settings[0].Value);
                // }
            }
            catch (Exception)
            {
                //// No op.
            }
            return false;
        }

        public bool HasOrderRefill()
        {
            try
            {
                var setting = this.repository.Find("MCSS.Features.OrderRefill.IsActive");
                if (setting != null)
                {
                    return JsonConvert.DeserializeObject<bool>(setting.Value);
                }
                //// Fallback in case there are other that uses this type.
                // var settings = Filter(x => x.Active && x.Name == "IsActive" && x.Namespace == "MCSS.Features.OrderRefill").List();
                // if (settings.Count == 1)
                // {
                //     return JsonConvert.DeserializeObject<bool>(settings[0].Value);
                // }
            }
            catch (Exception)
            {
                //// No op.
            }
            return false;
        }

        public bool HasPatientTag()
        {
            try
            {
                var setting = this.repository.Find("MCSS.Device.NFC.PatientTag");
                if (setting != null)
                {
                    return JsonConvert.DeserializeObject<bool>(setting.Value);
                }
                //// Fallback in case there are other that uses this type.
                // var settings = Filter(x => x.Active && x.Name == "PatientTag" && x.Namespace == "MCSS.Device.NFC").List();
                // if (settings.Count == 1)
                // {
                //     return JsonConvert.DeserializeObject<bool>(settings[0].Value);
                // }
            }
            catch (Exception)
            {
                //// No op.
            }
            return false;
        }

        public int GetCalendarColorQuantity()
        {
            try
            {
                var setting = this.repository.Find("Web.Calendar.NumberOfCategoryColors");
                if (setting != null)
                {
                    return JsonConvert.DeserializeObject<int>(setting.Value);
                }
                //// Fallback in case there are other that uses this type.
                // var settings = Filter(x => x.Active && x.Name == "Web.Calendar.NumberOfCategoryColors").List();
                // if (settings.Count == 1)
                // {
                //    return JsonConvert.DeserializeObject<int>(settings[0].Value);
                // }
            }
            catch (Exception)
            {
                //// No op.
            }
            return 9;
        }

        /// <summary>
        /// A dictionary with all settings for calendar
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetCalendarSettings()
        {
            var result = new Dictionary<string, object>();
            var settings = this.repository.FindByNamespace("MCSS.Calendar");
            foreach (var setting in settings)
            {
                result.Add(setting.Name, JsonConvert.DeserializeObject(setting.Value, setting.Type));
            }
            return result;
        }

        public bool HasCalendarOverview()
        {
            var calendarSettings = GetCalendarSettings();
            if (calendarSettings.ContainsKey("HasOverview"))
            {
                return (bool)calendarSettings["HasOverview"];
            }
            return true;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private T ReturnDefault<T>(object defaultValue = null)
        {
            if (defaultValue != null)
            {
                return (T) defaultValue;
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private T ReturnCached<T>(ApplicationSettingIdentity key, object defaultValue = null)
        {
            try
            {
                if (key == null)
                {
                    return this.ReturnDefault<T>(defaultValue);
                }
                if (this.cache.Find(key.Key) == null)
                {
                    var item = this.repository.Find(key.Key);
                    if (item == null)
                    {
                        return this.ReturnDefault<T>(defaultValue);
                    }
                    this.Add(item);
                }
                return (T) this.cache.Find<T>(key.Key);
            }
            catch (Exception ex)
            {
                Log.ErrorException("<SettingService> cache problem with key " + key.Key, ex);
                return this.ReturnDefault<T>(defaultValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool Add(Setting item)
        {
            try
            {
                var cachedItem = JsonConvert.DeserializeObject(item.Value, item.Type);
                this.cache.Upsert(item.MachineName, cachedItem, new RuntimeEvictionPolicy
                {
                    Priority = CacheItemPriority.Default,
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorException("<SettingService> cache problem with id " + item.Id, ex);
                return false;
            }
        }

        #endregion
    }
}