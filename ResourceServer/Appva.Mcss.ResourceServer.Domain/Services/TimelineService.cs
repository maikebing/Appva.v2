// <copyright file="TimelineService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
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
    public interface ITimelineService : IService
    {
        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="patientId">TODO: patientId</param>
        /// <param name="fromDate">TODO: fromDate</param>
        /// <param name="toDate">TODO: toDate</param>
        /// <param name="typeIds">TODO: typeIds</param>
        /// <returns>TODO: returns</returns>
        IList<Task> FindByPatient(Guid patientId, DateTime fromDate, DateTime toDate, IList<string> typeIds);

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="taxonId">TODO: taxonId</param>
        /// <param name="fromDate">TODO: fromDate</param>
        /// <param name="toDate">TODO: toDate</param>
        /// <param name="typeIds">TODO: typeIds</param>
        /// <returns>TODO: returns</returns>
        IList<Task> FindByTaxon(Guid taxonId, DateTime fromDate, DateTime toDate, IList<string> typeIds);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class TimelineService : AbstractTaskService, ITimelineService
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineService"/> class.
        /// </summary>
        /// <param name="taskRepository">The <see cref="ITaskRepository"/>.</param>
        /// <param name="sequenceRepository">The <see cref="ISequenceRepository"/>.</param>
        public TimelineService(ITaskRepository taskRepository, ISequenceRepository sequenceRepository)
            : base(taskRepository, sequenceRepository)
        {
        }

        #endregion

        #region ITimelineService Members.

        /// <inheritdoc />
        public IList<Task> FindByPatient(Guid patientId, DateTime fromDate, DateTime toDate, IList<string> typeIds)
        {
            var sequences = this.SequenceRepository.ListByPatientId(patientId);
            var tasks = this.TaskRepository.ListByPatientId(patientId, fromDate, toDate);
            return this.FindTasks(fromDate, toDate, sequences, tasks, typeIds);
        }

        /// <inheritdoc />
        public IList<Task> FindByTaxon(Guid taxonId, DateTime fromDate, DateTime toDate, IList<string> typeIds)
        {
            var sequences = this.SequenceRepository.ListByTaxonId(taxonId);
            var delayedTasks = this.TaskRepository.ListByTaxonId(taxonId, fromDate, toDate);
            return this.FindTasks(fromDate, toDate, sequences, delayedTasks, typeIds);
        }

        #endregion
    }
}