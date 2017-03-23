using Appva.Cqrs;
using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    internal sealed class EditGeneralSettingsHandler : RequestHandler<Identity<EditGeneralSettingsModel>, EditGeneralSettingsModel>
    {
        public override EditGeneralSettingsModel Handle(Identity<EditGeneralSettingsModel> message)
        {
            return new EditGeneralSettingsModel
            {

            };
        }
    }
}