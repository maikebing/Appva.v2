// <copyright file="DeleteMeasurementValueHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    public class DeleteMeasurementValueHandler : RequestHandler<DeleteMeasurementValue, DeleteMeasurementValueModel>
    {
        #region RequestHandler overrides

        public override DeleteMeasurementValueModel Handle(DeleteMeasurementValue message)
        {
            return new DeleteMeasurementValueModel
            {
                ValueId = message.ValueId
            };
        }

        #endregion
    }
}