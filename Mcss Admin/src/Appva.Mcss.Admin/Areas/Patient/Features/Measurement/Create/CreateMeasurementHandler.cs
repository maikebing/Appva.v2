using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class CreateMeasurementHandler : RequestHandler<CreateMeasurement, CreateMeasurementModel>
    {
        private readonly IMeasurementService service;
        private readonly ISettingsService settings;
        private readonly IDelegationService delegations;

        public CreateMeasurementHandler(IMeasurementService service, ISettingsService settings, IDelegationService delegations)
        {
            this.service = service;
            this.settings = settings;
            this.delegations = delegations;
        }

        public override CreateMeasurementModel Handle(CreateMeasurement message)
        {
            //var delegation = this.delegations.
            var model = new CreateMeasurementModel();
            model.Id = message.Id;
            model.SelectUnitList = this.settings.Find(ApplicationSettings.InventoryUnitsWithAmounts)
                .Select(x => new SelectListItem {
                    Text = String.Format("{0} ({1})", x.Name, x.Unit),
                    Value = x.Id.ToString()
                });

            model.SelectDelegationList = this.delegations.ListDelegationTaxons()
                .Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            return model;
        }
    }
}