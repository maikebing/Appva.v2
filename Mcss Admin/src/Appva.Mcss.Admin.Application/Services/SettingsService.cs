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
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Logging;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;
    using Newtonsoft.Json;
    using Appva.Core.Extensions;
    using System.Configuration;

    #endregion

    /// <summary>
    /// Access Control List tenant settings.
    /// </summary>
    public interface IAccessControlListTenantSettings
    {
        /// <summary>
        /// Returns whether or not access control list (ACL) is installed for the
        /// current tenant.
        /// </summary>
        /// <returns>True, if ACL is installed; defaults to false</returns>
        bool IsAccessControlListInstalled();

        /// <summary>
        /// Returns whether or not access control list (ACL) is activated for the
        /// current tenant.
        /// </summary>
        /// <returns>True, if ACL is activated; defaults to false</returns>
        bool IsAccessControlListActivated();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISettingsService :
        IAccessControlListTenantSettings,
        IService
    {
        /// <summary>
        /// Returns the <c>Setting</c> by key.
        /// </summary>
        /// <typeparam name="T">The type to convert to</typeparam>
        /// <param name="key">The unique key</param>
        /// <param name="defaultValue">The default value if value does not exist</param>
        /// <returns>Returns the <c>Setting</c> or null if not found</returns>
        T Find<T>(ApplicationSettingIdentity<T> key, object defaultValue = null) where T : struct;

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
        void Upsert<T>(ApplicationSettingIdentity<T> key, T value) where T : struct;

        

        ///////// OLD !!!

        bool HasSeniorAlert();
        bool HasOrderRefill();
        bool HasPatientTag();
        string GetSessionTimeout();
        bool SignOutWhenReady();
        string GetClientLoginMethod();
        int GetCalendarColorQuantity();
        Dictionary<string, object> GetCalendarSettings();
        Dictionary<string, object> GetAccountSettings();
        bool HasCalendarOverview();
        string CreateBackendAccountMailBody();
        string CreateAccountMailBody();
        bool DisplayAccountUsername();
        string GetNotificationAdmin();
        string GetAdminLogin();
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

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/> instance to use</param>
        /// <param name="repository">The <see cref="ISettingsRepository"/> instance</param>
        public SettingsService(IRuntimeMemoryCache cache, ISettingsRepository repository, IPersistenceContext persistence)
        {
            this.cache = cache;
            this.persistence = persistence;
            this.repository = repository;
        }

        #endregion

        #region ISettingsService Members.

        /// <inheritdoc />
        public T Find<T>(ApplicationSettingIdentity<T> key, object defaultValue = null) where T : struct
        {
            return this.ReturnCached<T>(key, defaultValue);
        }

        /// <inheritdoc />
        public IEnumerable<Setting> List()
        {
            return this.repository.List();
        }

        /// <inheritdoc />
        public void Upsert<T>(ApplicationSettingIdentity<T> key, T value) where T : struct
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
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "IsActive" && x.Namespace == "MCSS.SeniorAlert").SingleOrDefault();
            if (result != null)
            {
                TypeConverter tc = TypeDescriptor.GetConverter(result.Type);
                return (bool)tc.ConvertFromString(result.Value);
            }
            return false;
        }

        public bool HasOrderRefill()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "IsActive" && x.Namespace == "MCSS.Features.OrderRefill").SingleOrDefault();
            if (result != null)
            {
                TypeConverter tc = TypeDescriptor.GetConverter(result.Type);
                return (bool)tc.ConvertFromString(result.Value);
            }
            return false;
        }

        public bool HasPatientTag()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "PatientTag" && x.Namespace == "MCSS.Device.NFC").SingleOrDefault();
            if (result != null)
            {
                TypeConverter tc = TypeDescriptor.GetConverter(result.Type);
                return (bool)tc.ConvertFromString(result.Value);
            }

            return false;
        }

        public string GetSessionTimeout()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "SessionTimeout" && x.Namespace == "MCSS.Device.Authorization").SingleOrDefault();
            if (result != null)
            {
                return result.Value;
            }
            return "30";
        }

        public bool SignOutWhenReady()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "SignOutWhenReady" && x.Namespace == "MCSS.Device.Authorization").SingleOrDefault();
            if (result != null)
            {
                TypeConverter tc = TypeDescriptor.GetConverter(result.Type);
                return (bool)tc.ConvertFromString(result.Value);
            }
            return true;
        }

        public string GetClientLoginMethod()
        {
            return this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "Tenant.Client.LoginMethod").SingleOrDefault().Value;
        }

        public int GetCalendarColorQuantity()
        {
            var colors = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "Web.Calendar.NumberOfCategoryColors").SingleOrDefault();
            return colors.IsNull() ? 9 : Int32.Parse(colors.Value);
        }

        /// <summary>
        /// A dictyonary with all settings for calendar
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetCalendarSettings()
        {
            var result = new Dictionary<string, object>();
            var settings = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Namespace == "MCSS.Calendar").List();
            foreach (var setting in settings)
            {
                TypeConverter tc = TypeDescriptor.GetConverter(setting.Type);
                result.Add(setting.Name, tc.ConvertFromString(setting.Value));
            }
            return result;
        }

        /// <summary>
        /// A dictyonary with all settings for Accounts
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetAccountSettings()
        {
            var result = new Dictionary<string, object>();
            var settings = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Namespace == "MCSS.Core.Account").List();
            foreach (var setting in settings)
            {
                TypeConverter tc = TypeDescriptor.GetConverter(setting.Type);
                result.Add(setting.Name, tc.ConvertFromString(setting.Value));
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

        public string CreateBackendAccountMailBody()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "CreateBackendAccountMailBody" && x.Namespace == "MCSS.Core.Account").SingleOrDefault();
            if (result.IsNotNull())
            {
                return result.Value;
            }
            return ConfigurationManager.AppSettings.Get("EmailCreateBackEndAccountBody");
        }

        public string CreateAccountMailBody()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "CreateAccountMailBody" && x.Namespace == "MCSS.Core.Account").SingleOrDefault();
            if (result.IsNotNull())
            {
                return result.Value;
            }
            return ConfigurationManager.AppSettings.Get("EmailCreateAccountBody");
        }

        public bool DisplayAccountUsername()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "DisplayUsername" && x.Namespace == "MCSS.Core.Account").SingleOrDefault();

            if (result.IsNotNull())
            {
                TypeConverter tc = TypeDescriptor.GetConverter(result.Type);
                return (bool)tc.ConvertFromString(result.Value);
            }
            return false;
        }

        public string GetNotificationAdmin()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "AdministrationRole" && x.Namespace == "MCSS.Notifications").SingleOrDefault();

            if (result.IsNotNull())
            {
                TypeConverter tc = TypeDescriptor.GetConverter(result.Type);
                return (string)tc.ConvertFromString(result.Value);
            }
            return "_AA";
        }

        public string GetAdminLogin()
        {
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "AdminAuthorizationMethod" && x.Namespace == "MCSS.Secuity.Authorization").SingleOrDefault();

            if (result.IsNotNull())
            {
                TypeConverter tc = TypeDescriptor.GetConverter(result.Type);
                return (string)tc.ConvertFromString(result.Value);
            }
            return "form";
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
        private T ReturnCached<T>(ApplicationSettingIdentity<T> key, object defaultValue = null) where T : struct
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

        #region IAccessControlListTenantSettings Members.

        /// <inheritdoc />
        public bool IsAccessControlListInstalled()
        {
            return this.Find(ApplicationSettings.IsAccessControlInstalled, false);
        }

        /// <inheritdoc />
        public bool IsAccessControlListActivated()
        {
            return this.Find(ApplicationSettings.IsAccessControlActivated, false);
        }

        #endregion
    }
}