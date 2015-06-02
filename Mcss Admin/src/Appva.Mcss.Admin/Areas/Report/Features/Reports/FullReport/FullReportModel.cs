// <copyright file="FullReportModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FullReportModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FullReportModel"/> class.
        /// </summary>
        public FullReportModel()
        {
        }

        #endregion

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PageableSet<Task> Tasks { get; set; }

        public ReportData Report { get; set; }

        public ScheduleSettings Scehdules { get; set; }

        public Guid Schedule { get; set; }
    }
}