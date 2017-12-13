// <copyright file="UpdateMeasurementPublisher.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// Class UpdateMeasurementPublisher.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.UpdateObservationModel, Appva.Mcss.Admin.Models.ListObservation}" />
    public class UpdateMeasurementPublisher : RequestHandler<UpdateObservationModel, ListObservation>
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

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementPublisher"/> class.
        /// </summary>
        /// <param name="measurementService">The measurement service.</param>
        /// <param name="taxonService">The taxon service.</param>
        public UpdateMeasurementPublisher(IObservationService observationService, ITaxonomyService taxonService)
        {
            this.observationService = observationService;
            this.taxonService       = taxonService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListObservation Handle(UpdateObservationModel message)
        {
            var observation = this.observationService.Get(message.ObservationId);
            if (observation == null)
            {
                throw new ArgumentNullException("observation", string.Format("The observation with ID: {0} does not exist.", message.ObservationId));
            }
            var delegation = this.taxonService.Get(Guid.Parse(message.SelectedDelegation));
            if (delegation == null)
            {
                observation.Update(message.Name, message.Instruction);
            }
            else
            {
                observation.Update(message.Name, message.Instruction, delegation);
            }
            //// UNRESOLVED: Fix Me!!!
            this.observationService.Update(observation);
            return new ListObservation
            {
                Id = message.Id,
                ObservationId = message.ObservationId
            };
        }

        #endregion
    }
}