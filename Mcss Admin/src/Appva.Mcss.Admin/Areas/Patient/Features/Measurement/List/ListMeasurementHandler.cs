using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Admin.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class ListMeasurementsHandler : RequestHandler<ListMeasurement, ListMeasurementModel>
    {
        private ISettingsService settings;
        private readonly IMeasurementService service;
        private readonly IPatientTransformer patientTransformer;

        public ListMeasurementsHandler(ISettingsService settings, IMeasurementService service, IPatientTransformer patientTransformer)
        {
            this.settings = settings;
            this.service = service;
            this.patientTransformer = patientTransformer;
        }

        public override ListMeasurementModel Handle(ListMeasurement message)
        {
            var measurementList = this.service.GetMeasurementCategories(message.Id);

            return new ListMeasurementModel
            {
                Patient = this.patientTransformer.ToPatient(this.service.GetPatient(message.Id)),
                Names = new List<string> { "Vikt", "Längd", "P-Glukos", "Blodtryck" },
                MeasurementList = measurementList
            };
        }
    }
}