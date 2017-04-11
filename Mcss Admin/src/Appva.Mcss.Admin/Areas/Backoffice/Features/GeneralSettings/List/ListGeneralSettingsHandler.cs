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
            int colorIndex = 0;
            var settings = this.settingService.List();
            var model = new ListGeneralSettingsModel();
            model.List = new List<ListGeneralSettings>();

            foreach (var item in settings.Select((s, i) => new { Set = s, Index = i }))
            {
                if (model.IgnoredSettings.Contains(item.Set.MachineName))
                {
                    continue;
                }

                var settingItems = new ListGeneralSettings();
                settingItems.Id = item.Set.Id;
                settingItems.Index = item.Index;
                settingItems.Name = item.Set.Name;
                settingItems.MachineName = item.Set.MachineName;
                settingItems.Category = ToCategoryString(item.Set.Namespace, 35);
                settingItems.Description = item.Set.Description;
                settingItems.Value = item.Set.Value;
                settingItems.Type = item.Set.Type;
                settingItems.CategoryColor = model.Colors[colorIndex];

                if (model.JsonSettings.Contains(item.Set.MachineName))
                {
                    settingItems.IsJson = true;

                    try
                    {
                        if (item.Set.MachineName == model.JsonSettings[0])
                        {
                            //settingItems.InventoryObject = JsonConvert.DeserializeObject<List<InventoryObject>>(setting.Value);
                        }
                        else if (item.Set.MachineName == model.JsonSettings[1])
                        {
                            settingItems.PdfLookAndFeel = JsonConvert.DeserializeObject<PdfLookAndFeel>(item.Set.Value);
                        }
                        else if (item.Set.MachineName == model.JsonSettings[2])
                        {
                            settingItems.SecurityTokenConfig = JsonConvert.DeserializeObject<SecurityTokenConfiguration>(item.Set.Value);
                        }
                        else if (item.Set.MachineName == model.JsonSettings[3])
                        {
                            settingItems.SecurityMailerConfig = JsonConvert.DeserializeObject<SecurityMailerConfiguration>(item.Set.Value);
                        }
                    }
                    catch
                    {
                        settingItems.IsJson = false;
                    }
                }

                if (item.Index < settings.Count() - 1 && settings.ElementAt(item.Index + 1).MachineName != item.Set.MachineName)
                {
                    colorIndex++;
                }
                
                if(colorIndex > model.JsonSettings.Length - 1)
                {
                    colorIndex = 0;
                }

                model.List.Add(settingItems);
            }

            return model;
        }

        #endregion

        #region Methods.

        public static string ToCategoryString(string s, int max)
        {
            var array = s.Split('.');
            string category = "";

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].ToLower() != "mcss")
                {
                    category += array[i] + (i > 0 && i < array.Length - 1 ? "/" : "");
                }
            }

            if (category.Length > max)
            {
                category = category.Substring(category.Length - max).Trim();
                int index = category.IndexOf('/');
                category = category.Contains("/") && category[0] != '/' ? category.Remove(0, index) : category;
            }

            return category;
        }

        #endregion
    }
}