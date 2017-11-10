// <copyright file="CreateMeasurementPublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateMeasurementPublisher : RequestHandler<CreateMeasurementModel, ListMeasurement>
    {
        #region Variables

        /// <summary>
        /// The Measurement Service
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMeasurementPublisher"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        public CreateMeasurementPublisher(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListMeasurement Handle(CreateMeasurementModel message)
        {
            var patient = this.service.GetPatient(message.Id);
            if (patient == null)
            {
                throw new ArgumentNullException();
            }
            
            this.service.CreateMeasurementObservation(MeasurementObservation.New(
                scale: message.SelectedScale,
                delegation: this.service.GetTaxon(Guid.Parse(message.SelectedDelegation)), 
                patient: patient, 
                name: message.Name, 
                description: message.Description));

            return new ListMeasurement
            {
                Id = message.Id,
            };
        }

        #endregion
    }
}