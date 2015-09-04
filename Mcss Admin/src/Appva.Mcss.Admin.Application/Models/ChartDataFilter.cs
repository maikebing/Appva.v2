// <copyright file="ChartDataFilter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ChartDataFilter
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartDataFilter"/> class.
        /// </summary>
        public ChartDataFilter()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Filter by organisation, optional
        /// </summary>
        public Guid? Organisation
        {
            get; 
            set;
        }

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
        /// The startdate for the chart
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The enddate for the chart
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// If calendar events shall be included in report or not
        /// </summary>
        public bool IncludeCalendarTasks
        {
            get;
            set;
        }

        #endregion
    }
}