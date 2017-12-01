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
    /// <seealso cref="Appva.Cqrs.RequestHandler{Appva.Mcss.Admin.Models.UpdateMeasurementModel, Appva.Mcss.Admin.Models.ListMeasurement}" />
    public class UpdateMeasurementPublisher : RequestHandler<UpdateMeasurementModel, ListMeasurement>
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

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementPublisher"/> class.
        /// </summary>
        /// <param name="measurementService">The measurement service.</param>
        /// <param name="taxonService">The taxon service.</param>
        public UpdateMeasurementPublisher(IMeasurementService measurementService, ITaxonomyService taxonService)
        {
            this.measurementService = measurementService;
            this.taxonService       = taxonService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListMeasurement Handle(UpdateMeasurementModel message)
        {
            var observation = this.measurementService.Get(message.MeasurementId);
            if (observation == null)
            {
                throw new ArgumentNullException("observation", string.Format("The observation with ID: {0} does not exist.", message.MeasurementId));
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
            this.measurementService.Update(observation);
            return new ListMeasurement
            {
                Id = message.Id,
                MeasurementId = message.MeasurementId
            };
        }

        #endregion
    }
}