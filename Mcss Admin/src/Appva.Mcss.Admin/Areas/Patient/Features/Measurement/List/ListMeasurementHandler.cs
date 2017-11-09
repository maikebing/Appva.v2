// <copyright file="ListMeasurementHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListMeasurementHandler : RequestHandler<ListMeasurement, ListMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        /// <summary>
        /// The patient transformer
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMeasurementHandler"/> class.
        /// </summary>
        /// <param name="settings">The settings service<see cref="ISettingsService"/>.</param>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        /// <param name="patientTransformer">The patient transformer<see cref="IPatientTransformer"/>.</param>
        public ListMeasurementHandler(IMeasurementService service, IPatientTransformer patientTransformer)
        {
            this.service = service;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override ListMeasurementModel Handle(ListMeasurement message)
        {
            

            if (message.MeasurementId == Guid.Empty)
            {
                var patient = this.service.GetPatient(message.Id);
                return new ListMeasurementModel
                {
                    PatientViewModel = this.patientTransformer.ToPatient(patient),
                    MeasurementList = this.service.GetMeasurementObservationsList(patient.Id)
                };
            }

            var observation = this.service.GetMeasurementObservation(message.MeasurementId);
            var model = new ListMeasurementModel();

            model.Id = message.Id;
            model.MeasurementId = message.MeasurementId;
            model.PatientViewModel = this.patientTransformer.ToPatient(observation.Patient);
            model.MeasurementList = this.service.GetMeasurementObservationsList(observation.Patient.Id);
            model.MeasurementValueList = this.service.GetValueList(observation.Id);
            model.MeasurementUnit = MeasurementScale.GetUnitForScale(observation.Scale);
            model.MeasurementLongScale = MeasurementScale.GetNameForScale(observation.Scale);
            model.MeasurementScale = observation.Scale;
            model.Delegation = observation.Delegation;
            model.MeasurementName = observation.Name;
            model.MeasurementInstructions = observation.Description;

            return model;
        }

        #endregion
    }
}