// <copyright file="AddMeasurementValuePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMeasurementValuePublisher : RequestHandler<AddMeasurementValueModel, ListMeasurement>
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMeasurementValuePublisher"/> class.
        /// </summary>
        /// <param name="account">The account service<see cref="IAccountService"/>.</param>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        public AddMeasurementValuePublisher(IAccountService account, IMeasurementService service)
        {
            this.account = account;
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListMeasurement Handle(AddMeasurementValueModel message)
        {
            var observation = this.service.GetMeasurementObservation(message.MeasurementId);

            if (MeasurementScale.HasValidValue(message.Value, observation.Scale))
            {
                if (observation.Scale == MeasurementScale.Scale.Common.ToString())
                {
                    message.Value = MeasurementScale.GetCommonScaleValue(message.Value);
                }
                var measurement = new Measurement(message.Value);
                var data = new List<SignedData> { SignedData.New(new Domain.VO.Base64Binary(message.Value)) };
                var signature = Signature.New(this.account.CurrentPrincipal(), data);

                this.service.CreateValue(ObservationItem.New(observation, measurement, signature: signature));
            }

            return new ListMeasurement
            {
                Id = observation.Patient.Id,
                MeasurementId = observation.Id
            };
        }

        #endregion
    }
}