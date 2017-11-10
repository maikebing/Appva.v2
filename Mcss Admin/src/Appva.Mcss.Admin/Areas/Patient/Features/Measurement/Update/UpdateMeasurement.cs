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
        public Guid MeasurementId
        {
            get;
            set;
        }

        #endregion
    }
}