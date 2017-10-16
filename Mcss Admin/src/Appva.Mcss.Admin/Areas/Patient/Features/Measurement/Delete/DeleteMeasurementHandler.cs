using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    #endregion

    public class DeleteMeasurementHandler : RequestHandler<DeleteMeasurement, DeleteMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The IMeasurementsService
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region Constructor

        public DeleteMeasurementHandler(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region Handler overrides

        public override DeleteMeasurementModel Handle(DeleteMeasurement message)
        {
            return new DeleteMeasurementModel
            {
                MeasurementId = message.MeasurementId
            };
        }

        #endregion
    }
}