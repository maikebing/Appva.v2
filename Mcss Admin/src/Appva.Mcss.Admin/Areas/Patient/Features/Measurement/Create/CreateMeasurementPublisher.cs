﻿// <copyright file="CreateMeasurementPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Application.Models;

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

        /// <summary>
        /// The Patient Transformer
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The Settings Service
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMeasurementPublisher"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        /// <param name="transformer">The Patient Transformer<see cref="IPatientTransformer"/>.</param>
        /// <param name="settings">The settings service<see cref="ISettingsService"/>.</param>
        public CreateMeasurementPublisher(IMeasurementService service, IPatientTransformer transformer, ISettingsService settings)
        {
            this.service = service;
            this.transformer = transformer;
            this.settings = settings;
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