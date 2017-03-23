using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    internal sealed class ListGeneralSettingsHandler : RequestHandler<Parameterless<ListGeneralSettingsModel>, ListGeneralSettingsModel>
    {
        ISettingsService settingService;

        public ListGeneralSettingsHandler(ISettingsService settingService)
        {
            this.settingService = settingService;
        }

        public override ListGeneralSettingsModel Handle(Parameterless<ListGeneralSettingsModel> message)
        {
            var settings = this.settingService.List();
            var setting = new ListGeneralSettingsModel();

            setting.List = settings;
            return setting;
        }
    }
}