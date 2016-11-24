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
    using System.Configuration;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Core.Extensions;
    using Appva.Core.Logging;
    using Appva.Core.Resources;
    using Appva.Ldap.Configuration;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Persistence;
    using Newtonsoft.Json;
    using Validation;

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
    /// Old settings which needs to be merged.
    /// </summary>
    public interface IOldSettings
    {
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

        /// <summary>
        /// Returns whether or not to auto generate the password for the
        /// mobile device.
        /// </summary>
        /// <returns>True if password auto generation is enables; otherwise false</returns>
        bool AutogeneratePasswordForMobileDevice();
    }

    /// <summary>
    /// The security settings interface.
    /// </summary>
    public interface ISecuritySettings
    {
        /// <summary>
        /// Returns the security token configuration.
        /// </summary>
        /// <returns>The <see cref="SecurityTokenConfiguration"/></returns>
        SecurityTokenConfiguration SecurityTokenConfiguration();

        /// <summary>
        /// Returns whether or not the security token configuration is
        /// installed or not.
        /// </summary>
        /// <returns>True if security token configuration is installed; otherwise false</returns>
        bool IsSecurityTokenConfigurationInstalled();

        /// <summary>
        /// Returns the E-mail messaging configuration.
        /// </summary>
        /// <returns>The <see cref="SecurityMailerConfiguration"/></returns>
        SecurityMailerConfiguration MailMessagingConfiguration();

        /// <summary>
        /// Returns whether or not siths authorization is enabled or not.
        /// </summary>
        /// <returns>True if siths authorization is enabled; otherwise false</returns>
        bool IsSithsAuthorizationEnabled();

        /// <summary>
        /// Returns the password configuration.
        /// </summary>
        /// <returns>The <see cref="SecurityPasswordConfiguration"/></returns>
        SecurityPasswordConfiguration PasswordConfiguration();
    }

    /// <summary>
    /// The gui look and feel interface.
    /// </summary>
    public interface ILookAndFeel
    {
        /// <summary>
        /// Returns the pdf look and feel configuration.
        /// </summary>
        /// <returns>The <see cref="PdfLookAndFeel"/></returns>
        PdfLookAndFeel PdfLookAndFeelConfiguration();
    }

    /// <summary>
    /// Tenant-specific configuration settings
    /// </summary>
    public interface IConfigurationSettings
    {
        /// <summary>
        /// Gets all available inventory amount lists for withdrawal
        /// </summary>
        /// <returns>List of <see cref="InventoryAmountListModel"/></returns>
        IList<InventoryAmountListModel> GetIventoryAmountLists();
    }

    /// <summary>
    /// Settings for Ldap connection
    /// </summary>
    public interface ILdapSettings
    {
        /// <summary>
        /// Get Ldap configuration
        /// </summary>
        /// <returns></returns>
        ILdapConfiguration GetLdapConfiguration();
    }

    public interface IAuditConfiguration
    {
        /// <summary>
        /// Returns whether or not the audit logging is enabled.
        /// </summary>
        /// <returns>True if enabled; otherwise false</returns>
        bool IsAuditLoggingEnabled();

        /// <summary>
        /// Returns the audit logging configuration.
        /// </summary>
        /// <returns>The configuration</returns>
        AuditLoggingConfiguration AuditLoggingConfiguration();
    }

    public interface ICookieExpiration
    {
        /// <summary>
        /// Returns the cookie expiration.
        /// </summary>
        /// <returns>The time span</returns>
        TimeSpan GetCookieExpiration();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISettingsService :
        IAccessControlListTenantSettings,
        ISecuritySettings,
        ILookAndFeel,
        IOldSettings,
        IConfigurationSettings,
        IAuditConfiguration,
        ILdapSettings,
        ICookieExpiration,
        IService
    {
        /// <summary>
        /// Returns the <c>Setting</c> by key.
        /// FIXME: Internalize this to ensure that proper methods are executed.
        /// </summary>
        /// <typeparam name="T">The type to convert to</typeparam>
        /// <param name="key">The unique key</param>
        /// <returns>Returns the <c>Setting</c> or null if not found</returns>
        T Find<T>(ApplicationSettingIdentity<T> key);

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
        void Upsert<T>(ApplicationSettingIdentity<T> key, T value);
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
        /// The implemented <see cref="ITenantAwareMemoryCache"/> instance.
        /// </summary>
        private readonly ITenantAwareMemoryCache cache;

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
        public SettingsService(ITenantAwareMemoryCache cache, ISettingsRepository repository, IPersistenceContext persistence)
        {
            this.cache = cache;
            this.persistence = persistence;
            this.repository = repository;
        }

        #endregion

        #region ISettingsService Members.

        /// <inheritdoc />
        public T Find<T>(ApplicationSettingIdentity<T> key)
        {
            return this.ReturnCached<T>(key);
        }

        /// <inheritdoc />
        public IEnumerable<Setting> List()
        {
            return this.repository.List();
        }

        /// <inheritdoc />
        public void Upsert<T>(ApplicationSettingIdentity<T> key, T value)
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
                    //// Temporary fix for checking types which can be used with other implementation
                    //// of settings.
                    value.GetType().Namespace.StartsWith("System") && !value.GetType().FullName.Contains("Appva.") ? value.GetType() : typeof(string))
                    .Activate());
            }
            else
            {
                item.Update(key.Key, key.Namespace, key.Name, key.Description, JsonConvert.SerializeObject(value));
            }
            this.cache.Upsert<T>(
                this.CreateCacheKey(key.Key), 
                value,
                new RuntimeEvictionPolicy
                {
                    Priority = CacheItemPriority.Default,
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
        }

        #endregion

        #region IAccessControlListTenantSettings Members.

        /// <inheritdoc />
        public bool IsAccessControlListInstalled()
        {
            return this.Find(ApplicationSettings.IsAccessControlInstalled);
        }

        /// <inheritdoc />
        public bool IsAccessControlListActivated()
        {
            return this.Find(ApplicationSettings.IsAccessControlActivated);
        }

        #endregion

        #region ISecuritySettings.

        /// <inheritdoc />
        public SecurityTokenConfiguration SecurityTokenConfiguration()
        {
            return this.Find<SecurityTokenConfiguration>(ApplicationSettings.TokenConfiguration);
        }

        /// <inheritdoc />
        public bool IsSecurityTokenConfigurationInstalled()
        {
            return this.Find<SecurityTokenConfiguration>(ApplicationSettings.TokenConfiguration) != null;
        }

        /// <inheritdoc />
        public SecurityMailerConfiguration MailMessagingConfiguration()
        {
            return this.Find<SecurityMailerConfiguration>(ApplicationSettings.MailMessagingConfiguration);
        }

        /// <inheritdoc />
        public SecurityPasswordConfiguration PasswordConfiguration()
        {
            return this.Find<SecurityPasswordConfiguration>(ApplicationSettings.PasswordConfiguration);
        }

        /// <inheritdoc />
        public bool IsSithsAuthorizationEnabled()
        {
            return this.GetAdminLogin() == "siths";
        }

        #endregion

        #region ILookAndFeel Members.

        /// <inheritdoc />
        public PdfLookAndFeel PdfLookAndFeelConfiguration()
        {
            return this.Find(ApplicationSettings.PdfLookAndFeelConfiguration);
        }

        #endregion

        #region IOldSettings Members.

        /// <inheritdoc />
        public bool AutogeneratePasswordForMobileDevice()
        {
            return this.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice);
        }

        ////
        //// FIXME: Old settings.
        //// Old Settings which needs to be handled properly.
        ////

        /// <inheritdoc />
        /// TODO: Rename to IsRiskAssessmentEnabled
        public bool HasSeniorAlert()
        {
            if (this.Find<bool>(ApplicationSettings.IsRiskAssessmentEnabled))
            {
                return true;
            }
            //// Fallback for UNIQUE(NAME + NAMESPACE) which will be removed.
            var result = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                  .And(x => x.Name == "IsActive")
                  .And(x => x.Namespace == "MCSS.SeniorAlert")
                .Take(1)
                .SingleOrDefault();
            if (result == null)
            {
                return false;
            }
            var converter = TypeDescriptor.GetConverter(result.Type);
            return (bool) converter.ConvertFromString(result.Value);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public string GetClientLoginMethod()
        {
            return this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "Tenant.Client.LoginMethod").SingleOrDefault().Value;
        }

        /// <inheritdoc />
        public int GetCalendarColorQuantity()
        {
            var colors = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Name == "Web.Calendar.NumberOfCategoryColors").SingleOrDefault();
            return colors.IsNull() ? 9 : int.Parse(colors.Value);
        }

        /// <inheritdoc />
        public Dictionary<string, object> GetCalendarSettings()
        {
            var result = new Dictionary<string, object>();
            var settings = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Namespace == "MCSS.Calendar").List();
            foreach (var setting in settings)
            {
                var tc = TypeDescriptor.GetConverter(setting.Type);
                result.Add(setting.Name, tc.ConvertFromString(setting.Value));
            }
            return result;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public bool HasCalendarOverview()
        {
            var calendarSettings = this.GetCalendarSettings();
            if (calendarSettings.ContainsKey("HasOverview"))
            {
                return (bool)calendarSettings["HasOverview"];
            }
            return true;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        #region IConfigurationSettings implementation.

        /// <inheritdoc />
        public IList<InventoryAmountListModel> GetIventoryAmountLists()
        {
            var units = this.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts);

            //// Check in settings database if units exist in old format
            //// TODO: Remove in comming releases
            const string InventoryNamespace = "MCSS.Core.Inventory.Units";
            var deprecatedUnitSettings = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                  .And(x => x.Namespace == InventoryNamespace)
                .List();
            
            //// If deprecated settings for units exist, transfer to new setting
            //// TODO: Remove in comming releases
            if (deprecatedUnitSettings.Count != 0)
            {
                units = deprecatedUnitSettings.Select(x => new InventoryAmountListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Amounts = JsonConvert.DeserializeObject<IList<double>>(x.Value)
                }).ToList();

                //// Deactivates deprecated setting
                //// TODO: Remove in comming releases
                foreach (var d in deprecatedUnitSettings)
                {
                    d.IsActive = false;
                    this.repository.Save(d);
                }
            }
            this.Upsert<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts, units);
            return units;
        }

        #endregion

        #region IAuditConfiguration Members

        /// <inheritdoc />
        public bool IsAuditLoggingEnabled()
        {
            return this.Find<bool>(ApplicationSettings.IsAuditCollectionActivated);
        }

        /// <inheritdoc />
        public AuditLoggingConfiguration AuditLoggingConfiguration()
        {
            return this.Find<AuditLoggingConfiguration>(ApplicationSettings.AuditConfiguration);
        }

        #endregion

        #region ILdapSettings

        /// <inheritdoc />
        public ILdapConfiguration GetLdapConfiguration()
        {
            var settings = this.persistence.QueryOver<Setting>()
                .Where(x => x.IsActive)
                .And(x => x.Namespace == "MCSS.Integration.ActiveDirectory")
                .List();

            if(settings.Count < 1)
            {
                return null;
            }

            return LdapConfiguration.CreateNew(
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.LDAPConnectionString").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.LDAPUsername").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.LDAPPassword").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.UsernameField").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.PinField").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.MailField").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.HsaField").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.SSNField").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.FirstNameField").Value,
                    settings.FirstOrDefault(x => x.MachineName == "MCSS.Integration.ActiveDirectory.LastNameField").Value
                );
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setting"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private T ReturnCached<T>(ApplicationSettingIdentity<T> setting)
        {
            Requires.NotNull(setting, "setting");
            try
            {
                var cacheKey = this.CreateCacheKey(setting.Key);
                if (this.cache.Find(cacheKey) == null)
                {
                    var item = this.repository.Find(setting.Key);
                    if (item == null)
                    {
                        return setting.Default;
                    }
                    this.Add<T>(item);
                }
                return (T)this.cache.Find<T>(cacheKey);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "<SettingService> cache problem with key {0}", setting.Key);
                return setting.Default;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool Add<T>(Setting item)
        {
            try
            {
                var cachedItem = JsonConvert.DeserializeObject<T>(item.Value);
                this.cache.Upsert(
                    this.CreateCacheKey(item.MachineName),
                    cachedItem,
                    new RuntimeEvictionPolicy
                    {
                        Priority = CacheItemPriority.Default,
                        SlidingExpiration = TimeSpan.FromMinutes(30)
                    });
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "<SettingService> cache problem with id {0}", item.Id);
                return false;
            }
        }

        /// <summary>
        /// Returns the cache key for settings.
        /// </summary>
        /// <param name="key">The original key</param>
        /// <returns>A new cache key</returns>
        private string CreateCacheKey(string key)
        {
            return CacheTypes.Setting.FormatWith(key);
        }

        #endregion

        #region ICookieExpiration Members.

        /// <inheritdoc />
        public TimeSpan GetCookieExpiration()
        {
            return this.Find<TimeSpan>(ApplicationSettings.CookieExpiration);
        }

        #endregion
    }
}