// <copyright file="ReportStatistics.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Statistics.Data
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class StatisticsData : IRequest<ReportData>
    {
        /// <summary>
        /// Filter by patient, optional
        /// </summary>
        public Guid? Patient 
        { 
            get;
            set;
        }

        /// <summary>
        /// Filter by account, optional
        /// </summary>
        public Guid? Account
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by schedule, optional
        /// </summary>
        public Guid? ScheduleSetting
        {
            get;
            set;
        }

        /// <summary>
        /// The start date
        /// </summary>
        public DateTime Start
        {
            get;
            set;
        }

        /// <summary>
        /// The end date
        /// </summary>
        public DateTime End
        {
            get;
            set;
        }
    }
}