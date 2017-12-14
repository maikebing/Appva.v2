﻿// <copyright file="UpdateMeasurementHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Validation;

    #endregion

    /// <summary>
    /// Class UpdateMeasurementHandler.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.UpdateObservation, Appva.Mcss.Admin.Models.UpdateObservationModel}" />
    public class UpdateMeasurementHandler : RequestHandler<UpdateObservation, UpdateObservationModel>
    {
        #region Variables.

        /// <summary>
        /// The MeasurementService
        /// </summary>
        private readonly IObservationService observationService;

        /// <summary>
        /// The delegation service
        /// </summary>
        private readonly IDelegationService delegationService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementHandler"/> class.
        /// </summary>
        /// <param name="measurementService">The measurement service.</param>
        /// <param name="delegationService">The delegation service.</param>
        public UpdateMeasurementHandler(IObservationService observationService, IDelegationService delegationService)
        {
            this.observationService = observationService;
            this.delegationService  = delegationService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override UpdateObservationModel Handle(UpdateObservation message)
        {
            var observation = this.observationService.Get(message.ObservationId);
            Requires.NotNull(observation, "observation");
            var selectDelegationList = this.delegationService.ListDelegationTaxons().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            var selectedDelegation = observation.Delegation != null ? observation.Delegation.Id.ToString() : null;
            return new UpdateObservationModel(observation, selectDelegationList, selectedDelegation);
        }

        #endregion
    }
}