// <copyright file="AddMeasurementValueHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMeasurementValueHandler : RequestHandler<AddObservationValue, AddObservationValueModel>
    {
        #region Variables.

        /// <summary>
        /// The observation service
        /// </summary>
        private readonly IObservationService observationService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMeasurementValueHandler"/> class.
        /// </summary>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        public AddMeasurementValueHandler(IObservationService observationService)
        {
            this.observationService = observationService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override AddObservationValueModel Handle(AddObservationValue message)
        {
            var observation = this.observationService.Get(message.ObservationId);
            if (observation == null)
            {
                throw new ArgumentNullException("observation", string.Format("Observation with ID: {0} does not exist.", message.ObservationId));
            }

            //// UNRESOLVAED: Stop using MeasurementScale
            return new AddObservationValueModel
            {
                ObservationId = observation.Id,
                Name          = observation.Name,
                Instruction   = observation.Description,
                Unit          = MeasurementScale.GetUnitForScale(observation.Scale), // observation.GetUnit()
                Scale         = observation.Scale,
                LongScale     = MeasurementScale.GetNameForScale(observation.Scale) // obsevation.GetName()
            };
        }

        #endregion
    }
}