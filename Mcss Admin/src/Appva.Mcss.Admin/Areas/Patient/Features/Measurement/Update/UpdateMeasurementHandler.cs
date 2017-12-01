// <copyright file="UpdateMeasurementHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// Class UpdateMeasurementHandler.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.UpdateMeasurement, Appva.Mcss.Admin.Models.UpdateMeasurementModel}" />
    public class UpdateMeasurementHandler : RequestHandler<UpdateMeasurement, UpdateMeasurementModel>
    {
        #region Variables.

        /// <summary>
        /// The MeasurementService
        /// </summary>
        private readonly IMeasurementService measurementService;

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
        public UpdateMeasurementHandler(IMeasurementService measurementService, IDelegationService delegationService)
        {
            this.measurementService = measurementService;
            this.delegationService  = delegationService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override UpdateMeasurementModel Handle(UpdateMeasurement message)
        {
            var observation = this.measurementService.Get(message.MeasurementId);
            if (observation == null)
            {
                throw new ArgumentNullException("observation", string.Format("The observation with ID: {0} does not exist.", message.MeasurementId));
            }
            return new UpdateMeasurementModel
            {
                MeasurementId        = observation.Id,
                Name                 = observation.Name,
                Instruction          = observation.Description,
                SelectedScale        = MeasurementScale.GetNameForScale(observation.Scale),
                SelectedUnit         = MeasurementScale.GetUnitForScale(observation.Scale),
                SelectDelegationList = this.delegationService.ListDelegationTaxons().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
        }

        #endregion
    }
}