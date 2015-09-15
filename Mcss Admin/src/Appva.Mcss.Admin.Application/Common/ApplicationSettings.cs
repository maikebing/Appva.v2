﻿// <copyright file="ApplicationSettings.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services.Settings
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Appva.Mcss.Admin.Application.Security.Jwt;
    using Appva.Mcss.Admin.Domain.VO;

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
            PdfLookAndFeel.CreateDefault());

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