using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class DeleteMeasurementPublisher : RequestHandler<DeleteMeasurementModel, ListMeasurementModel>
    {
        private readonly IMeasurementService service;
        private readonly IPatientTransformer transformer;

        public DeleteMeasurementPublisher(IMeasurementService service, IPatientTransformer transformer)
        {
            this.service = service;
            this.transformer = transformer;
        }

        public override ListMeasurementModel Handle(DeleteMeasurementModel message)
        {
            var patientViewModel = new PatientViewModel();
            var observation = this.service.GetMeasurementObservation(message.MeasurementId);

            if (observation != null)
            {
                this.service.DeleteMeasurementValueList(observation.Items);

                patientViewModel = this.transformer.ToPatient(observation.Patient);
                this.service.DeleteMeasurementObservation(observation);
            }

            return new ListMeasurementModel
            {
                Patient = patientViewModel,
                MeasurementList = this.service.GetMeasurementObservationsList(patientViewModel.Id)
            };
        }
    }
}