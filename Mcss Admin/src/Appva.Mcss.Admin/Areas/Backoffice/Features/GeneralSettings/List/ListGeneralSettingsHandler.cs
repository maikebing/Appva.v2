// <copyright file="ListGeneralSettingsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Ldap.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal sealed class ListGeneralSettingsHandler : RequestHandler<Parameterless<ListGeneralSettingsModel>, ListGeneralSettingsModel>
    {
        #region Fields.

        private readonly ISettingsService settingService;
        private readonly string[] machineNames = new string[] {
            "MCSS.Core.Inventory.Units",
            "Mcss.Core.Pdf",
            "Mcss.Core.Security.Analytics.Audit.Configuration",
            "Mcss.Core.Security.Jwt.Configuration.SecurityToken",
            "Mcss.Core.Security.Messaging.Email",
            "Mcss.Integration.Ldap.LdapConfiguration",
            "MCSS.Device.Security",
            "MCSS.Secuity.Authorization.AdminAuthorizationMethod",
            "MCSS.Device.Settings.Timeline.OverviewTimelineTaskTypes"
        };

        private readonly string[] ignoredSettings = new string[]
        {
            "Mcss.Core.Security.Analytics.Audit.Configuration",
            "Mcss.Integration.Ldap.LdapConfiguration",
            "MCSS.Device.HelpPage",
            "MCSS.Device.Beacon.UDID",
            "Tenant.Client.LoginMethod"
        };

        private string[] colorCodes = {
            "#64B5F6", "#81C784", "#7986CB", "#E57373",
            "#4DB6AC", "#FFB74D", "#A1887F", "#90A4AE",
            "#F06292", "#1E88E5", "#F57C00", "#7CB342",
            "#4527A0", "#B71C1C", "#455A64", "#9E9E9E",
            "#FBC02D", "#81C784", "#A1887F", "#64B5F6",
            "#90A4AE", "#E57373", "#1E88E5", "#7986CB"
        };

        #endregion

        #region Constructor.
        public ListGeneralSettingsHandler(ISettingsService settingService)
        {
            this.settingService = settingService;
        }
        #endregion

        #region RequestHandler Overrides.

        public override ListGeneralSettingsModel Handle(Parameterless<ListGeneralSettingsModel> message)
        {
            var settings = this.settingService.List();
            var setting = new ListGeneralSettingsModel();
            setting.List = new List<ListGeneralSettings>();
            int colorIndex = 0;

            foreach (var item in settings.Select((s, i) => new { Set = s, Index = i }))
            {
                var settingItems = new ListGeneralSettings();
                settingItems.Id = item.Set.Id;
                settingItems.Name = item.Set.Name;
                settingItems.MachineName = item.Set.MachineName;
                settingItems.BoolValue = null;
                settingItems.IsJson = false;
                settingItems.Category = ListGeneralSettings.ToCategoryString(item.Set.MachineName, 35);
                settingItems.Description = item.Set.Description;
                settingItems.Value = item.Set.Value;
                settingItems.Type = item.Set.Type;
                settingItems.CategoryColorCode = colorCodes[colorIndex];
                settingItems.MachineNames = machineNames;
                settingItems.IgnoredSettings = ignoredSettings;
                settingItems.Index = item.Index;

                if (item.Set.Type == typeof(Boolean))
                {
                    settingItems.BoolValue = Convert.ToBoolean(item.Set.Value);
                }

                if (item.Set.Type == typeof(String))
                {
                    settingItems.StringValue = "";
                    
                    if (item.Set.MachineName == machineNames[6] 
                        || item.Set.MachineName == machineNames[7] 
                        || item.Set.MachineName == machineNames[8])
                    {
                        settingItems.StringValue = item.Set.Value;
                        settingItems.IsJson = true;
                    }
                    


                    try
                    {
                        if (item.Set.MachineName == machineNames[0])
                        {
                            //settingItems.InventoryObject = JsonConvert.DeserializeObject<List<InventoryObject>>(setting.Value);
                        }
                        else if (item.Set.MachineName == machineNames[1])
                        {
                            settingItems.PdfLookAndFeel = JsonConvert.DeserializeObject<PdfLookAndFeel>(item.Set.Value);
                            settingItems.IsJson = true;
                        }
                        else if (item.Set.MachineName == machineNames[3]) {

                            settingItems.SecurityTokenConfig = JsonConvert.DeserializeObject<SecurityTokenConfiguration>(item.Set.Value);
                            settingItems.IsJson = true;

                        }
                        else if (item.Set.MachineName == machineNames[4])
                        {
                            settingItems.SecurityMailerConfig = JsonConvert.DeserializeObject<SecurityMailerConfiguration>(item.Set.Value);
                            settingItems.IsJson = true;
                        }
                        
                        else if (item.Set.MachineName == machineNames[5])
                        {
                            settingItems.LdapConfig = JsonConvert.DeserializeObject<LdapConfiguration>(item.Set.Value);
                            settingItems.IsJson = true;
                        }
                        
                    }
                    catch
                    {
                        settingItems.StringValue = item.Set.Value;
                    }

                    if (settingItems.IsJson == false)
                    {
                        settingItems.StringValue = item.Set.Value;
                    }

                }

                if (item.Set.Type == typeof(Int32))
                {
                    settingItems.IntValue = Convert.ToInt32(item.Set.Value);
                }

                if (item.Set.Type == typeof(IList<double>))
                {
                    List<double> list = null;

                    try
                    {
                        //list = JsonConvert.DeserializeObject<List<double>>(item.Set.Value);
                    }
                    catch { }

                    settingItems.ListValues = list;
                }

                if (item.Index < settings.Count() - 1 && settings.ElementAt(item.Index + 1).MachineName != item.Set.MachineName)
                {
                    colorIndex++;
                }
                
                if(colorIndex > colorCodes.Length - 1)
                {
                    colorIndex = 0;
                }

                if (ignoredSettings.Contains(item.Set.MachineName) == false)
                {
                    setting.List.Add(settingItems);

                }
            }

            setting.List = setting.List;
            return setting;
        }

        #endregion
    }
}