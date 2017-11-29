// <copyright file="UpdateMeasurementPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMeasurementPublisher : RequestHandler<UpdateMeasurementModel, ListMeasurement>
    {
        #region Variables

        /// <summary>
        /// The Measurement Service
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementPublisher"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        public UpdateMeasurementPublisher(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override ListMeasurement Handle(UpdateMeasurementModel message)
        {
            var observation = this.service.Get(message.MeasurementId);

            if (observation != null)
            {
                //// UNRESOLVED: Change me!!
                /*observation.Name = message.Name;
                observation.Description = message.Instruction;
                observation.Delegation = this.service.GetTaxon(Guid.Parse(message.SelectedDelegation));
                this.service.Update(observation);*/
            }

            return new ListMeasurement
            {
                Id = message.Id,
                MeasurementId = message.MeasurementId
            };
        }

        #endregion
    }
}