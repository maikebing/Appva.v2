// <copyright file="UpdateMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMeasurement : Identity<UpdateMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The Measurement Observation Id.
        /// </summary>
        public Guid MeasurementObservationId
        {
            get;
            set;
        }

        #endregion
    }
}