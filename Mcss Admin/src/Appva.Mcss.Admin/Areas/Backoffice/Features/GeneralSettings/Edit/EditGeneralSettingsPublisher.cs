// <copyright file="EditGeneralSettingsPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.GeneralSettings.Edit
{

    #region Imports.
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion
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

        #region RequestHandler Overrides.

        public override bool Handle(EditGeneralSettingsModel message)
        {
            var settings = this.settingsService.List();
            var setting = settings.Where(x => x.Id == message.Id).SingleOrDefault();



            if (message.intValue == null && message.stringValue == null)
            {
                setting.Update(setting.MachineName, setting.Namespace, setting.Name, setting.Description, Convert.ToString(message.boolValue));

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

            return false;
        }
        #endregion
    }
}