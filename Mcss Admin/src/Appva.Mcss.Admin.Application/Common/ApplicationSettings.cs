// <copyright file="ApplicationSettings.cs" company="Appva AB">
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
    using System.Diagnostics.CodeAnalysis;
    using Appva.Mcss.Admin.Application.Security.Jwt;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Ldap.Configuration;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ApplicationSettings
    {
        #region Device.

        //// TODO: Add more here.

        #endregion

        #region Admin.

        /// <summary>
        /// Makes the field for Mobile Device password editable.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsMobileDevicePasswordEditable = ApplicationSettingIdentity<bool>.CreateNew(
            "MCSS.Core.Account.EditableClientPassword",
            "Password for Mobile Device is editable",
            "MCSS.Core.Account",
            "Makes the field for Mobile Device password editable in administration",
            true);

        /// <summary>
        /// Whether or not risk assessment is enabled or not.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsRiskAssessmentEnabled = ApplicationSettingIdentity<bool>.CreateNew(
            "MCSS.SeniorAlert.IsActive",
            "Risk assessment information is visible",
            "MCSS.SeniorAlert",
            "Whether or not risk assessment information, such as 'Senior' alert is visible",
            false);

        #endregion

        #region Account

        /// <summary>
        /// Auto-generates the mobile device password.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> AutogeneratePasswordForMobileDevice = ApplicationSettingIdentity<bool>.CreateNew(
            "MCSS.Core.Account.AutogeneratePasswordForClient",
            "Auto-generate password for mobile device",
            "MCSS.Core.Account",
            "Auto-generates mobile device password for user accounts automatically during creation",
            true);

        /// <summary>
        /// Makes the usernames for accounts visible in administration
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsUsernameVisible = ApplicationSettingIdentity<bool>.CreateNew(
            "MCSS.Core.Account.DisplayUsername",
            "Show account username",
            "MCSS.Core.Account",
            "Makes the usernames for accounts visible in administration",
            false);

        /// <summary>
        /// The visibility for Hsa ID on user accounts.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsHsaIdVisible = ApplicationSettingIdentity<bool>.CreateNew(
            "MCSS.Core.Account.IsHsaIdVisible",
            "Hsa ID visibility",
            "MCSS.Core.Account",
            "The Hsa ID visibility on user accounts in administration",
            false);

        #endregion

        #region Access Control List.

        /// <summary>
        /// Auto generates the mobile device password.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsAccessControlInstalled = ApplicationSettingIdentity<bool>.CreateNew(
            "Mcss.Core.Security.Acl.IsInstalled",
            "Access Control List is installed",
            "Mcss.Core.Security.Acl",
            "If true the Access Control List (ACL) is installed for the tenant",
            false);

        /// <summary>
        /// Access control is enabled for the current tenant.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsAccessControlActivated = ApplicationSettingIdentity<bool>.CreateNew(
            "Mcss.Core.Security.Acl.IsActive",
            "Access Control List is enabled",
            "Mcss.Core.Security.Acl",
            "If true the Access Control List (ACL) is enabled and active for the tenant",
            false);

        /// <summary>
        /// Access control is in preview mode for selected roles (Mcss Administrative role only). 
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsAccessControlInPreviewMode = ApplicationSettingIdentity<bool>.CreateNew(
            "Mcss.Core.Security.Acl.IsInPreviewMode",
            "Access Control List is in preview mode",
            "Mcss.Core.Security.Acl",
            "If true the Access Control List (ACL) is enabled only for Appva Mcss Administrative roles",
            false);

        #endregion

        #region Security.

        /// <summary>
        /// Security token configuration, i.e. issuer, audience, key, lifetime.
        /// </summary>
        /// <remarks>The setting returns a <c>SecurityTokenConfiguration</c></remarks>
        public static readonly ApplicationSettingIdentity<SecurityTokenConfiguration> TokenConfiguration = ApplicationSettingIdentity<SecurityTokenConfiguration>.CreateNew(
            "Mcss.Core.Security.Jwt.Configuration.SecurityToken",
            "Security Token Configuration",
            "Mcss.Core.Security.Jwt",
            "The JWT token configuration for issuing and authorizing security tokens",
            null);

        /// <summary>
        /// The E-mail configuration.
        /// </summary>
        /// <remarks>The setting returns a <c>SecurityMailerConfiguration</c></remarks>
        public static readonly ApplicationSettingIdentity<SecurityMailerConfiguration> MailMessagingConfiguration = ApplicationSettingIdentity<SecurityMailerConfiguration>.CreateNew(
            "Mcss.Core.Security.Messaging.Email",
            "Mail Configuration",
            "Mcss.Core.Security.Messaging",
            "The E-mail configuration for sending and signing",
            SecurityMailerConfiguration.CreateNew());

        /// <summary>
        /// The password configuration.
        /// </summary>
        /// <remarks>The setting returns a <c>SecurityPasswordConfiguration</c></remarks>
        public static readonly ApplicationSettingIdentity<SecurityPasswordConfiguration> PasswordConfiguration = ApplicationSettingIdentity<SecurityPasswordConfiguration>.CreateNew(
            "Mcss.Core.Security.Configuration.Password",
            "Password Configuration",
            "Mcss.Core.Security.Password",
            "The password configuration",
            SecurityPasswordConfiguration.CreateNew());

        #endregion

        #region External Auditing Logging.

        /// <summary>
        /// Audit logging analytics collection is enabled for the current tenant.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsAuditCollectionActivated = ApplicationSettingIdentity<bool>.CreateNew(
            "Mcss.Core.Security.Analytics.Audit.IsActive",
            "Audit logging collection is enabled",
            "Mcss.Core.Security.Analytics.Audit",
            "If true the audit logging analytics is enabled and active for the tenant",
            false);

        /// <summary>
        /// Audit logging analytics collection is enabled for the current tenant.
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<AuditLoggingConfiguration> AuditConfiguration = ApplicationSettingIdentity<AuditLoggingConfiguration>.CreateNew(
            "Mcss.Core.Security.Analytics.Audit.Configuration",
            "Audit logging configuration",
            "Mcss.Core.Security.Analytics.Audit",
            "The audit configuration settings",
            AuditLoggingConfiguration.CreateNew(new List<Guid>()));

        #endregion

        #region PDF.
 
         /// <summary>
         /// PDF configuration for look and feel.
         /// </summary>
         /// <remarks>The setting returns a <c>PdfProcessing</c></remarks>
         public static readonly ApplicationSettingIdentity<PdfLookAndFeel> PdfLookAndFeelConfiguration = ApplicationSettingIdentity<PdfLookAndFeel>.CreateNew(
             "Mcss.Core.Pdf",
             "Pdf Generation Configuration",
             "Mcss.Core.Pdf",
             "The PDF configuration for look and feel",
             PdfLookAndFeel.CreateDefault(null, "Appva AB"));

         /// <summary>
         /// PDF configuration for look and feel.
         /// </summary>
         /// <remarks>The setting returns a <c>PdfProcessing</c></remarks>
         public static readonly ApplicationSettingIdentity<bool> PdfShowInstructionsOnSeparatePage = ApplicationSettingIdentity<bool>.CreateNew(
             "Mcss.Core.Pdf.ShowInstructionsOnSeparatePage",
             "Pdf prescription name and instruction",
             "Mcss.Core.Pdf",
             "Whether or not to show the instructions on a separate reference page",
             true);
 
         #endregion

        #region Inventory

        /// <summary>
        /// The interval for inventory recount.
        /// </summary>
        /// <remarks>The setting returns an <c>int</c></remarks>
        public static readonly ApplicationSettingIdentity<int> InventoryCalculationSpanInDays = ApplicationSettingIdentity<int>.CreateNew(
           "MCSS.Core.Inventory.InventoryCalculationSpanInDays",
           "The interval for inventory recount",
           "MCSS.Core.Inventory",
           "If an inventory is not reocunted in this number of days it will be listed on the overview",
           30);

        #endregion

        #region LDAP.

        /// <summary>
        /// LDAP connection is enabled
        /// </summary>
        /// <remarks>The setting returns a <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> IsLdapConnectionEnabled = ApplicationSettingIdentity<bool>.CreateNew(
            "Mcss.Integration.Ldap.IsLdapConnectionEnabled",
            "Ldap connection enabled",
            "Mcss.Integration.Ldap",
            "Id the ldap connection is configured and enabled",
            false);

        /// <summary>
        /// LDAP configuration
        /// </summary>
        /// <remarks>The setting returns a <c>LdapConfiguration</c></remarks>
        public static readonly ApplicationSettingIdentity<LdapConfiguration> LdapConfiguration = ApplicationSettingIdentity<LdapConfiguration>.CreateNew(
            "Mcss.Integration.Ldap.LdapConfiguration",
            "LDAP connection configuration",
            "Mcss.Integration.Ldap",
            "The LDAP configuration",
            null);

        #endregion
    }

    /// <summary>
    /// TODO: Give a proper name, make internal.
    /// </summary>
    /// <typeparam name="T">The default value type</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class ApplicationSettingIdentity<T>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingIdentity{T}"/> class.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="name">The friendly name</param>
        /// <param name="namespace">The namespace or context</param>
        /// <param name="description">The description of usage</param>
        /// <param name="defaultValue">The default value</param>
        private ApplicationSettingIdentity(string key, string name, string @namespace, string description, T defaultValue)
        {
            this.Key = key;
            this.Name = name;
            this.Namespace = @namespace;
            this.Description = description;
            this.Default = defaultValue;
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

        /// <summary>
        /// The default value of the setting.
        /// </summary>
        public T Default
        {
            get;
            private set;
        }

        #endregion

        #region Internal Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationSettingIdentity{T}"/> class.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="name">The friendly name</param>
        /// <param name="namespace">The namespace or context</param>
        /// <param name="description">The description of usage</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>A new <see cref="ApplicationSettingIdentity{T}"/> instance</returns>
        internal static ApplicationSettingIdentity<T> CreateNew(string key, string name, string @namespace, string description, T defaultValue)
        {
            return new ApplicationSettingIdentity<T>(key, name, @namespace, description, defaultValue);
        }

        #endregion
    }
}