// <copyright file="ReportChart.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Features.Statistics.Chart
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
    public class StatisticsChartData : IRequest<IList<object[]>>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsChartData"/> class.
        /// </summary>
        public StatisticsChartData()
        {
        }

        #endregion

        #region Properties.

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
        /// The startdate
        /// </summary>
        public DateTime Start
        {
            get;
            set;
        }

        /// <summary>
        /// The enddate
        /// </summary>
        public DateTime End
        {
            get;
            set;
        }

        #endregion
    }
}