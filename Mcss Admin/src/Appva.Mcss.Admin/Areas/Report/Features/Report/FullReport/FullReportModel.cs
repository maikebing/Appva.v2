// <copyright file="FullReportModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Appva.Mcss.Admin.Domain;
    using Appva.Repository;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class FullReportModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FullReportModel"/> class.
        /// </summary>
        public FullReportModel()
        {
        }

        #endregion

        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public IPaged<Task> Tasks { get; set; }

        public ReportData Report { get; set; }

        public IList<ScheduleSettings> Schedules { get; set; }

        public Guid? ScheduleSetting { get; set; }
    }
}