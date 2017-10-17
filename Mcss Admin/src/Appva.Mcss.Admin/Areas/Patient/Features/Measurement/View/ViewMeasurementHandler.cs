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
    using Appva.Mcss.Admin.Infrastructure;
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

        /// <summary>
        /// The patient transformer
        /// </summary>
        private readonly IPatientTransformer patientTransformer;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMeasurementHandler"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        public ViewMeasurementHandler(IMeasurementService service, IPatientTransformer patientTransformer)
        {
            this.service = service;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ViewMeasurementModel Handle(ViewMeasurement message)
        {
            var model = new ViewMeasurementModel();
            model.Observation = this.service.GetMeasurementObservation(message.MeasurementId);
            model.Unit = MeasurementScale.GetUnitForScale(model.Observation.Scale);
            model.Longscale = MeasurementScale.GetNameForScale((MeasurementScale.Scale) Enum.Parse(typeof(MeasurementScale.Scale), model.Observation.Scale, true));
            model.ListModel = new ListMeasurementModel
            {
                Patient = this.patientTransformer.ToPatient(model.Observation.Patient),
                MeasurementList = this.service.GetMeasurementObservationsList(message.Id)
            };

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