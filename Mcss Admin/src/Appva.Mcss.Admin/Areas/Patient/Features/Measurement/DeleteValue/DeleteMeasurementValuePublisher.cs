// <copyright file="DeleteMeasurementValuePublisher.cs" company="Appva AB">
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

    public class DeleteMeasurementValuePublisher : RequestHandler<DeleteMeasurementValueModel, ListMeasurement>
    {
        #region Variables

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        #endregion

        #region Constructor

        protected DeleteMeasurementValuePublisher(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListMeasurement Handle(DeleteMeasurementValueModel message)
        {
            var item = this.service.GetValue(message.ValueId);

            if (message.DeleteConfirmed == true && item != null)
            {
                this.service.DeleteValue(item);
            }

            return new ListMeasurement
            {
                Id = message.Id,
            };
        }

        #endregion
    }
}