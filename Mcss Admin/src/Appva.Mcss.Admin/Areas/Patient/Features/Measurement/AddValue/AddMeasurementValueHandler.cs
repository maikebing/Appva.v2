﻿// <copyright file="AddMeasurementValueHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMeasurementValueHandler : RequestHandler<AddMeasurementValue, AddMeasurementValueModel>
    {
        #region Variables.

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMeasurementValueHandler"/> class.
        /// </summary>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        public AddMeasurementValueHandler(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override AddMeasurementValueModel Handle(AddMeasurementValue message)
        {
            var observation = this.service.Get(message.MeasurementId);
            if (observation == null)
            {
                throw new ArgumentNullException("observation", string.Format("MeasurementObservation with ID: {0} does not exist.", message.MeasurementId));
            }            
            return new AddMeasurementValueModel
            {
                MeasurementId = observation.Id,
                Name          = observation.Name,
                Instruction   = observation.Description,
                Unit          = MeasurementScale.GetUnitForScale(observation.Scale),
                Scale         = observation.Scale,
                LongScale     = MeasurementScale.GetNameForScale(observation.Scale)
            };
        }

        #endregion
    }
}