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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// Class CreateMeasurementPublisher.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.CreateMeasurementModel, Appva.Mcss.Admin.Models.ListMeasurement}" />
    public class CreateMeasurementPublisher : RequestHandler<CreateMeasurementModel, ListMeasurement>
    {
        #region Variables.

        /// <summary>
        /// The Measurement Service
        /// </summary>
        private readonly IMeasurementService measurementService;

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
        public CreateMeasurementPublisher(IMeasurementService measurementService, ITaxonomyService taxonService, IPatientService patientService)
        {
            this.measurementService = measurementService;
            this.taxonService       = taxonService;
            this.patientService     = patientService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListMeasurement Handle(CreateMeasurementModel message)
        {
            var patient = this.patientService.Get(message.Id);
            if (patient == null)
            {
                throw new ArgumentNullException("patient", string.Format("Patient with ID: {0} does not exist.", message.Id));
            }
            Taxon delegation = null;
            if (string.IsNullOrEmpty(message.SelectedDelegation) == false)
            {
                delegation = this.taxonService.Get(Guid.Parse(message.SelectedDelegation));
                if (delegation == null)
                {
                    throw new ArgumentNullException("delegation", string.Format("Delegation with ID: {0} does not exist.", message.SelectedDelegation));
                }
            }
            this.measurementService.Create(patient, message.Name, message.Description, message.SelectedScale, delegation);
            return new ListMeasurement
            {
                Id = message.Id,
            };
        }

        #endregion
    }
}