// <copyright file="TaskRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate.Transform;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// List Tasks by given criterias
        /// </summary>
        /// <param name="model">The <see cref="SearchAccountModel"/></param>
        /// <param name="page">The current page, must be > 0</param>
        /// <param name="pageSize">The page-size</param>
        /// <returns>A <see cref="PageableSet"/> of <see cref="AccountModel"/></returns>
        PageableSet<Task> List(ListTaskModel model, int page = 1, int pageSize = 10);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageableSet<Task> List(ListTaskModel model, int page = 1, int pageSize = 10)
        {
            var query = this.persistenceContext.QueryOver<Task>()
                .Where(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= model.StartDate)
                .And(x => x.Scheduled <= model.EndDate);

            if (model.Account.HasValue)
            {
                query.JoinQueryOver<Account>(x => x.CompletedBy)
                    .Where(x => x.Id == model.Account.GetValueOrDefault());
            }

            if (model.Patient.HasValue)
            {
                query.JoinQueryOver<Patient>(x => x.Patient)
                    .Where(x => x.Id == model.Patient.GetValueOrDefault());
            }

            if (model.Taxon.GetValueOrDefault().IsEmpty())
            {
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(x => x.Id == model.Taxon.GetValueOrDefault());
            }

            if(model.ScheduleSetting.HasValue)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                        .Where(x => x.Id == model.ScheduleSetting.GetValueOrDefault());
            }
            else if (!model.IncludeCalendarTasks)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                        .Where(x => x.ScheduleType == ScheduleType.Action);
            }

            //// Fetch and transform
            query.Fetch(x => x.Patient).Eager
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());

            //// Checks that page is greater then 0
            if (page < 1)
            {
                page = 1;
            }

            //// Number of rows to skip
            var skip = (page - 1) * pageSize;

            return new PageableSet<Task>()
            {
                CurrentPage = page,
                NextPage = page++,
                PageSize = pageSize,
                TotalCount = query.RowCount(),
                Entities = query.Skip(skip).Take(pageSize).List<Task>()
            };
        }

        /// <summary>
        /// Gets a task whit the given id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>The <see cref="Task"/></returns>
        public Task Find(Guid id)
        {
            return this.persistenceContext.Get<Task>(id);
        }

        /// <summary>
        /// Updates the task
        /// </summary>
        /// <param name="entity">The <see cref="Task"/></param>
        public void Update(Task entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.persistenceContext.Update<Task>(entity);
        }

        #endregion
    }
}