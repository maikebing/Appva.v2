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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMeasurementPublisher : RequestHandler<UpdateMeasurementModel, ViewMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The Measurement Service
        /// </summary>
        private readonly IMeasurementService service;

        /// <summary>
        /// The Settings Service
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The Patient Transformer
        /// </summary>
        private readonly IPatientTransformer transformer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementPublisher"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        /// <param name="settings">The Settings Service<see cref="ISettingsService"/>.</param>
        /// <param name="transformer">The Patient Transformer<see cref="IPatientTransformer"/>.</param>
        public UpdateMeasurementPublisher(IMeasurementService service, ISettingsService settings, IPatientTransformer transformer)
        {
            this.service = service;
            this.settings = settings;
            this.transformer = transformer;
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override ViewMeasurementModel Handle(UpdateMeasurementModel message)
        {
            var observation = this.service.Get(message.MeasurementId);

            if (observation != null)
            {
                observation.Name = message.Name;
                observation.Description = message.Instruction;
                observation.Delegation = this.service.GetTaxon(Guid.Parse(message.SelectedDelegation));
                this.service.Update(observation);
            }

            return new ViewMeasurementModel
            {
                Observation = observation,
                Unit = JsonConvert.DeserializeObject<MeasurementScaleModel>(observation.Scale).Unit,
                ListModel = new ListMeasurementModel
                {
                    Patient = this.transformer.ToPatient(observation.Patient),
                    MeasurementList = this.service.GetMeasurementObservationsList(observation.Patient.Id)
                },
                Values = this.service.GetValueList(observation.Id)
            };
        }

        #endregion
    }
}