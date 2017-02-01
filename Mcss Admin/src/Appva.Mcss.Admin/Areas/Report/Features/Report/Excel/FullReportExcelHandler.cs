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
    using Appva.Mvc.Localization;

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

        /// <summary>
        /// The <see cref="IHtmlLocalizer"/>.
        /// </summary>
        private readonly IHtmlLocalizer localizer;

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
            IPersistenceContext context,
            IHtmlLocalizer localizer)
        {
            this.auditing        = auditing;
            this.identityService = identityService;
            this.tenantService   = tenantService;
            this.taskService     = taskService;
            this.filter          = filter;
            this.context         = context;
            this.localizer       = localizer;
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
                    TaskCompletedOnDate  = x.IsCompleted ? x.CompletedDate.Value.Date : x.UpdatedAt, //// FIXME: Its either completed or null.
                    TaskCompletedOnTime  = x.Delayed && x.CompletedBy.IsNull() ? this.localizer["Ej_given"].ToString() : this.localizer["kl_{0:HH:mm}", x.CompletedDate].ToString(),
                    TaskScheduledOnDate  = x.Scheduled.Date,
                    TaskScheduledOnTime  = this.localizer["kl_{0:HH:mm}",  x.Scheduled].ToString(),
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
                FileDownloadName = this.localizer["Rapport-{0}-{1}.xlsx", tenant.Name.Replace(" ", "-"), DateTime.Now.ToFileTimeUtc()].ToString()
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
                return task.Delayed && task.StatusTaxon.Weight < 2 ? this.localizer["{0}_för_sent", task.StatusTaxon.Name].ToString() : task.StatusTaxon.Name;
            }
            switch (task.Status)
            {
                case 1:
                    return this.localizer[(task.Delayed ? "Given_för_sent" : "OK")].ToString();
                case 2:
                    return this.localizer["Delvis_given"].ToString();
                case 3:
                    return this.localizer["Ej_given"].ToString();
                case 4:
                    return this.localizer["Kan_ej_ta"].ToString();
                case 5:
                    return this.localizer["Medskickad"].ToString();
                case 6:
                    return this.localizer["Räknad_mängd_stämmer_ej_med_saldo"].ToString();
                default:
                    if (task.Status.Equals(0) || task.Delayed)
                    {
                        return this.localizer[(task.DelayHandled ? "Larm_åtgärdat" : "Ej_given")].ToString();
                    }
                    return string.Empty;
            }
        }

        #endregion
    }
}