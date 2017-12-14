﻿// <copyright file="AddMeasurementValuePublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Domain.Unit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMeasurementValuePublisher : RequestHandler<AddObservationValueModel, ListObservation>
    {
        #region Variables.

        /// <summary>
        /// The account service
        /// </summary>
        private readonly IAccountService accountService;

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
        /// Initializes a new instance of the <see cref="AddMeasurementValuePublisher"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        /// <param name="observationService">The observation service.</param>
        /// <param name="observationItemService">The observation item service.</param>
        public AddMeasurementValuePublisher(IAccountService accountService, IObservationService observationService, IObservationItemService observationItemService)
        {
            this.accountService         = accountService;
            this.observationService     = observationService;
            this.observationItemService = observationItemService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListObservation Handle(AddObservationValueModel message)
        {
            var observation = this.observationService.Get(message.ObservationId);
            if (observation == null)
            {
                throw new ArgumentNullException("observation", string.Format("MesurementObservation with ID: {0} does not exist.", message.ObservationId));
            }

            //// UNRESOLVED: move validation to controller.
            //// UNRESOLVED: make sure to validate correctly.

            //// UNRESOLVED: Do more!!!
            //var value = Activator.CreateInstance(observation.ScaleType, message.Value) as IUnit;

            //object value;


            //switch(observation.GetType().Name)
            //{
            //    case "BristolObservation": value = Activator.CreateInstance() break;
            //    case "FecesObservation": break;
            //    case "WeightObservation": break;
            //    default: throw new ArgumentOutOfRangeException();
            //}

            //// UNRESOLVED: 
            //this.observationItemService.Create(observation, this.accountService.CurrentPrincipal(), value);

            return new ListObservation
            {
                Id            = observation.Patient.Id,
                ObservationId = observation.Id
            };
        }

        #endregion
    }
}