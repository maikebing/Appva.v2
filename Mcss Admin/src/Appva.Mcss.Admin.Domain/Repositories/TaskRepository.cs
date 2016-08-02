﻿// <copyright file="TaskRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITaskRepository :
        IIdentityRepository<Task>,
        IUpdateRepository<Task>,
        IRepository
    {
        /// <summary>
        /// Returns delayed tasks by patient and if the delay is handled.
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isDelayedHandled"></param>
        IList<Task> FindDelaysByPatient(Patient patient, bool isDelayHandled, IList<ScheduleSettings> list);

        /// <summary>
        /// List Tasks by given criterias
        /// </summary>
        /// <param name="model">The <see cref="SearchAccountModel"/></param>
        /// <param name="page">The current page, must be > 0</param>
        /// <param name="pageSize">The page-size</param>
        /// <returns>A <see cref="PageableSet"/> of <see cref="AccountModel"/></returns>
        PageableSet<Task> List(ListTaskModel model, IList<ScheduleSettings> list, int page = 1, int pageSize = 10);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class TaskRepository : ITaskRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        public TaskRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region ITaskRepository members

        /// <inheritdoc />
        public IList<Task> FindDelaysByPatient(Patient patient, bool isDelayHandled, IList<ScheduleSettings> list)
        {
            return this.persistenceContext.QueryOver<Task>()
                .Where(x => x.IsActive == true)
                  .And(x => x.Delayed == true)
                  .And(x => x.DelayHandled == isDelayHandled)
                  .And(x => x.Patient == patient)
                  .JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray())
                .List();
        }

        /// <inheritdoc />
        public PageableSet<Task> List(ListTaskModel model, IList<ScheduleSettings> list, int page = 1, int pageSize = 10)
        {
            page      = page < 1 ? 1 : page;
            var skip  = (page - 1) * pageSize;
            var query = this.persistenceContext.QueryOver<Task>()
                .Where(x => x.OnNeedBasis == false)
                  .And(x => x.Scheduled >= model.StartDate)
                  .And(x => x.Scheduled <= model.EndDate);
            if (model.IsActive.HasValue)
            {
                query.Where(x => x.IsActive == model.IsActive);
            }
            Account accountAlias = null;
            Patient patientAlias = null;
            Taxon   taxonAlias   = null;
            Sequence sequenceAlias = null;
            Schedule scheduleAlias = null;
            ScheduleSettings scheduleSettingsAlias = null;
            if (model.AccountId.IsNotEmpty())
            {
                query.JoinAlias(x => x.CompletedBy, () => accountAlias)
                    .Where(() => accountAlias.Id == model.AccountId);
            }
            if (model.PatientId.IsNotEmpty())
            {
                query.JoinAlias(x => x.Patient, () => patientAlias)
                    .Where(() => patientAlias.Id == model.PatientId);
            }
            if (model.TaxonId.IsNotEmpty())
            {
                query
                    .JoinAlias(x => x.Patient, () => patientAlias)
                    .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .WhereRestrictionOn(x => taxonAlias.Path)
                        .IsLike(model.TaxonId.ToString(), MatchMode.Anywhere);
            }
            if (model.SequenceId.IsNotEmpty())
            {
                query
                    .JoinAlias(x => x.Sequence, () => sequenceAlias)
                    .Where(() => sequenceAlias.Id == model.SequenceId); ;
            }
            var schedulesetting = model.ScheduleSettingId.IsEmpty() ? null : this.persistenceContext.Get<ScheduleSettings>(model.ScheduleSettingId);
            if (schedulesetting != null)
            {
                query
                    .JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .JoinAlias(() => scheduleAlias.ScheduleSettings, () => scheduleSettingsAlias)
                        .Where(() => scheduleSettingsAlias.Id == schedulesetting.Id);
                //// If selecting a calendar event, should we check if it can raise alert?
                if (schedulesetting.ScheduleType == ScheduleType.Calendar)
                {
                    query.Where(x => x.CanRaiseAlert);
                }
            }
            //// Does this mean all calendar tasks or simply the ones that are CanRaiseAlert?
            else if (model.IncludeCalendarTasks)
            {
                query
                    .JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .JoinAlias(() => scheduleAlias.ScheduleSettings, () => scheduleSettingsAlias)
                        .WhereRestrictionOn(() => scheduleSettingsAlias.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            else
            {
                query
                    .JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .JoinAlias(() => scheduleAlias.ScheduleSettings, () => scheduleSettingsAlias)
                        .WhereRestrictionOn(() => scheduleSettingsAlias.Id).IsIn(list.Select(x => x.Id).ToArray())
                        .And(Restrictions.Or(
                            Restrictions.Where(() => scheduleSettingsAlias.ScheduleType == ScheduleType.Action),
                            Restrictions.Where<Task>(x => x.CanRaiseAlert)
                        ));
            }
            query
                .Fetch  (x => x.Patient).Eager
                .Fetch  (x => x.StatusTaxon).Eager
                .OrderBy(x => x.Scheduled).Desc
                .ThenBy (x => x.CreatedAt).Desc
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (model.SkipPaging == false) 
            {
                query.Skip(skip).Take(pageSize);
            }
            var tasks = query.List<Task>();
            var totalCount = query.RowCount();
            return new PageableSet<Task>
            {
                CurrentPage = page,
                NextPage    = page++,
                PageSize    = pageSize,
                TotalCount  = totalCount,
                Entities    = tasks
            };
        }

        /// <inheritdoc />
        public Task Find(Guid id)
        {
            return this.persistenceContext.Get<Task>(id);
        }

        /// <inheritdoc />
        public void Update(Task entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.persistenceContext.Update<Task>(entity);
        }

        #endregion
    }
}