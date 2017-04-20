// <copyright file = "ListGeneralSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListGeneralSettingsModel : IRequest<bool>
    {
        #region Fields.

        /// <summary>
        /// Settings with a select list.
        /// </summary>
        private readonly string[] selectOption =
        {
            "MCSS.Device.Security",
            "MCSS.Secuity.Authorization.AdminAuthorizationMethod",
            "MCSS.Device.Settings.Timeline.OverviewTimelineTaskTypes"
        };

        /// <summary>
        /// JSON settings.
        /// </summary>
        private readonly string[] jsonSettings =
        {
            "Mcss.Core.Pdf",
            "Mcss.Core.Security.Jwt.Configuration.SecurityToken",
            "Mcss.Core.Security.Messaging.Email"
        };

        /// <summary>
        /// Ignored settings.
        /// </summary>
        private readonly string[] ignoredSettings =
        {
            "MCSS.Core.Inventory.Units",
            "Mcss.Core.Security.Analytics.Audit.Configuration",
            "Mcss.Integration.Ldap.LdapConfiguration",
            "MCSS.Device.HelpPage",
            "MCSS.Device.Beacon.UDID",
            "Mcss.Temporary.Global.Inventory.Amounts.Override",
            "Tenant.Client.LoginMethod"
        };

        /// <summary>
        /// Color codes.
        /// </summary>
        private string[] colors =
        {
            "#64B5F6", "#81C784", "#7986CB", "#E57373", "#4DB6AC", "#FFB74D", "#A1887F", "#90A4AE",
            "#F06292", "#1E88E5", "#F57C00", "#7CB342", "#4527A0", "#B71C1C", "#455A64", "#9E9E9E",
            "#FBC02D", "#81C784", "#A1887F", "#64B5F6", "#90A4AE", "#E57373", "#1E88E5", "#7986CB"
        };

        #endregion

        #region Properties.

        /// <summary>
        /// The settings list.
        /// </summary>
        public List<ListGeneralSettings> List
        {
            get;
            set;
        }

        /// <summary>
        /// Items that uses a dropdown list.
        /// </summary>
        public string[] SelectOptions
        {
            get { return this.selectOption; }
        }

        /// <summary>
        /// The JSON settings.
        /// </summary>
        public string[] JsonSettings
        {
            get { return this.jsonSettings; }
        }

        /// <summary>
        /// Settings to exclude from the list.
        /// </summary>
        public string[] IgnoredSettings
        {
            get { return this.ignoredSettings; }
        }

        /// <summary>
        /// Contains a list of color codes.
        /// </summary>
        public string[] Colors
        {
            get { return this.colors; }
        }

        #endregion
    }
}