// <copyright file="EditGeneralSettingsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

/*
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Areas.Backoffice.JsonObjects;
    using Appva.Mcss.Admin.Areas.Backoffice.JsonObjects.Pdf;

    #endregion

    internal sealed class EditGeneralSettingsHandler : RequestHandler<Identity<EditGeneralSettingsModel>, EditGeneralSettingsModel>
    {

        #region Properties.

        private readonly ISettingsService settingsService;
        private EditGeneralSettingsModel editSettings = new EditGeneralSettingsModel();
        private readonly string[] machineNames = new string[] {
            "MCSS.Core.Inventory.Units",
            "Mcss.Core.Pdf",
            "Mcss.Core.Security.Analytics.Audit.Configuration",
            "Mcss.Core.Security.Jwt.Configuration.SecurityToken",
            "Mcss.Core.Security.Messaging.Email",
            "Mcss.Integration.Ldap.LdapConfiguration"
        };

        #endregion

        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        #region Constructor.

        public EditGeneralSettingsHandler(ISettingsService settingsService)
        {
            this.settingsService = settingsService;

        }

        #endregion

        #region RequestHandler Overrides.

        public override EditGeneralSettingsModel Handle(Identity<EditGeneralSettingsModel> message)
        {
           


            var settings = this.settingsService.List();
            var setting = settings.Where(x => x.Id == message.Id).SingleOrDefault();

            PdfGenObject obj = JsonConvert.DeserializeObject<PdfGenObject>(setting.Value);

            editSettings.Name = setting.Name;
            editSettings.MachineName = setting.MachineName;
            editSettings.BoolValue = null;
            editSettings.IsJson = false;

            if (setting.Type == typeof(Boolean))
            {
                editSettings.BoolValue = Convert.ToBoolean(setting.Value);
            }

            if (setting.Type == typeof(String))
            {
                editSettings.StringValue = "";

                try
                {
                    if (setting.MachineName == machineNames[0])
                    {
                        editSettings.InventoryObject = JsonConvert.DeserializeObject<List<InventoryObject>>(setting.Value);
                    }
                    else if (setting.MachineName == machineNames[1])
                    {
                        editSettings.PdfGenObject = JsonConvert.DeserializeObject<PdfGenObject>(setting.Value);

                    }

                    editSettings.IsJson = true;
                }
                catch
                {
                    editSettings.StringValue = setting.Value;
                }
            }

            if (setting.Type == typeof(Int32))
            {
                editSettings.IntValue = Convert.ToInt32(setting.Value);
            }

            if(setting.Type == typeof(IList<double>))
            {
                List<double> list = null;

                try
                {
                    list = JsonConvert.DeserializeObject<List<double>>(setting.Value);
                }
                catch {}

                editSettings.ListValues = list;
            }

            return editSettings;
        }

        #endregion
    }
}
*/