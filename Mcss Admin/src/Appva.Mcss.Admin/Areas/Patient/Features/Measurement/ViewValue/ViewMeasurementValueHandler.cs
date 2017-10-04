// <copyright file="CreateMeasurementPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ViewMeasurementValueHandler : RequestHandler<ViewMeasurementValue, ViewMeasurementValueModel>
    {
        #region Variables

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMeasurementValueHandler"/> class.
        /// </summary>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        protected ViewMeasurementValueHandler(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ViewMeasurementValueModel Handle(ViewMeasurementValue message)
        {
            var item = this.service.GetValue(message.ValueId);
            var observation = this.service.GetMeasurementObservation(item.Observation.Id);

            return new ViewMeasurementValueModel
            {
                ValueId = item.Id,
                Value = item.Measurement.Value,
                Unit = JsonConvert.DeserializeObject<InventoryAmountListModel>(observation.Scale).Unit
            };
        }

        #endregion
    }
}