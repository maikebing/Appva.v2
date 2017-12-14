// <copyright file="CreateMeasurementPublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Validation;

    #endregion

    /// <summary>
    /// Class CreateMeasurementPublisher.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.CreateObservationModel, Appva.Mcss.Admin.Models.ListObservation}" />
    public class CreateMeasurementPublisher : RequestHandler<CreateObservationModel, ListObservation>
    {
        #region Variables.

        /// <summary>
        /// The Measurement Service
        /// </summary>
        private readonly IObservationService observationService;

        /// <summary>
        /// The taxon service
        /// </summary>
        private readonly ITaxonomyService taxonService;

        /// <summary>
        /// The patient service
        /// </summary>
        private readonly IPatientService patientService;


        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMeasurementPublisher"/> class.
        /// </summary>
        /// <param name="measurementService">The measurement service.</param>
        /// <param name="taxonService">The taxon service.</param>
        /// <param name="patientService">The patient service.</param>
        public CreateMeasurementPublisher(IObservationService observationService, ITaxonomyService taxonService, IPatientService patientService)
        {
            this.observationService = observationService;
            this.taxonService       = taxonService;
            this.patientService     = patientService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListObservation Handle(CreateObservationModel message)
        {
            var patient = this.patientService.Get(message.Id);
            Requires.NotNull(patient, "patient");
            Taxon delegation = null;
            if (message.SelectedDelegation.IsNotEmpty())
            {
                delegation = this.taxonService.Get(Guid.Parse(message.SelectedDelegation));
                Requires.NotNull(delegation, "delegation");
            }
            var observation = ObservationFactory.CreateNew(patient, message.Name, message.Description, message.SelectedScale, delegation);
            this.observationService.Create(observation);
            return new ListObservation(patient.Id, observation.Id);
        }

        #endregion
    }
}