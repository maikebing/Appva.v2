using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{

    public class DeleteMeasurementHandler : RequestHandler<DeleteMeasurement, DeleteMeasurementModel>
    {
        private readonly IMeasurementService service;

        public DeleteMeasurementHandler(IMeasurementService service)
        {
            this.service = service;
        }

        public override DeleteMeasurementModel Handle(DeleteMeasurement message)
        {
            return new DeleteMeasurementModel
            {
                Observation = this.service.GetMeasurementObservation(message.MeasurementId)
            };
        }
    }
}