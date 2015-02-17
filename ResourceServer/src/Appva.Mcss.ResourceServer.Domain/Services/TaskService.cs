// <copyright file="TaskService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Domain.Repositories;
    using Mcss.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITaskService : IService
    {
        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="patientId">TODO: patientId</param>
        /// <param name="fromDate">TODO: fromDate</param>
        /// <param name="toDate">TODO: toDate</param>
        /// <returns>TODO: returns</returns>
        IList<Task> ListTasksByPatient(Guid patientId, DateTime fromDate, DateTime toDate);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class TaskService : AbstractTaskService, ITaskService
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="taskRepository">The <see cref="ITaskRepository"/></param>
        /// <param name="sequenceRepository">The <see cref="ISequenceRepository"/></param>
        public TaskService(ITaskRepository taskRepository, ISequenceRepository sequenceRepository)
            : base(taskRepository, sequenceRepository)
        {
        }

        #endregion

        #region ITaskService Members.

        /// <inheritdoc />
        public IList<Task> ListTasksByPatient(Guid patientId, DateTime fromDate, DateTime toDate)
        {
            var sequences = this.SequenceRepository.ListByPatientId(patientId);
            var tasks = this.TaskRepository.ListByPatientId(patientId, fromDate, toDate);
            return this.FindTasks(fromDate, toDate, sequences, tasks, new List<string> { "ordination" });
        }

        #endregion
    }
}