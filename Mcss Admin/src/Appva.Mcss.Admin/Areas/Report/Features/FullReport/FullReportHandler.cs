// <copyright file="FullReportHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FullReportHandler : RequestHandler<FullReport, FullReportModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IReportService"/>.
        /// </summary>
        private readonly IReportService reports;

        /// <summary>
        /// The <see cref="ITaskService"/>
        /// </summary>
        private readonly ITaskService tasks;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FullReportHandler"/> class.
        /// </summary>
        public FullReportHandler(IReportService reports, ITaskService tasks)
        {
            this.reports = reports;
            this.tasks = tasks;
        }

        #endregion

        #region RequestHandler overrides.

        public override FullReportModel Handle(FullReport message)
        {
            if (!message.Start.HasValue)
            {
                message.Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if(!message.End.HasValue)
            {
                message.End = message.Start.GetValueOrDefault().AddMonths(1).AddDays(-1);
            }
            return new FullReportModel
            {
                Tasks = tasks.List(),
                Report = reports.GetReportData(new ChartDataFilter { StartDate = message.Start.GetValueOrDefault(), EndDate = message.End.GetValueOrDefault() })
            };
        }

        #endregion
    }
}