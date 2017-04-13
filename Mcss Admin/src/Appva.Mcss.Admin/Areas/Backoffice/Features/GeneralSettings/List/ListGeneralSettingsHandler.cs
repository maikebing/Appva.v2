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
            int index = 0;
            var settings = this.settingService.List();
            var model = new ListGeneralSettingsModel();
            model.List = new List<ListGeneralSettings>();

            foreach (var item in settings)
            {
                if (model.IgnoredSettings.Contains(item.MachineName))
                {
                    index = index > 0 ? index-- : index;
                    continue;
                }

                var settingItems = new ListGeneralSettings();
                settingItems.Id = item.Id;
                settingItems.Index = index;
                settingItems.Name = item.Name;
                settingItems.MachineName = item.MachineName;
                settingItems.Description = item.Description;
                settingItems.Value = item.Value;
                settingItems.Type = item.Type;
                settingItems.CategoryColor = model.Colors[colorIndex];
                SetCategoryName(settingItems, item.Namespace, "mcss.");

                if (model.JsonSettings.Contains(item.MachineName))
                {
                    settingItems.IsJson = true;

                    try
                    {
                        if (item.MachineName == model.JsonSettings[0])
                        {
                            settingItems.PdfLookAndFeel = JsonConvert.DeserializeObject<PdfLookAndFeel>(item.Value);
                        }
                        else if (item.MachineName == model.JsonSettings[1])
                        {
                            settingItems.SecurityTokenConfig = JsonConvert.DeserializeObject<SecurityTokenConfiguration>(item.Value);
                        }
                        else if (item.MachineName == model.JsonSettings[2])
                        {
                            settingItems.SecurityMailerConfig = JsonConvert.DeserializeObject<SecurityMailerConfiguration>(item.Value);
                        }
                    }
                    catch
                    {
                        settingItems.IsJson = false;
                    }
                }

                if (index < settings.Count() - 1 && settings.ElementAt(index + 1).Namespace != item.Namespace)
                {
                    colorIndex++;
                }
                
                if(colorIndex > index)
                {
                    colorIndex = 0;
                }

                index++;

                model.List.Add(settingItems);
            }

            return model;
        }

        #endregion

        #region Methods.

        private void SetCategoryName(ListGeneralSettings settingItem, string text, string replacedText)
        {
            text = text.ToLower().Replace(replacedText.ToLower(), "");
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
                int index = settingItem.MachineName.LastIndexOf('.');
                subCategory = settingItem.MachineName.Substring(index + 1);
            }

            settingItem.Category = category[0].ToString().ToUpper() + category.Substring(1);
            settingItem.SubCategory = subCategory;
        }

        #endregion
    }
}