// <copyright file="MeasurementController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMeasurementValueHandler : RequestHandler<UpdateMeasurementValue, UpdateMeasurementValueModel>
    {
        #region Variables

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementValueHandler"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IMeasurementService"/>.</param>
        protected UpdateMeasurementValueHandler(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override UpdateMeasurementValueModel Handle(UpdateMeasurementValue message)
        {
            var item = this.service.GetValue(message.ValueId);

            return new UpdateMeasurementValueModel
            {
                ValueId = item.Id,
                Value = item.Measurement.Value,
                Comment = item.Comment.Content
            };
        }

        #endregion
    }
}