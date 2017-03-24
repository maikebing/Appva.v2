using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.GeneralSettings.Edit
{
    public class EditGeneralSettingsPublisher : RequestHandler<EditGeneralSettingsModel, bool>
    {
        #region Properties.

        ISettingsService settingsService;
        #endregion

        #region Constructor.
        public EditGeneralSettingsPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }
        #endregion

        public override bool Handle(EditGeneralSettingsModel message)
        {
            var settings = this.settingsService.List();
            var setting = settings.Where(x => x.Id == message.Id).SingleOrDefault();



            if (message.boolValue != null)
            {
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(message.boolValue));

                //this.settingsService.Upsert(setting.MachineName, message.boolValue);
                return true;
            }

            if (message.intValue != null)
            {
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(message.intValue));

                return true;
            }

            if (message.stringValue != null)
            {
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(message.stringValue));

                return true;
            }

            if (message.listValue != null)
            {
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(message.listValue));

               //this.settingsService.Upsert(setting, )

                return true;
            }
            // setting.Update(message.)
            //this.settingsService.Upsert(setting.MachineName, )

            return false;
        }
    }
}