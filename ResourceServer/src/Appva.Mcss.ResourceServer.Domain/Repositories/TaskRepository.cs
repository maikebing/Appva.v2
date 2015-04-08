// <copyright file="TaskRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Appva.Persistence;
    using Appva.Repository;
    using Mcss.Domain.Entities;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// The device repository.
    /// </summary>
    public interface ITaskRepository : IRepository<Task>
    {
        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="sequenceId">TODO: sequenceId</param>
        /// <param name="scheduledDate">TODO: scheduledDate</param>
        /// <returns>TODO: returns</returns>
        Task GetBySequenceAndScheduledDate(Guid sequenceId, DateTime scheduledDate);

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="query">TODO: query</param>
        /// <returns>TODO: returns</returns>
        IList<Task> List(TaskListQuery query);

        /// <summary>
        /// TODO: Remove me! The List is enough.
        /// </summary>
        /// <param name="patientId">TODO: patientId</param>
        /// <param name="fromDate">TODO: fromDate</param>
        /// <param name="endDate">TODO: endDate</param>
        /// <param name="isDelayed">TODO: isDelayed</param>
        /// <param name="isDelayHandled">TODO: isDelayHandled</param>
        /// <param name="isOnNeedsBased">TODO: isOnNeedsBased</param>
        /// <returns>TODO: returns</returns>
        IList<Task> ListByPatientId(Guid patientId, DateTime? fromDate, DateTime? endDate, bool? isDelayed = null, bool? isDelayHandled = null, bool isOnNeedsBased = false);

        /// <summary>
        /// TODO: Remove me! The List is enough.
        /// </summary>
        /// <param name="taxonId">TODO: taxonId</param>
        /// <param name="fromDate">TODO: fromDate</param>
        /// <param name="endDate">TODO: endDate</param>
        /// <param name="isDelayed">TODO: isDelayed</param>
        /// <param name="isDelayHandled">TODO: isDelayHandled</param>
        /// <param name="isOnNeedsBased">TODO: isOnNeedsBased</param>
        /// <returns>TODO: returns</returns>
        IList<Task> ListByTaxonId(Guid taxonId, DateTime? fromDate, DateTime? endDate, bool? isDelayed = null, bool? isDelayHandled = null, bool isOnNeedsBased = false);

        /// <summary>
        /// Returns last completed task in this sequence.
        /// </summary>
        /// <param name="id">TODO: id</param>
        /// <returns>TODO: returns</returns>
        DateTime GetLastTaskDateBySequence(Guid id);
    }

    /// <summary>
    /// Implementation of <see cref="ITaskRepository"/>.
    /// </summary>
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TaskRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region ITaskRepository Members.

        /// <inheritdoc />
        public Task GetBySequenceAndScheduledDate(Guid sequenceId, DateTime scheduledDate)
        {
            return Where(x => x.Active == true)
                   .And(x => x.Sequence.Id == sequenceId)
                   .And(x => x.Scheduled == scheduledDate)
                   .SingleOrDefault<Task>();
        }

        /// <inheritdoc />
        public IList<Task> ListByPatientId(Guid patientId, DateTime? fromDate, DateTime? endDate, bool? isDelayed = null, bool? isDelayHandled = null, bool isOnNeedsBased = false)
        {
            var tasks = Where(x => x.Patient.Id == patientId)
                .And(x => x.Active == true)
                .And(x => x.OnNeedBasis == isOnNeedsBased);
            if (fromDate.HasValue)
            {
                tasks.And(x => x.Scheduled >= fromDate.GetValueOrDefault() || x.EndDate >= fromDate.GetValueOrDefault());
            }
            if (endDate.HasValue)
            {
                tasks.And(x => x.Scheduled <= endDate.GetValueOrDefault() || x.StartDate <= endDate.GetValueOrDefault());
            }
            if (isDelayed.HasValue)
            {
                tasks.And(x => x.Delayed == isDelayed);
            }
            if (isDelayHandled.HasValue)
            {
                tasks.And(x => x.DelayHandled == isDelayHandled);
            }
            tasks.JoinQueryOver<Schedule>(x => x.Schedule)
                    .Where(x => x.Active == true);
            return tasks.List();
        }

        /// <inheritdoc />
        public IList<Task> ListByTaxonId(Guid taxonId, DateTime? fromDate, DateTime? endDate, bool? isDelayed = null, bool? isDelayHandled = null, bool isOnNeedsBased = false)
        {
            Schedule schedule = null;
            var tasks = Where(x => x.Active == true)
                .And(x => x.OnNeedBasis == isOnNeedsBased);
            if (fromDate.HasValue)
            {
                tasks.And(x => x.Scheduled >= fromDate.GetValueOrDefault() || x.EndDate >= fromDate.GetValueOrDefault());
            }
            if (endDate.HasValue)
            {
                tasks.And(x => x.Scheduled <= endDate.GetValueOrDefault() || x.StartDate <= endDate.GetValueOrDefault());
            }
            if (isDelayed.HasValue)
            {
                tasks.And(x => x.Delayed == isDelayed);
            }
            if (isDelayHandled.HasValue)
            {
                tasks.And(x => x.DelayHandled == isDelayHandled);
            }
            tasks.JoinAlias(x => x.Schedule, () => schedule)
                .Where(() => schedule.Active == true);
            tasks.JoinQueryOver<Taxon>(x => x.Taxon)
                .Where(Restrictions.On<Taxon>(x => x.Path)
                    .IsLike(taxonId.ToString(), MatchMode.Anywhere));
            return tasks.List();
        }

        /// <inheritdoc />
        public IList<Task> List(TaskListQuery query)
        {
            Task taskAlias = null;
            Taxon taxonAlias = null;
            Patient patientAlias = null;
            Sequence sequenceAlias = null;
            var tasks = this.PersistenceContext.QueryOver<Task>(() => taskAlias)
                .Where(x => x.Active == true);
            if (query.IsDelayed != null)
            {
                tasks.And(x => x.Delayed == query.IsDelayed);
            }
            if (query.IsDelayHandled != null)
            {
                tasks.And(x => x.DelayHandled == query.IsDelayHandled);
            }
            if (query.IsCompleted != null)
            {
                tasks.And(x => x.IsCompleted == query.IsCompleted);
            }
            if (query.IsNeedBased != null)
            {
                tasks.And(x => x.OnNeedBasis == query.IsNeedBased);
            }
            if (query.TaxonId != null)
            {
                tasks.JoinAlias(() => taskAlias.Taxon, () => taxonAlias)
                    .WhereRestrictionOn(() => taxonAlias.Path)
                    .IsLike(query.TaxonId.ToString(), MatchMode.Anywhere);
            }
            if (query.PatientId != null)
            {
                tasks.JoinAlias(() => taskAlias.Patient, () => patientAlias)
                    .Where(() => patientAlias.Id == query.PatientId);
            }
            if (query.StartDate != null)
            {
                tasks.Where(x => x.CompletedDate > query.StartDate);
            }
            if (query.EndDate != null)
            {
                tasks.Where(x => x.CompletedDate < query.EndDate);
            }
            tasks = tasks.OrderBy(x => x.CompletedDate).Desc.ThenBy(x => x.Scheduled).Desc;
            tasks.JoinAlias(() => taskAlias.Sequence, () => sequenceAlias)
                .Where(() => sequenceAlias.Active == true);
            return tasks.List();
        }

        /// <inheritdoc />
        public DateTime GetLastTaskDateBySequence(Guid id)
        {
            return Where(x => x.Active)
                .And(x => x.Sequence.Id == id)
                .And(x => x.IsCompleted)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Max<Task>(x => x.CompletedDate)))
                .SingleOrDefault<DateTime>();
        }

        #endregion
    }

    /// <summary>
    /// A <see cref="Task"/> query.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class TaskListQuery
    {
        /// <summary>
        /// Optional taxon id.
        /// </summary>
        public Guid? TaxonId { get; set; }

        /// <summary>
        /// Optional patient id.
        /// </summary>
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Optional is delayed.
        /// </summary>
        public bool? IsDelayed { get; set; }

        /// <summary>
        /// Optional is delayed handled.
        /// </summary>
        public bool? IsDelayHandled { get; set; }

        /// <summary>
        /// Optional is completed.
        /// </summary>
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Optional is need based.
        /// </summary>
        public bool? IsNeedBased { get; set; }

        /// <summary>
        /// Optional start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Optional end date.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}