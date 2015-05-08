// <copyright file="Report.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Report
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        public Report()
        {
        }

        #endregion

        #region Properties.

        public DateTime StartDate
        {
            get;
            set;
        }
        public DateTime EndDate
        {
            get;
            set;
        }

        public double TasksOnTime
        {
            get;
            set;
        }
        public double TasksNotOnTime
        {
            get;
            set;
        }

        public double ComparedDateSpanTasksOnTime
        {
            get;
            set;
        }
        public double ComparedDateSpanTasksNotOnTime
        {
            get;
            set;
        }

        public double AverageDifferenceInTime
        {
            get;
            set;
        }
        public double ComparedAverageDifferenceInTime
        {
            get;
            set;
        }

        public ReportSearch<Task> Search
        {
            get;
            set;
        }

        #endregion

    }
}