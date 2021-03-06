﻿// <copyright file="GenerateExcelHandler.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Transform;
    using Appva.Core.Extensions;
    using Appva.Office;
    using Appva.Mcss.Web.Controllers;
    using Appva.Core.IO;
    using Appva.Tenant.Identity;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class GenerateExcelHandler : RequestHandler<GenerateExcel, FileContentResult>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateExcelHandler"/> class.
        /// </summary>
        public GenerateExcelHandler(
            IAuditService auditing, 
            IAccountService accountService, 
            IIdentityService identityService,
            ITenantService tenantService, 
            ITaskService taskService,
            IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.accountService = accountService;
            this.identityService = identityService;
            this.taskService = taskService;
            this.tenantService = tenantService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override FileContentResult Handle(GenerateExcel message)
        {
            var tasks = this.taskService.List(new ListTaskModel
                {
                    IsActive = true,
                    SkipPaging = true,
                    StartDate = message.StartDate.Date,
                    EndDate = message.EndDate.LastInstantOfDay(),
                    PatientId = message.Id,
                    ScheduleSettingId = message.ScheduleSettingsId
                }, 1, 30);
            /*var query = this.persistence.QueryOver<Task>()
                .Where(x => x.IsActive == true)
                .And(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= message.StartDate.Date)
                .And(x => x.Scheduled <= message.EndDate.LastInstantOfDay())
                .And(x => x.Patient.Id == message.Id)
                .Fetch(x => x.Patient).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .OrderBy(x => x.UpdatedAt).Desc;
            Schedule scheduleAlias = null;
            ScheduleSettings scheduleSettingsAlias = null;
            if (message.ScheduleSettingsId.HasValue)
            {
                query.Inner.JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .Where(() => scheduleAlias.ScheduleSettings.Id == message.ScheduleSettingsId);
            }
            else
            {
                var account = this.accountService.Find(this.identityService.PrincipalId);
                var scheduleSettings = TaskService.GetAllRoleScheduleSettingsList(account);
                query.Inner.JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .JoinAlias(() => scheduleAlias.ScheduleSettings, () => scheduleSettingsAlias)
                    .WhereRestrictionOn(() => scheduleSettingsAlias.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray());
            }*/
            var patient = this.persistence.Get<Patient>(message.Id);
            this.auditing.Read(
                patient,
                "skapade excellista för boende {0} (REF: {1}).", 
                patient.FullName, 
                patient.Id);
            //var tasks = query.List();
            var path = PathResolver.ResolveAppRelativePath("Templates\\Template.xlsx");
            var bytes = ExcelWriter.CreateNew<Task, ExcelTaskModel>(
                path,
                x => new ExcelTaskModel
                {
                    Task = x.Name,
                    TaskCompletedOnDate = x.IsCompleted ? x.CompletedDate.Value.Date : x.UpdatedAt, //// FIXME: Its either completed or null.
                    TaskCompletedOnTime = (x.Delayed && x.CompletedBy.IsNull()) ? "Ej given" : string.Format("{0} {1:HH:mm}", "kl", x.CompletedDate),
                    TaskScheduledOnDate = x.Scheduled.Date,
                    TaskScheduledOnTime = string.Format("{0} {1:HH:mm}", "kl", x.Scheduled),
                    MinutesBefore = x.RangeInMinutesBefore,
                    MinutesAfter = x.RangeInMinutesAfter,
                    PatientFullName = x.Patient.FullName,
                    CompletedBy = x.CompletedBy.IsNotNull() ? x.CompletedBy.FullName : "",
                    TaskCompletionStatus = Status(x)
                },
                tasks.Items.ToList());
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
                if (task.Delayed && task.StatusTaxon.Weight < 2)
                {
                    return string.Format("{0} för sent", task.StatusTaxon.Name);
                }
                else
                {
                    return task.StatusTaxon.Name;
                }
            }
            if (task.Status.Equals(1))
            {
                if (task.Delayed)
                {
                    return "Given för sent";
                }
                return "OK";
            }
            else if (task.Status.Equals(2))
            {
                return "Delvis given";
            }
            else if (task.Status.Equals(3))
            {
                return "Ej given";
            }
            else if (task.Status.Equals(4))
            {
                return "Kan ej ta";
            }
            else if (task.Status.Equals(5))
            {
                return "Medskickad";
            }
            else if (task.Status.Equals(6))
            {
                return "Räknad mängd stämmer ej med saldo";
            }
            if (task.Status.Equals(0) || task.Delayed)
            {
                if (task.DelayHandled)
                {
                    return "Larm åtgärdat";
                }
                return "Ej given";
            }
            return string.Empty;
        }

        #endregion
    }
}