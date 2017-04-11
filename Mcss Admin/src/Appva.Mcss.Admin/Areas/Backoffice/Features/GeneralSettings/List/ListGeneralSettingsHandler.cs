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
                settingItems.Description = item.Set.Description;
                settingItems.Value = item.Set.Value;
                settingItems.Type = item.Set.Type;
                settingItems.CategoryColor = model.Colors[colorIndex];
                SetCategoryName(settingItems, item.Set.Namespace);

                if (model.JsonSettings.Contains(item.Set.MachineName))
                {
                    settingItems.IsJson = true;

                    try
                    {
                        if (item.Set.MachineName == model.JsonSettings[0])
                        {
                            settingItems.PdfLookAndFeel = JsonConvert.DeserializeObject<PdfLookAndFeel>(item.Set.Value);
                        }
                        else if (item.Set.MachineName == model.JsonSettings[1])
                        {
                            settingItems.SecurityTokenConfig = JsonConvert.DeserializeObject<SecurityTokenConfiguration>(item.Set.Value);
                        }
                        else if (item.Set.MachineName == model.JsonSettings[2])
                        {
                            settingItems.SecurityMailerConfig = JsonConvert.DeserializeObject<SecurityMailerConfiguration>(item.Set.Value);
                        }
                    }
                    catch
                    {
                        settingItems.IsJson = false;
                    }
                }

                if (item.Index < settings.Count() - 1 && settings.ElementAt(item.Index + 1).Namespace != item.Set.Namespace)
                {
                    colorIndex++;
                }
                
                if(colorIndex > item.Index)
                {
                    colorIndex = 0;
                }

                model.List.Add(settingItems);
            }

            return model;
        }

        #endregion

        #region Methods.

        private void SetCategoryName(ListGeneralSettings item, string text)
        {
            text = text.ToLower().Replace("mcss.", "");
            var array = text.Split('.');
            string category = "", subCategory = "";

            if(array.Length > 0)
                category = array[0];

            if (array.Length > 1)
            {
                var newArray = array.Skip(1).ToArray();
                subCategory = string.Join("/", newArray);
            }

            if (subCategory.Length - 1 == '/')
            {
                subCategory = subCategory.Substring(0, subCategory.Length - 1);
            }

            if(subCategory == string.Empty)
            {
                int index = item.MachineName.LastIndexOf('.');
                subCategory = item.MachineName.Substring(index + 1);
            }

            item.Category = category;
            item.SubCategory = subCategory;
        }

        #endregion
    }
}