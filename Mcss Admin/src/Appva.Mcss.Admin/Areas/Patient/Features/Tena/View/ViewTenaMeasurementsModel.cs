// <copyright file="ViewTenaMeasurementsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ViewTenaMeasurementsModel
    {
        /// <summary>
        /// Tena Observation Period ID
        /// </summary>
        public TenaObservationPeriod ObservationPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// List of Tena Observation Measurements
        /// </summary>
        public List<ObservationItem> ObservationItems
        {
            get;
            set;
        }
    }
}