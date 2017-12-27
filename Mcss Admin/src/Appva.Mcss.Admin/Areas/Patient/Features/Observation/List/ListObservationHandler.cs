// <copyright file="ListObservationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListObservationHandler : RequestHandler<ListObservation, ListObservationModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IObservationService"/>.
        /// </summary>
        private readonly IObservationService observationService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListObservationHandler"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="patientService">The patient service.</param>
        /// <param name="patientTransformer">The patient transformer.</param>
        public ListObservationHandler(IObservationService observationService, IPatientService patientService, IPatientTransformer patientTransformer)
        {
            this.observationService     = observationService;
            this.patientService         = patientService;
            this.patientTransformer     = patientTransformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListObservationModel Handle(ListObservation message)
        {
            var patient = this.patientService.Get(message.Id);
            Requires.NotNull(patient, "patient");
            var patientViewModel = this.patientTransformer.ToPatient(patient);
            var observationList = this.observationService.ListByPatient(patient.Id);
            if (message.ObservationId.IsEmpty() && observationList.IsEmpty())
            {
                return new ListObservationModel(patientViewModel, observationList);
            }
            var observation = message.ObservationId.IsEmpty() ? observationList.First() : observationList.Where(x => x.Id == message.ObservationId).SingleOrDefault();
            Requires.NotNull(observation, "observation");
            var observationItemList = this.observationService.ListMeasurements(observation.Id, message.StartDate, message.EndDate);
            return new ListObservationModel(observation, patientViewModel, observationList, observationItemList);
        }
        #endregion
    }
}