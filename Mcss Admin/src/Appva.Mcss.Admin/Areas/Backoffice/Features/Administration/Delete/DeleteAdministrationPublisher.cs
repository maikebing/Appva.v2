using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Mcss.Admin.Models;

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Administration.Delete
{
    internal sealed class DeleteAdministrationPublisher : RequestHandler<DeleteAdministrationModel, Parameterless<ListInventoriesModel>>
    {
        private readonly ISettingsService settingsService;

        public DeleteAdministrationPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public override Parameterless<ListInventoriesModel> Handle(DeleteAdministrationModel message)
        {
            var settings = this.settingsService.Find<List<AdministrationValueModel>>(ApplicationSettings.AdministrationUnitsWithAmounts);
            var administration = settings.Where(x => x.Id == message.Id).FirstOrDefault();

            settings.Remove(administration);

            this.settingsService.Upsert<List<AdministrationValueModel>>(ApplicationSettings.AdministrationUnitsWithAmounts, settings);

            return new Parameterless<ListInventoriesModel>();
        }
    }
}