// <copyright file="RenderChartHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using NHibernate.Transform;
    using NHibernate.Criterion;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Cqrs;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.Controllers;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RenderChartHandler : RequestHandler<RenderChart, IList<object[]>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IChartService"/>.
        /// </summary>
        private readonly IChartService chartService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderChartHandler"/> class.
        /// </summary>
        public RenderChartHandler(IChartService chartService)
        {
            this.chartService = chartService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override IList<object[]> Handle(RenderChart message)
        {
            return this.chartService.Create(new ScheduleReportFilter
                {
                    PatientId = message.Id,
                    ScheduleSettingsId = message.ScheduleSettingsId
                },
                message.StartDate,
                message.EndDate);
        }

        #endregion
    }
}