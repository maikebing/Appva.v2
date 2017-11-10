// <copyright file="AddMeasurementValue.cs" company="Appva AB">
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
    /// Class AddMeasurementValue.
    /// </summary>
    public class AddMeasurementValue : Identity<AddMeasurementValueModel>
    {
        #region Variables

        /// <summary>
        /// The measurement observation id
        /// </summary>
        public Guid MeasurementId { get; set; }

        #endregion
    }
}