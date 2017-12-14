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
    public class ListMeasurementHandler : RequestHandler<ListObservation, ListObservationModel>
    {
        #region Variables.

        /// <summary>
        /// The observation service
        /// </summary>
        private readonly IObservationService observationService;

        /// <summary>
        /// The observation item service
        /// </summary>
        private readonly IObservationItemService observationItemService;

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
        public ListMeasurementHandler(IObservationService observationService, IObservationItemService observationItemService, IPatientService patientService, IPatientTransformer patientTransformer)
        {
            this.observationService     = observationService;
            this.observationItemService = observationItemService;
            this.patientService         = patientService;
            this.patientTransformer     = patientTransformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListObservationModel Handle(ListObservation message)
        {
            var patient = this.patientService.Get(message.Id);
            if (patient == null)
            {
                throw new ArgumentNullException("patient", string.Format("Patient with ID: {0} does not exist.", message.Id));
            }
            if (message.ObservationId.IsNotEmpty())
            {
                var observation = this.observationService.Get(message.ObservationId);
                if (observation == null)
                {
                    throw new ArgumentNullException("observation", string.Format("Observation with ID: {0} does not exist.", message.ObservationId));
                }
                //// UNRESOLVED: TO DO!
                var tName = "";
                return new ListObservationModel
                {
                    Id                      = observation.Patient.Id,
                    ObservationId           = observation.Id,
                    PatientViewModel        = this.patientTransformer.ToPatient(observation.Patient),
                    ObservationList         = this.observationService.ListByPatient(observation.Patient.Id),
                    ObservationItemList     = this.observationItemService.List(observation.Id),
                    Unit                    = MeasurementScale.GetUnitForScale(observation.Scale),
                    LongScale               = MeasurementScale.GetNameForScale(observation.Scale),
                    Scale                   = tName,
                    Delegation              = observation.Delegation,
                    Name                    = observation.Name,
                    Instructions            = observation.Description
                };
            }
            return new ListObservationModel
            {
                PatientViewModel = this.patientTransformer.ToPatient(patient),
                ObservationList  = this.observationService.ListByPatient(patient.Id)
            };
        }

        #endregion
    }
}