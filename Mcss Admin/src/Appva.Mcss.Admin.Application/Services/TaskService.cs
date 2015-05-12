﻿// <copyright file="TaskService.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

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
        IList<Task> FindDelaysByPatient(Patient patient, bool isDelayHandled, List<ScheduleSettings> list);

        /// <summary>
        /// Updates the task status.
        /// </summary>
        /// <param name="task"></param>
        void HandleAnyAlert(Account account, Task task, List<ScheduleSettings> list);

        /// <summary>
        /// Updates the status of all tasks
        /// </summary>
        /// <param name="account"></param>
        /// <param name="tasks"></param>
        void HandleAnyAlert(Account account, Patient patient);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TaskService : ITaskService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public TaskService(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region ITaskService Members

        /// <inheritdoc />
        public Task Get(Guid id)
        {
            return this.context.Get<Task>(id);
        }

        /// <inheritdoc />
        public IList<Task> FindDelaysByPatient(Patient patient, bool isDelayHandled, List<ScheduleSettings> list)
        {
            var query = this.context.QueryOver<Task>()
                .Where(x => x.IsActive == true)
                .And(x => x.Delayed == true)
                .And(x => x.DelayHandled == isDelayHandled)
                .And(x => x.Patient == patient);
            if (list != null && list.Count > 0)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            return query.List();
        }

        /// <inheritdoc />
        public void HandleAnyAlert(Account account, Task task, List<ScheduleSettings> list)
        {
            task.DelayHandled = true;
            task.DelayHandledBy = account;
            var tasks = FindDelaysByPatient(task.Patient, false, list);
            if (tasks.Count.Equals(1) && tasks.First().Id.Equals(task.Id))
            {
                var patient = task.Patient;
                patient.HasUnattendedTasks = false;
                this.context.Update(patient);
            }
            this.context.Update(task);
            //var currentUser = Current();
            //LogService.Info(string.Format("Användare {0} kvitterade {1} ({2:yyyy-MM-dd HH:mm} REF: {3}).", currentUser.UserName, task.Name, task.Scheduled, task.Id), currentUser, task.Patient, LogType.Write);
        }

        /// <inheritdoc />
        public void HandleAnyAlert(Account account, Patient patient)
        {
            foreach (var task in FindDelaysByPatient(patient, false, null))
            {
                task.DelayHandled = true;
                task.DelayHandledBy = account;
                this.context.Update(task);
                //LogService.Info(string.Format("Användare {0} kvitterade {1} ({2:yyyy-MM-dd HH:mm} REF: {3}).", account.UserName, task.Name, task.Scheduled, task.Id), account, task.Patient, LogType.Write);
            }
            patient.HasUnattendedTasks = false;
            this.context.Update(patient);
            //LogService.Info(string.Format("Användare {0} kvitterade alla försenade insatser för {1} ({2}).", account.UserName, patient.FullName, patient.Id), account, patient, LogType.Write);
        }

        #endregion
    }
}