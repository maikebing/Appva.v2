// <copyright file="TaskService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Appva.Repository;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITaskService : IService
    {
        /// <summary>
        /// Returns a single task by id.
        /// </summary>
        /// <param name="id"></param>
        Task Get(Guid id);

        /// <summary>
        /// Returns delayed tasks by patient and if the delay is handled.
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isDelayedHandled"></param>
        IList<Task> FindDelaysByPatient(Patient patient, bool isDelayHandled, IList<ScheduleSettings> list);

        /// <summary>
        /// Lists tasks by given criterias
        /// </summary>
        /// <returns>A <see cref="PageableSet"/> of Tasks</returns>
        PageableSet<Task> List(ListTaskModel model, int page = 1, int pageSize = 10);

        /// <summary>
        /// Updates the task status.
        /// </summary>
        /// <param name="taskId">The task ID</param>
        void HandleAlert(Guid taskId);

        /// <summary>
        /// Updates the status of all tasks for a patient.
        /// </summary>
        /// <param name="patient">The patient</param>
        void HandleAlertsForPatient(Patient patient);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TaskService : ITaskService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService;

        /// <summary>
        /// The <see cref="ITaskRepository"/>.
        /// </summary>
        private readonly ITaskRepository taskRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="auditService">The <see cref="IAuditService"/></param>
        /// <param name="taskRepository">The <see cref="ITaskRepository"/></param>
        public TaskService(IIdentityService identityService, IAccountService accountService, IAuditService auditService, ITaskRepository taskRepository)
        {
            this.identityService = identityService;
            this.accountService  = accountService;
            this.auditService    = auditService;
            this.taskRepository  = taskRepository;
        }

        #endregion

        #region ITaskService Members

        /// <inheritdoc />
        public Task Get(Guid id)
        {
            return this.taskRepository.Find(id);
        }

        /// <inheritdoc />
        public IList<Task> FindDelaysByPatient(Patient patient, bool isDelayHandled, IList<ScheduleSettings> list)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = GetAllRoleScheduleSettingsList(account);
            return this.taskRepository.FindDelaysByPatient(patient, isDelayHandled, scheduleSettings);
        }

        /// <inheritdoc />
        public void HandleAlert(Guid taskId)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var task = this.taskRepository.Find(taskId);
            task.DelayHandled = true;
            task.DelayHandledBy = account;
            this.taskRepository.Update(task);
            this.auditService.Update(task.Patient, "kvitterade {0} ({1:yyyy-MM-dd HH:mm} REF: {2}).", task.Name, task.Scheduled, task.Id);
        }

        /// <inheritdoc />
        public void HandleAlertsForPatient(Patient patient)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = GetRoleScheduleSettingsList(account);
            var tasks = this.FindDelaysByPatient(patient, false, scheduleSettings);
            foreach (var task in tasks)
            {
                task.DelayHandled = true;
                task.DelayHandledBy = account;
                this.taskRepository.Update(task);
                this.auditService.Update(task.Patient, "kvitterade {0} ({1:yyyy-MM-dd HH:mm} REF: {2}).", task.Name, task.Scheduled, task.Id);
            }
            this.auditService.Update(patient, "kvitterade alla försenade insatser för {0} ({1}).", patient.FullName, patient.Id);
        }

        /// <inheritdoc />
        public PageableSet<Task> List(ListTaskModel model, int page = 1, int pageSize = 10)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = GetRoleScheduleSettingsList(account);
            return this.taskRepository.List(model, scheduleSettings, page, pageSize);
        }
        
        #endregion

        #region Public Helper Methods.

        /// <summary>
        /// Returns the schedule settings applied to a role for a specific user account.
        /// </summary>
        /// <param name="account">The account</param>
        /// <returns>A list of <see cref="ScheduleSettings"/></returns>
        public static IList<ScheduleSettings> GetRoleScheduleSettingsList(Account account)
        {
            var retval = new List<ScheduleSettings>();
            foreach (var role in account.Roles)
            {
                foreach (var schedule in role.ScheduleSettings)
                {
                    if (schedule.ScheduleType != ScheduleType.Action)
                    {
                        continue;
                    }
                    retval.Add(schedule);
                }
            }
            return retval;
        }

        public static IList<ScheduleSettings> CalendarRoleScheduleSettingsList(Account account)
        {
            var retval = new List<ScheduleSettings>();
            foreach (var role in account.Roles)
            {
                foreach (var schedule in role.ScheduleSettings)
                {
                    if (schedule.ScheduleType != ScheduleType.Calendar)
                    {
                        continue;
                    }
                    retval.Add(schedule);
                }
            }
            return retval;
        }

        /// <summary>
        /// Returns the schedule settings applied to a role for a specific user account.
        /// </summary>
        /// <param name="account">The account</param>
        /// <returns>A list of <see cref="ScheduleSettings"/></returns>
        public static IList<ScheduleSettings> GetAllRoleScheduleSettingsList(Account account)
        {
            var retval = new List<ScheduleSettings>();
            foreach (var role in account.Roles)
            {
                foreach (var schedule in role.ScheduleSettings)
                {
                    retval.Add(schedule);
                }
            }
            return retval;
        }

        #endregion
    }
}