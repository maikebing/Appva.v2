// <copyright file="ListMeasurementHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListMeasurementHandler : RequestHandler<ListMeasurement, ListMeasurementModel>
    {
        #region Variables.

        /// <summary>
        /// The measurement service.
        /// </summary>
        private readonly IMeasurementService measurementService;

        /// <summary>
        /// The patient service.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The patient transformer.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMeasurementHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="patientService">The patient service.</param>
        /// <param name="patientTransformer">The patient transformer.</param>
        public ListMeasurementHandler(IMeasurementService measurementService, IPatientService patientService, IPatientTransformer patientTransformer)
        {
            this.measurementService = measurementService;
            this.patientService     = patientService;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListMeasurementModel Handle(ListMeasurement message)
        {
            var patient = this.patientService.Get(message.Id);
            if (patient == null)
            {
                throw new ArgumentNullException("patient", string.Format("Patient with ID: {0} does not exist", message.Id));
            }
            if (message.MeasurementId.IsNotEmpty())
            {
                var observation = this.measurementService.Get(message.MeasurementId);
                return new ListMeasurementModel
                {
                    Id                      = observation.Patient.Id,
                    MeasurementId           = observation.Id,
                    PatientViewModel        = this.patientTransformer.ToPatient(observation.Patient),
                    MeasurementList         = this.measurementService.ListByPatient(observation.Patient.Id),
                    MeasurementValueList    = this.measurementService.GetValueList(observation.Id),
                    MeasurementUnit         = MeasurementScale.GetUnitForScale(observation.Scale),
                    MeasurementLongScale    = MeasurementScale.GetNameForScale(observation.Scale),
                    MeasurementScale        = observation.Scale,
                    Delegation              = observation.Delegation,
                    MeasurementName         = observation.Name,
                    MeasurementInstructions = observation.Description
                };
            }
            return new ListMeasurementModel
            {
                PatientViewModel = this.patientTransformer.ToPatient(patient),
                MeasurementList  = this.measurementService.ListByPatient(patient.Id)
            };
        }

        #endregion
    }
}