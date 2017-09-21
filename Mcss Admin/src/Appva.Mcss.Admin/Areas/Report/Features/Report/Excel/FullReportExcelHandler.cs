// <copyright file="FullReportExcelHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Extensions;
    using Appva.Core.IO;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Web.Controllers;
    using Appva.Office;
    using Appva.Persistence;
    using Appva.Tenant.Identity;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FullReportExcelHandler : RequestHandler<FullReportExcel, FileContentResult>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        /// <summary>
        /// The <see cref="ITaskService"/>
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>
        /// </summary>
        private readonly ITaxonFilterSessionHandler filter;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FullReportExcelHandler"/> class.
        /// </summary>
        public FullReportExcelHandler(
            IAuditService auditing,
            IIdentityService identityService,
            ITenantService tenantService,
            ITaskService taskService,
            ITaxonFilterSessionHandler filter,
            IPersistenceContext context)
        {
            this.auditing        = auditing;
            this.identityService = identityService;
            this.tenantService   = tenantService;
            this.taskService     = taskService;
            this.filter          = filter;
            this.context         = context;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override FileContentResult Handle(FullReportExcel message)
        {
            var account          = this.context.Get <Account>(this.identityService.PrincipalId);
            var scheduleSettings = TaskService.GetAllRoleScheduleSettingsList(account);
            var tasks            = this.taskService.List(new ListTaskModel
            {
                SkipPaging        = true,
                IsActive          = true,
                ScheduleSettingId = message.ScheduleSettingsId,
                StartDate         = message.StartDate.Date,
                EndDate           = message.EndDate.LastInstantOfDay(),
                TaxonId           = this.filter.GetCurrentFilter().Id
            });
           /* Schedule         scheduleAlias         = null;
            ScheduleSettings scheduleSettingsAlias = null;
            var query = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                  .And(x => x.OnNeedBasis == false)
                  .And(x => x.Scheduled   >= message.StartDate.Date)
                  .And(x => x.Scheduled   <= message.EndDate.LastInstantOfDay())
                .Fetch(x => x.Patient).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .OrderBy(x => x.UpdatedAt).Desc;
            var schedulesetting = message.ScheduleSettingsId.IsEmpty() ? null : this.context.Get<ScheduleSettings>(message.ScheduleSettingsId);
            if (schedulesetting != null)
            {
                query.Inner.JoinAlias(x => x.Schedule, () => scheduleAlias, () => scheduleAlias.IsActive)
                    .Where(() => scheduleAlias.ScheduleSettings.Id == schedulesetting.Id);
                if (schedulesetting.ScheduleType == ScheduleType.Calendar)
                {
                    query.Where(x => x.CanRaiseAlert);
                }
            }
            else
            {
                var account = this.context.Get<Account>(this.identityService.PrincipalId);
                var schedulerSettingsIds = TaskService.GetAllRoleScheduleSettingsList(account)
                    .Where(x => x.ScheduleType == ScheduleType.Action || x.CanRaiseAlerts).Select(x => x.Id);
                query.Inner
                    .JoinAlias(x => x.Schedule, () => scheduleAlias, () => scheduleAlias.IsActive)
                    .JoinAlias(() => scheduleAlias.ScheduleSettings, () => scheduleSettingsAlias )
                    .WhereRestrictionOn(() => scheduleSettingsAlias.Id)
                        .IsIn(schedulerSettingsIds.ToArray());
            }
            if (this.filter.GetCurrentFilter() != null && this.filter.GetCurrentFilter().Id.IsNotEmpty())
            {
                query.JoinQueryOver<Patient>(x => x.Patient)
                    .JoinQueryOver<Taxon>(x => x.Taxon)
                        .WhereRestrictionOn(x => x.Path).IsLike(this.filter.GetCurrentFilter().Id.ToString(), MatchMode.Anywhere);
            }
            this.auditing.Read("skapade excellista för perioden {0} t o m {1}.", message.StartDate, message.EndDate);
            var tasks = query.List();*/
            this.auditing.Read("skapade excellista för perioden {0} t o m {1}.", message.StartDate, message.EndDate);
            var path  = PathResolver.ResolveAppRelativePath("Templates\\Template.xlsx");
            var bytes = ExcelWriter.CreateNew<Task, ExcelTaskModel>(
                path,
                x => new ExcelTaskModel
                {
                    Task                 = x.Name,
                    TaskCompletedOnDate  = x.IsCompleted ? x.CompletedDate.Value.Date.ToString("yyyy-MM-dd") : x.UpdatedAt.ToString("yyyy-MM-dd"), //// FIXME: Its either completed or null.
                    TaskCompletedOnTime  = x.Delayed && x.CompletedBy.IsNull() ? "Ej given" : x.CompletedBy.IsNull() ? "-" : string.Format("{0} {1:HH:mm}", "kl", x.CompletedDate),
                    TaskScheduledOnDate  = x.Scheduled.Date.ToString("yyyy-MM-dd"),
                    TaskScheduledOnTime  = string.Format("{0} {1:HH:mm}", "kl", x.Scheduled),
                    MinutesBefore        = x.RangeInMinutesBefore,
                    MinutesAfter         = x.RangeInMinutesAfter,
                    PatientFullName      = x.Patient.FullName,
                    CompletedBy          = x.CompletedBy.IsNotNull() ? x.CompletedBy.FullName : "",
                    TaskCompletionStatus = Status(x)
                },
                tasks.Entities);
            ITenantIdentity tenant;
            this.tenantService.TryIdentifyTenant(out tenant);
            return new FileContentResult(bytes, "application/vnd.ms-excel")
            {
                FileDownloadName = string.Format("Rapport-{0}-{1}.xlsx", tenant.Name.Replace(" ", "-"), DateTime.Now.ToFileTimeUtc())
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Returns the task status as string.
        /// TODO: Refactor!
        /// </summary>
        /// <param name="task">The task</param>
        /// <returns>A string representation of the task status</returns>
        private string Status(Task task)
        {
            if (task.StatusTaxon != null)
            {
                return task.Delayed && task.StatusTaxon.Weight < 2 ? task.StatusTaxon.Name + " för sent" : task.StatusTaxon.Name;
            }
            switch (task.Status)
            {
                case 1:
                    return task.Delayed ? "Given för sent" : "OK";
                case 2:
                    return "Delvis given";
                case 3:
                    return "Ej given";
                case 4:
                    return "Kan ej ta";
                case 5:
                    return "Medskickad";
                case 6:
                    return "Räknad mängd stämmer ej med saldo";
                default:
                    if (task.Status.Equals(0) || task.Delayed)
                    {
                        return task.DelayHandled ? "Larm åtgärdat" : "Ej given";
                    }
                    return string.Empty;
            }
        }

        #endregion
    }
}