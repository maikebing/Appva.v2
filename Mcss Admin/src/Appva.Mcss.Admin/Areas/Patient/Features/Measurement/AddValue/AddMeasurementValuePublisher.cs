// <copyright file="AddMeasurementValuePublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Domain.Unit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMeasurementValuePublisher : RequestHandler<AddMeasurementValueModel, ListMeasurement>
    {
        #region Variables.

        /// <summary>
        /// The account service
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService measurementService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMeasurementValuePublisher"/> class.
        /// </summary>
        /// <param name="account">The account service<see cref="IAccountService"/>.</param>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        public AddMeasurementValuePublisher(IAccountService accountService, IMeasurementService measurementService)
        {
            this.accountService     = accountService;
            this.measurementService = measurementService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListMeasurement Handle(AddMeasurementValueModel message)
        {
            var observation = this.measurementService.Get(message.MeasurementId);
            if (observation == null)
            {
                throw new ArgumentNullException("observation", string.Format("MesurementObservation with ID: {0} does not exist.", message.MeasurementId));
            }

            //// UNRESOLVED: move validation to controller.
            //// UNRESOLVED: make sure to validate correctly.
            var value = Activator.CreateInstance(observation.ScaleType, message.Value) as IUnit;

            this.measurementService.CreateValue(observation, this.accountService.CurrentPrincipal(), value);

            return new ListMeasurement
            {
                Id            = observation.Patient.Id,
                MeasurementId = observation.Id
            };
        }

        #endregion
    }
}