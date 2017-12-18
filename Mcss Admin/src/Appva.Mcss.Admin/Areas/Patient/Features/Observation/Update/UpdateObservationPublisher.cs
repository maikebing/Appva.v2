// <copyright file="UpdateObservationPublisher.cs" company="Appva AB">
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
    using Validation;

    #endregion

    /// <summary>
    /// Class UpdateMeasurementPublisher.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.UpdateObservationModel, Appva.Mcss.Admin.Models.ListObservation}" />
    public class UpdateObservationPublisher : RequestHandler<UpdateObservationModel, ListObservation>
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
        /// Initializes a new instance of the <see cref="UpdateObservationPublisher"/> class.
        /// </summary>
        /// <param name="measurementService">The measurement service.</param>
        /// <param name="taxonService">The taxon service.</param>
        public UpdateObservationPublisher(IObservationService observationService, ITaxonomyService taxonService)
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
            Requires.NotNull(observation, "observation");
            observation.Update(message.Name, message.Instruction, this.taxonService.Get(Guid.Parse(message.SelectedDelegation)));
            this.observationService.Update(observation);
            return new ListObservation(message.Id, message.ObservationId);
        }

        #endregion
    }
}