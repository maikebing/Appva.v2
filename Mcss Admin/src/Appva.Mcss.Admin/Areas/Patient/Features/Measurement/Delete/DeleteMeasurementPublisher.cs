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

        protected DeleteMeasurementPublisher(IMeasurementService service, IPatientTransformer transformer)
        {
            this.service = service;
            this.transformer = transformer;
        }

        public override ListMeasurementModel Handle(DeleteMeasurementModel message)
        {
            var patientViewModel = new PatientViewModel();

            if (message.Observation.Equals(this.service.GetMeasurementObservation(message.Observation.Id)))
            {
                patientViewModel = this.transformer.ToPatient(message.Observation.Patient);
                this.service.DeleteMeasurementObservation(message.Observation);
            }

            return new ListMeasurementModel
            {
                Patient = patientViewModel,
                MeasurementList = this.service.GetMeasurementObservationsList(message.Id)
            };
        }
    }
}