// <copyright file="ViewTenaMeasurements.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ViewTenaMeasurements : Identity<ViewTenaMeasurementsModel>
    {
        /// <summary>
        /// Tena Observation Period ID
        /// </summary>
        public Guid PeriodId
        {
            get;
            set;
        }
    }
}