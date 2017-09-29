using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class CreateMeasurementPublisher : RequestHandler<CreateMeasurementModel, ListMeasurementModel>
    {
        private readonly IMeasurementService service;
        private readonly IPatientTransformer transformer;
        private readonly ITaxonomyService taxonService;
        private readonly ISettingsService settings;

        public CreateMeasurementPublisher(IMeasurementService service, IPatientTransformer transformer, ITaxonomyService taxonService, ISettingsService settings)
        {
            this.service = service;
            this.transformer = transformer;
            this.taxonService = taxonService;
            this.settings = settings;
        }

        public override ListMeasurementModel Handle(CreateMeasurementModel message)
        {
            //// Fetch data, Create Object and Save to database
            var patient = this.service.GetPatient(message.Id);
            var delegation = this.taxonService.Get(Guid.Parse(message.SelectedDelegation));
            var scale = JsonConvert.SerializeObject(this.settings.Find(ApplicationSettings.InventoryUnitsWithAmounts).Where(x => x.Id == Guid.Parse(message.SelectedUnit)));

            var observation = MeasurementObservation.New(scale, delegation, patient, message.Name, message.Description);

            this.service.CreateMeasurementObservation(observation);

            return new ListMeasurementModel
            {
                Patient = this.transformer.ToPatient(patient),
                Names = new List<string> { "Vikt", "Längd", "P-Glukos", "Blodtryck" },
                MeasurementList = this.service.GetMeasurementCategories(message.Id)
            };
        }
    }
}