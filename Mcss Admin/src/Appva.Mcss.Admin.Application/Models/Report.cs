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

        /// <summary>
        /// TODO: What is StartDate?
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is EndDate?
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is TasksOnTime?
        /// </summary>
        public double TasksOnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is TasksNotOnTime?
        /// </summary>
        public double TasksNotOnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is ComparedDateSpanTasksOnTime?
        /// </summary>
        public double ComparedDateSpanTasksOnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is ComparedDateSpanTasksNotOnTime?
        /// </summary>
        public double ComparedDateSpanTasksNotOnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is AverageDifferenceInTime?
        /// </summary>
        public double AverageDifferenceInTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is ComparedAverageDifferenceInTime?
        /// </summary>
        public double ComparedAverageDifferenceInTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: What is Search?
        /// </summary>
        public ReportSearch<Task> Search
        {
            get;
            set;
        }

        #endregion
    }
}