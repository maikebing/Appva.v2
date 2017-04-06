// <copyright file="EditGeneralSettingsPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

/*
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.GeneralSettings.Edit
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Application.Common;
    using System.Drawing;

    #endregion

    public class EditGeneralSettingsPublisher : RequestHandler<EditGeneralSettingsModel, bool>
    {
        #region Properties.
        ISettingsService settingsService;


        #endregion

        private static String RGBConverter(System.Drawing.Color c)
        {
            return c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString();
        }

        #region Constructor.
        public EditGeneralSettingsPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }
        #endregion

        #region RequestHandler Overrides.
        public override bool Handle(EditGeneralSettingsModel message)
        {
            var settings = this.settingsService.List();
            var setting = settings.Where(x => x.Id == message.Id).SingleOrDefault();
            var color = System.Drawing.ColorTranslator.FromHtml(message.BackgroundColor);

            string[] RGB = RGBConverter(color).Split(',');
            int R = Convert.ToInt32(RGB[0]);
            int G = Convert.ToInt32(RGB[1]);
            int B = Convert.ToInt32(RGB[2]);
            


            if (message.IntValue == null && message.StringValue == null)
            {
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(message.BoolValue));

                return true;
            }

            if (message.IntValue != null)
            {
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(message.IntValue));

                return true;
            }

            if (message.StringValue != null)
            {

                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, message.StringValue.ToString());

                return true;
            }

            return false;
        }

        #endregion
    }
}
*/