using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    internal sealed class EditGeneralSettingsHandler : RequestHandler<Identity<EditGeneralSettingsModel>, EditGeneralSettingsModel>
    {
        private readonly ISettingsService settingsService;

        public EditGeneralSettingsHandler(ISettingsService settingsService)
        {
            this.settingsService = settingsService;

        }
        public override EditGeneralSettingsModel Handle(Identity<EditGeneralSettingsModel> message)
        {

            var settings = this.settingsService.List();

            var settingType = this.settingsService.List().Select(x => x.Type).ToList();



            var setting = settings.Where(x => x.Id == message.Id).SingleOrDefault();


            if (setting.Type == typeof(Boolean))
                return new EditGeneralSettingsModel
                {
                    boolValue = Convert.ToBoolean(setting.Value),
                    Name = setting.Name
                };

            if (setting.Type == typeof(String))
                return new EditGeneralSettingsModel
                {
                    stringValue = setting.Value,
                    Name = setting.Name
                };

            if (setting.Type == typeof(Int32))
                return new EditGeneralSettingsModel
                {
                    intValue = Convert.ToInt32(setting.Value),
                    Name = setting.Name
                };

            if (setting.Type == typeof(IList<>))
                return new EditGeneralSettingsModel
                {
                    Name = setting.Name,
                    listValue = setting.Value.Split(',').Select(Int32.Parse).ToList()

                };
            else
                return new EditGeneralSettingsModel
                {
                    Id = setting.Id,
                    Name = setting.Name

                };

        }
    }
}