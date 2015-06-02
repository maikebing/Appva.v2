// <copyright file="ReportChartHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Statistics.Chart
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Caching.Providers;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class StatisticsChartDataHandler : RequestHandler<StatisticsChartData, IList<object[]>>
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
        /// Initializes a new instance of the <see cref="StatisticsChartData"/> class.
        /// </summary>
        public StatisticsChartDataHandler(IReportService reports, IRuntimeMemoryCache cache)
        {
            this.reports = reports;
            this.cache = cache;
        }

        #endregion

        #region RequestHandler overrides

        public override IList<object[]> Handle(StatisticsChartData message)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1970-01-01").Ticks);
            return this.reports.GetChartData(new ChartDataFilter
            {
                StartDate = message.Start,
                EndDate = message.End,
                Organisation = this.cache.Find<Guid>("Taxon.Default.Cache"),
                ScheduleSetting = message.ScheduleSetting,
                Account = message.Account,
                Patient = message.Patient
            }).Select(x => new object[] { x.Date.Subtract(span).Ticks / 10000, x.OnTimePercentage }).ToList<object[]>();
        }

        #endregion
    }
}