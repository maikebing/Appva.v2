// <copyright file="AddMeasurementValueHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMeasurementValuePublisher : RequestHandler<UpdateMeasurementValueModel, ViewMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        /// <summary>
        /// The account service
        /// </summary>
        private readonly IAccountService account;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementValuePublisher"/> class.
        /// </summary>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        /// <param name="account">The account service<see cref="IAccountService"/>.</param>
        protected UpdateMeasurementValuePublisher(IMeasurementService service, IAccountService account)
        {
            this.service = service;
            this.account = account;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ViewMeasurementModel Handle(UpdateMeasurementValueModel message)
        {
            var item = this.service.GetValue(message.ValueId);

            if (item.Measurement.Value != message.Value && HasValidValue(message.Value))
            {
                var measurement = new Measurement(message.Value, item.Measurement.Unit);
                var data = item.Signature.Data;
                data.Add(SignedData.New(new Base64Binary(message.Value)));
                var signature = new Signature(item.Signature.Type, this.account.CurrentPrincipal(), data);

                //item.Update(measurement, signature);
                //this.service.UpdateValue(item);
            }

            return new ViewMeasurementModel
            {
                Observation = this.service.GetMeasurementObservation(item.Observation.Id),
                Values = this.service.GetValueList(item.Observation.Id)
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