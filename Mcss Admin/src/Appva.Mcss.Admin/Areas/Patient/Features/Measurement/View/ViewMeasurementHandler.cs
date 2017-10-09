// <copyright file="MeasurementController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    public class ViewMeasurementHandler : RequestHandler<ViewMeasurement, ViewMeasurementModel>
    {
        #region Variables

        private readonly IMeasurementService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMeasurementHandler"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        public ViewMeasurementHandler(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ViewMeasurementModel Handle(ViewMeasurement message)
        {
            var model = new ViewMeasurementModel();
            model.Observation = this.service.GetMeasurementObservation(message.MeasurementId);
            model.Unit = JsonConvert.DeserializeObject<IList<MeasurementScaleModel>>(model.Observation.Scale)[0].Unit;
            if (message.StartDate == null && message.EndDate == null)
            {
                model.Values = this.service.GetValueList(model.Observation.Id);
            }
            else
            {
                var startDate = (DateTime)(message.StartDate > message.EndDate ? message.StartDate : message.EndDate);
                var endDate = (DateTime)(message.StartDate < message.EndDate ? message.StartDate : message.EndDate);
                
                model.Values = this.service.GetValueListByDate(model.Observation.Id, startDate, endDate);
            }

            return model;
        }

        #endregion
    }
}