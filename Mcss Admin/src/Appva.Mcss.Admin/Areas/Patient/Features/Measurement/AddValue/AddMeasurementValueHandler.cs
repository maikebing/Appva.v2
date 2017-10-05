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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMeasurementValueHandler : RequestHandler<AddMeasurementValue, AddMeasurementValueModel>
    {
        #region Variables

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMeasurementValueHandler"/> class.
        /// </summary>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        public AddMeasurementValueHandler(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override AddMeasurementValueModel Handle(AddMeasurementValue message)
        {
            var observation = this.service.GetMeasurementObservation(message.MeasurementId);
            var unit = JsonConvert.DeserializeObject<IList<InventoryAmountListModel>>(observation.Scale)[0].Unit;

            return new AddMeasurementValueModel
            {
                MeasurementId = observation.Id,
                Name = observation.Name,
                Instruction = observation.Description,
                Unit = unit
            };
        }

        #endregion
    }
}