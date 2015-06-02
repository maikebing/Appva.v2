﻿// <copyright file="ReportStatisticsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Statistics.Data
{
    #region Imports.

    using Appva.Caching.Providers;
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
    internal sealed class StatisticsDataHandler : RequestHandler<StatisticsData, ReportData>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IReportService"/>.
        /// </summary>
        private readonly IReportService reports;

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsDataHandler"/> class.
        /// </summary>
        public StatisticsDataHandler(IReportService reports, IRuntimeMemoryCache cache)
        {
            this.reports = reports;
            this.cache = cache;
        }

        #endregion

        #region RequestHandler overrides

        public override ReportData Handle(StatisticsData message)
        {
            return this.reports.GetReportData(new ChartDataFilter
                {
                    StartDate = message.Start,
                    EndDate = message.End,
                    Organisation = this.cache.Find<Guid>("Taxon.Default.Cache"),
                    ScheduleSetting = message.ScheduleSetting,
                    Account = message.Account,
                    Patient = message.Patient
                });
        }

        #endregion
    }
}