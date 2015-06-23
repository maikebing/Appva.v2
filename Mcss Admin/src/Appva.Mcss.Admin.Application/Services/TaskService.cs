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
        /// Lists tasks by given criterias
        /// </summary>
        /// <returns>A <see cref="PageableSet"/> of Tasks</returns>
        PageableSet<Task> List(ListTaskModel model, int page = 1, int pageSize = 10);

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
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ITaskRepository"/>.
        /// </summary>
        private readonly ITaskRepository tasks;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public TaskService(IAuditService auditing, IPersistenceContext context, ITaskRepository tasks)
        {
            this.auditing = auditing;
            this.context = context;
            this.tasks = tasks;
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
            this.tasks.Update(task);
            this.auditing.Update(task.Patient, "kvitterade {0} ({1:yyyy-MM-dd HH:mm} REF: {2}).", task.Name, task.Scheduled, task.Id);
        }

        /// <inheritdoc />
        public void HandleAnyAlert(Account account, Patient patient)
        {
            foreach (var task in FindDelaysByPatient(patient, false, null))
            {
                task.DelayHandled = true;
                task.DelayHandledBy = account;
                this.tasks.Update(task);
                this.auditing.Update(task.Patient, "kvitterade {0} ({1:yyyy-MM-dd HH:mm} REF: {2}).", task.Name, task.Scheduled, task.Id);
            }
            patient.HasUnattendedTasks = false;
            this.context.Update(patient);
            this.auditing.Update(patient, "kvitterade alla försenade insatser för {0} ({1}).", patient.FullName, patient.Id);
        }

        /// <inheritdoc />
        public PageableSet<Task> List(ListTaskModel model, int page = 1, int pageSize = 10)
        {
            return this.tasks.List(model, page, pageSize);
        }

        #endregion
    }
}