﻿// <copyright file="ListGeneralSettingsHandler.cs" company="Appva AB">
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
            int index = 0, settingsIndex = 0;
            var settings = this.settingService.List();
            var model = new ListGeneralSettingsModel();
            model.List = new List<ListGeneralSettings>();

            foreach (var item in settings)
            {
                if (model.IgnoredSettings.Contains(item.MachineName))
                {
                    index = index > 0 ? index-- : index;
                    settingsIndex++;
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
                settingItems.CategoryStartHtml = "";
                settingItems.CategoryEndHtml = "";
                SetCategoryNames(settingItems, item.Namespace, "mcss.");

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
                    colorIndex++;
                
                if(colorIndex > index)
                    colorIndex = 0;

                if(index == 0 || (index > 0 && model.List.ElementAt(index - 1).Category != settingItems.Category))
                    settingItems.CategoryStartHtml = "<div id=\"setlist-content\">\r\n<h1>" + settingItems.Category + "</h1><ul id=\"setlist\">\r\n";

                if(settingsIndex < settings.Count() - 1 && SplitNamespaceString(settings.ElementAt(settingsIndex + 1).Namespace, "mcss.")[0].ToLower() != settingItems.Category.ToLower())
                    settingItems.CategoryEndHtml = "</ul>\r\n</div>\r\n";

                index++;
                settingsIndex++;
                model.List.Add(settingItems);
            }

            return model;
        }

        #endregion

        #region Methods.

        private void SetCategoryNames(ListGeneralSettings settingItem, string text, string replacedText)
        {
            string category = "", subCategory = "";
            var array = SplitNamespaceString(text, replacedText);
            category = array.Length > 0 ? array[0] : "";
            subCategory = array.Length > 1 ? string.Join("/", array.Skip(1).ToArray()) : "";

            if (subCategory != string.Empty && subCategory[subCategory.Length - 1] == '/')
                subCategory = subCategory.Substring(0, subCategory.Length - 1);

            if(subCategory == string.Empty)
            {
                int index = settingItem.MachineName.LastIndexOf('.');
                subCategory = settingItem.MachineName.Substring(index + 1);
            }

            settingItem.Category = category[0].ToString().ToUpper() + category.Substring(1);
            settingItem.SubCategory = subCategory;
        }

        private string[] SplitNamespaceString(string text, string replacedText)
        {
            return text.ToLower().Replace(replacedText.ToLower(), "").Split('.');
        }

        #endregion
    }
}