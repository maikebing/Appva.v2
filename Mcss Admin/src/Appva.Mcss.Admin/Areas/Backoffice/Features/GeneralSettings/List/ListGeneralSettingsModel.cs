// <copyright file = "ListGeneralSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Cqrs;

    #endregion

    public class ListGeneralSettingsModel : IRequest<bool>
    {
        #region Fields.

        private readonly string[] selectOption = new string[]
        {
            "MCSS.Device.Security",
            "MCSS.Secuity.Authorization.AdminAuthorizationMethod",
            "MCSS.Device.Settings.Timeline.OverviewTimelineTaskTypes"

        };

        private readonly string[] jsonSettings = new string[] {
            "Mcss.Core.Pdf",
            "Mcss.Core.Security.Jwt.Configuration.SecurityToken",
            "Mcss.Core.Security.Messaging.Email"
        };

        private readonly string[] ignoredSettings = new string[]
        {
            "MCSS.Core.Inventory.Units",
            "Mcss.Core.Security.Analytics.Audit.Configuration",
            "Mcss.Integration.Ldap.LdapConfiguration",
            "MCSS.Device.HelpPage",
            "MCSS.Device.Beacon.UDID",
            "Mcss.Temporary.Global.Inventory.Amounts.Override",
            "Tenant.Client.LoginMethod"
        };

        private string[] colors = {
            "#64B5F6", "#81C784", "#7986CB", "#E57373", "#4DB6AC", "#FFB74D", "#A1887F", "#90A4AE",
            "#F06292", "#1E88E5", "#F57C00", "#7CB342", "#4527A0", "#B71C1C", "#455A64", "#9E9E9E",
            "#FBC02D", "#81C784", "#A1887F", "#64B5F6", "#90A4AE", "#E57373", "#1E88E5", "#7986CB"
        };

        #endregion
        
        #region Properties.

        public List<ListGeneralSettings> List
        {
            get;
            set;
        }

        public string[] SelectOptions
        {
            get { return selectOption; }
        }

        public string[] JsonSettings
        {
            get { return jsonSettings; }
        }

        public string[] IgnoredSettings
        {
            get { return ignoredSettings; }
        }

        public string[] Colors
        {
            get { return colors; }
        }

        #endregion
    }
}