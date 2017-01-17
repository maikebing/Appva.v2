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
    using Appva.Ldap.Configuration;
    using Appva.Mcss.Admin.Domain.VO;
using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Common;

    #endregion

    /// <summary>
    /// TODO: Split up in different classes for each specific settings group, e.g. 
    /// public static partial ApplicationSettings { ... }
    /// </summary>
    public static class ApplicationSettings
    {
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

        /// <summary>
        /// The tenant authentication cookie expiration timespan.
        /// </summary>
        public static readonly ApplicationSettingIdentity<TimeSpan> CookieExpiration = ApplicationSettingIdentity<TimeSpan>.CreateNew(
            "Mcss.Core.Security.Configuration.CookieExpiration",
            "Cookie Expiration Configuration",
            "Mcss.Core.Security.CookieExpiration",
            "The authentication cookie expiration configuration",
            TimeSpan.FromHours(1));

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

        /// <summary>
        /// The different units available for inventories
        /// </summary>
        /// <remarks>The setting returns a List of <c>InventoryAmountListModel</c></remarks>
        public static readonly ApplicationSettingIdentity<List<InventoryAmountListModel>> InventoryUnitsWithAmounts = ApplicationSettingIdentity<List<InventoryAmountListModel>>.CreateNew(
            "MCSS.Core.Inventory.Units",
            "The units available as for an inventory",
            "MCSS.Core.Inventory",
            "The units available for inventories, and their amount-lists",
            InventoryDefaults.Units()
            );

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

        #region Delegation

        /// <summary>
        /// The send-to text in print delegation
        /// </summary>
        /// <remarks>The setting returns an <c>string</c></remarks>
        public static readonly ApplicationSettingIdentity<string> PrintDelegationSendToText = ApplicationSettingIdentity<string>.CreateNew(
           "MCSS.Core.Delegation.Print.SendToText",
           "The send-to text on printed delegations",
           "MCSS.Core.Delegation.Print",
           "The text showed at the bottom about who the paper shall be sent to",
           "Sänd kopia till MAS och enhetschef.");

        /// <summary>
        /// If a reason should be specified on delegation removal
        /// </summary>
        /// <remarks>The setting returns an <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> SpecifyReasonOnDelegationRemoval = ApplicationSettingIdentity<bool>.CreateNew(
           "MCSS.Core.Delegation.Delete.SpecifyReason",
           "If a reason should be specified on delegation removal",
           "MCSS.Core.Delegation.Delete",
           "If the user should be prompted to specify a reson when deleteing a delegation",
           false);

        /// <summary>
        /// If a reason should be specified on delegation removal
        /// </summary>
        /// <remarks>The setting returns an <c>List</c></remarks>
        public static readonly ApplicationSettingIdentity<List<string>> DelegationRemovalReasons = ApplicationSettingIdentity<List<string>>.CreateNew(
           "MCSS.Core.Delegation.Delete.Reasons",
           "Reasons for delegation removal",
           "MCSS.Core.Delegation.Delete",
           "Reasons for delegation removal",
           new List<string> 
           { 
               "Delegering utgången, förnyas ej",
               "Medarbetare slutat"
           });

        /// <summary>
        /// If a reason should be specified on delegation removal
        /// </summary>
        /// <remarks>The setting returns an <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> DefaultFilterDelegationOverviewByIssuer = ApplicationSettingIdentity<bool>.CreateNew(
           "MCSS.Core.Delegation.Overview.DefaultFilterByIssuer",
           "If the delegation overview by default should be filtered by the issuer",
           "MCSS.Core.Delegation.Overview",
           "If the delegation overview by default should be filtered by the issuer",
           false);
        
        /// <summary>
        /// After a change the delegation should be pending for a new activation
        /// </summary>
        /// <remarks>The setting returns an <c>bool</c></remarks>
        public static readonly ApplicationSettingIdentity<bool> RequireDelegationActivationAfterChange = ApplicationSettingIdentity<bool>.CreateNew(
           "MCSS.Core.Delegation.Setting.RequireActivationAfterChange",
           "If a delegation should require a new activation after a change",
           "MCSS.Core.Delegation.Setting",
           "After a change the delegation should be pending for a new activation",
           false);

        #endregion

        #region Temporary Fixes.

        /// <summary>
        /// The schedule settings role map.
        /// </summary>
        /// <remarks>The setting returns a dictionary with schedule setting ID (key) role ID (value)</remarks>
        public static readonly ApplicationSettingIdentity<Dictionary<Guid, Guid>> TemporaryScheduleSettingsRoleMap = ApplicationSettingIdentity<Dictionary<Guid, Guid>>.CreateNew(
           "MCSS.Temporary.Sequence.Role",
           "The schedule settings role mapping",
           "MCSS.Temporary",
           "A simple dictionary schedule settings ID to role ID for required role on sequence",
           new Dictionary<Guid, Guid>());

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
            this.Key         = key;
            this.Name        = name;
            this.Namespace   = @namespace;
            this.Description = description;
            this.Default     = defaultValue;
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