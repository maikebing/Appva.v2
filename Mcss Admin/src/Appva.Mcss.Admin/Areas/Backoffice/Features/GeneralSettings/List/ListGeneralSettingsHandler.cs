// <copyright file="ListGeneralSettingsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    using Admin.Models;
    #region Imports.
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using JsonObjects.Pdf;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
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
            "Mcss.Integration.Ldap.LdapConfiguration"
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

        private static String HexConverter(int r, int g, int b)
        {
            return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

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
                settingItems.Category = ToCategoryString(item.Set.MachineName, 35);
                settingItems.Description = item.Set.Description;
                settingItems.Value = item.Set.Value;
                settingItems.Type = item.Set.Type;
                settingItems.CategoryColorCode = colorCodes[colorIndex];
                settingItems.ItemColorCodes = new string[2];
                settingItems.ItemId = "item" + item.Index;

                if (item.Set.Type == typeof(Boolean))
                {
                    settingItems.BoolValue = Convert.ToBoolean(item.Set.Value);
                }

                if (item.Set.Type == typeof(String))
                {
                    settingItems.StringValue = "";

                    try
                    {
                        if (item.Set.MachineName == machineNames[0])
                        {
                            //settingItems.InventoryObject = JsonConvert.DeserializeObject<List<InventoryObject>>(setting.Value);
                        }
                        else if (item.Set.MachineName == machineNames[1])
                        {
                            settingItems.PdfGenObject = JsonConvert.DeserializeObject<PdfGenObject>(item.Set.Value);
                            setting.Colors = new PdfGenColors
                            {
                                BackgroundColor = HexConverter(settingItems.PdfGenObject.BackgroundColor.R, settingItems.PdfGenObject.BackgroundColor.G, settingItems.PdfGenObject.BackgroundColor.B ),
                                FontColor = HexConverter(settingItems.PdfGenObject.FontColor.R, settingItems.PdfGenObject.FontColor.G, settingItems.PdfGenObject.FontColor.B),
                                TableBorderColor = HexConverter(settingItems.PdfGenObject.TableBorderColor.R, settingItems.PdfGenObject.TableBorderColor.G, settingItems.PdfGenObject.TableBorderColor.B),
                                TableHeaderColor = HexConverter(settingItems.PdfGenObject.TableHeaderColor.R, settingItems.PdfGenObject.TableHeaderColor.G, settingItems.PdfGenObject.TableHeaderColor.B)
                            };
                        }

                        settingItems.IsJson = true;
                    }
                    catch
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

                if (item.Index % 2 == 0)
                {
                    settingItems.ItemColorCodes[0] = "#81C784";
                    settingItems.ItemColorCodes[1] = "#66BB6A";
                }
                else
                {
                    settingItems.ItemColorCodes[0] = "#7986CB";
                    settingItems.ItemColorCodes[1] = "#5C6BC0";
                }

                if (item.Index < settings.Count() - 1 && settings.ElementAt(item.Index + 1).MachineName != item.Set.MachineName)
                {
                    colorIndex++;
                }
                
                if(colorIndex > colorCodes.Length - 1)
                {
                    colorIndex = 0;
                }

                setting.List.Add(settingItems);
            }

            setting.List = setting.List;
            return setting;
        }

        #endregion

        private string ToCategoryString(string s, int max)
        {
            var array = s.Split('.');
            string category = "";

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].ToLower() != "mcss")
                {
                    category += array[i] + (i > 0 && i < array.Length - 1 ? " / " : "");
                }
            }

            if (category.Length > max)
            {
                category = category.Substring(category.Length - max).Trim();
                int index = category.IndexOf(' ');
                category = category.Contains("/") && category[0] != '/' ? "..." + category.Remove(0, index) : "... " + category;
            }

            return category;
        }
    }
}