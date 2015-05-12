// <copyright file="SettingKey.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services.Settings
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ApplicationSettings
    {
        #region Device.

        /// <summary>
        /// Auto-generates the mobile device password.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity AutogeneratePasswordForMobileDevice = ApplicationSettingIdentity.CreateNew(
            "System.Core.Users.AutogeneratePasswordForClient", 
            "Auto-generate password for mobile device",
            "System.Core.Users",
            "Auto-generates mobile device password for user accounts automatically during creation");

        /// <summary>
        /// Makes the field for Mobile Device password editable.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity IsMobileDevicePasswordEditable = ApplicationSettingIdentity.CreateNew(
            "MCSS.Core.Account.EditableClientPassword",
            "Password for Mobile Device is editable",
            "System.Core.Account",
            "Makes the field for Mobile Device password editable in administration");

        #endregion

        #region Admin.

        public static ApplicationSettingIdentity IsUsernameVisible = ApplicationSettingIdentity.CreateNew(
            "MCSS.Core.Account.DisplayUsername",
            "Show account username",
            "System.Core.Account",
            "Makes the usernames for accounts visible in administration");

        #endregion

        #region Access Control List.

        /// <summary>
        /// Auto generates the mobile device password.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity IsAccessControlInstalled = ApplicationSettingIdentity.CreateNew(
            "Mcss.Core.Security.Acl.IsInstalled",
            "Access Control List is installed",
            "Mcss.Core.Security.Acl",
            "If true the Access Control List (ACL) is installed for the tenant");

        /// <summary>
        /// Access control is enabled for the current tenant.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity IsAccessControlActivated = ApplicationSettingIdentity.CreateNew(
            "Mcss.Core.Security.Acl.IsActive",
            "Access Control List is enabled",
            "Mcss.Core.Security.Acl",
            "If true the Access Control List (ACL) is enabled and active for the tenant");

        /// <summary>
        /// Access control is in preview mode for selected roles (Mcss Administrative role only). 
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity IsAccessControlInPreviewMode = ApplicationSettingIdentity.CreateNew(
            "Mcss.Core.Security.Acl.IsInPreviewMode",
            "Access Control List is in preview mode",
            "Mcss.Core.Security.Acl",
            "If true the Access Control List (ACL) is enabled only for Appva Mcss Administrative roles");

        #endregion

        #region External Auditing Logging.

        /// <summary>
        /// Audit logging analytics collection is enabled for the current tenant.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity IsAuditCollectionActivated = ApplicationSettingIdentity.CreateNew(
            "Mcss.Core.Security.Analytics.Audit.IsActive",
            "Audit logging collection is enabled",
            "Mcss.Core.Security.Analytics.Audit",
            "If true the audit logging analytics is enabled and active for the tenant");

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ApplicationSettingIdentity
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingIdentity"/> class.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="name">The friendly name</param>
        /// <param name="context">The namespace or context</param>
        /// <param name="description">The description of usage</param>
        private ApplicationSettingIdentity(string key, string name, string @namespace, string description)
        {
            this.Key = key;
            this.Name = name;
            this.Namespace = @namespace;
            this.Description = description;
        }

        #endregion

        #region Internal Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationSettingIdentity"/> class.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="name">The friendly name</param>
        /// <param name="context">The namespace or context</param>
        /// <param name="description">The description of usage</param>
        internal static ApplicationSettingIdentity CreateNew(string key, string name, string @namespace, string description)
        {
            return new ApplicationSettingIdentity(key, name, @namespace, description);
        }
        
        #endregion

        #region Public Properties.

        /// <summary>
        /// The unique key.
        /// </summary>
        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// The friendly name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The namespace.
        /// </summary>
        public string Namespace
        {
            get;
            private set;
        }

        /// <summary>
        /// The description of the setting usage.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        #endregion
    }
}