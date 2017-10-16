// <copyright file="ObservationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mcss.Admin.Infrastructure;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMeasurementValuePublisher : RequestHandler<AddMeasurementValueModel, ViewMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The account service
        /// </summary>
        private readonly IAccountService account;

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        private readonly IPatientTransformer transformer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMeasurementValuePublisher"/> class.
        /// </summary>
        /// <param name="account">The account service<see cref="IAccountService"/>.</param>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        public AddMeasurementValuePublisher(IAccountService account, IMeasurementService service, IPatientTransformer transformer)
        {
            this.account = account;
            this.service = service;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ViewMeasurementModel Handle(AddMeasurementValueModel message)
        {
            var account = this.account.CurrentPrincipal();
            var observation = this.service.GetMeasurementObservation(message.MeasurementId);

            if (account != null && observation != null && MeasurementScale.HasValidValue(message.Value, message.Scale))
            {
                var taxon = observation.Delegation;
                var measurement = new Measurement(message.Value, taxon);
                var data = new List<SignedData> { SignedData.New(new Domain.VO.Base64Binary(message.Value)) };
                var signature = Signature.New(taxon, account, data);
                var value = ObservationItem.New(observation, measurement, signature);

                this.service.CreateValue(value);
            }

            return new ViewMeasurementModel
            {
                ListModel = new ListMeasurementModel { 
                    MeasurementList = this.service.GetMeasurementObservationsList(observation.Patient.Id),
                    Patient = transformer.ToPatient(observation.Patient)
                },
                Observation = observation,
                Values = this.service.GetValueList(observation.Id),
                Unit = JsonConvert.DeserializeObject<MeasurementScaleModel>(observation.Scale).Unit,
            };
        }

        #endregion

        #region Private members

        private static bool HasValidValue(string value)
        {
            var result = true;

            if (value == null || value == string.Empty || value.Trim() == "")
            {
                result = false;
            }

            return result;
        }

        #endregion
    }
}