// <copyright file="AddMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion
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