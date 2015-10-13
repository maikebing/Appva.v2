// <copyright file="FullReportHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FullReportHandler : RequestHandler<FullReport, FullReportModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IReportService"/>.
        /// </summary>
        private readonly IReportService reports;

        /// <summary>
        /// The <see cref="ITaskService"/>
        /// </summary>
        private readonly ITaskService tasks;

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService schedules;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>
        /// </summary>
        private readonly ITaxonFilterSessionHandler filter;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FullReportHandler"/> class.
        /// </summary>
        public FullReportHandler(
            IAccountService accountService, 
            IIdentityService identityService,
            IReportService reports, 
            ITaskService tasks, 
            IScheduleService schedules, 
            ITaxonFilterSessionHandler filter)
        {
            this.accountService = accountService;
            this.identityService = identityService;
            this.reports = reports;
            this.tasks = tasks;
            this.schedules = schedules;
            this.filter = filter;
        }

        #endregion

        #region RequestHandler overrides.

        public override FullReportModel Handle(FullReport message)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = TaskService.GetAllRoleScheduleSettingsList(account);
            return new FullReportModel
            {
                Start = message.Start.GetValueOrDefault(DateTime.Now.AddMonths(-1)).Date,
                End = message.End.GetValueOrDefault(DateTime.Now).LastInstantOfDay(),
                Schedules = scheduleSettings,
                Tasks = tasks.List(
                    new ListTaskModel 
                    {
                        StartDate = message.Start.GetValueOrDefault(DateTime.Now.AddMonths(-1)).Date,
                        EndDate = message.End.GetValueOrDefault(DateTime.Now).LastInstantOfDay(),
                        ScheduleSettingId = message.ScheduleSetting,
                        TaxonId = this.filter.GetCurrentFilter().Id
                    }, 
                    message.Page,
                    30),
                Report = reports.GetReportData(new ChartDataFilter 
                {
                    StartDate = message.Start.GetValueOrDefault(DateTime.Now.AddMonths(-1)).Date,
                    EndDate = message.End.GetValueOrDefault(DateTime.Now).LastInstantOfDay(),
                    ScheduleSetting = message.ScheduleSetting,
                    Organisation = this.filter.GetCurrentFilter().Id
                }),
                ScheduleSetting = message.ScheduleSetting
            };
        }

        #endregion
    }
}