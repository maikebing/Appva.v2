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

            return true;
        }
    }
}