// <copyright file="AddObservationValuePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Validation;

    #endregion

    /// <summary>
    /// Class AddMeasurementValuePublisher.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.AddObservationValueModel, Appva.Mcss.Admin.Models.ListObservation}" />
    public class AddObservationValuePublisher : RequestHandler<AddObservationValueModel, ListObservation>
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

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddObservationValuePublisher"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        /// <param name="observationService">The observation service.</param>
        /// <param name="observationItemService">The observation item service.</param>
        public AddObservationValuePublisher(IObservationService observationService, IObservationItemService observationItemService)
        {
            this.observationService     = observationService;
            this.observationItemService = observationItemService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListObservation Handle(AddObservationValueModel message)
        {
            var observation = this.observationService.Get(message.ObservationId);
            Requires.NotNull(observation, "observation");
            this.observationItemService.Create(observation, message.Value);
            return new ListObservation(observation.Patient.Id, observation.Id);
        }

        #endregion
    }
}