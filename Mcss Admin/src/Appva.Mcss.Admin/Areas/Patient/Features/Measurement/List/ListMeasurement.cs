// <copyright file="ListMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListMeasurement : IRequest<ListMeasurementModel>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the measurement identifier.
        /// </summary>
        public Guid MeasurementId
        {
            get;
            set;
        }
    }
}