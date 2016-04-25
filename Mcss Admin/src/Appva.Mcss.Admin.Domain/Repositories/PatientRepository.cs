// <copyright file="PatientRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using NHibernate.Type;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;

    #endregion

    public interface IPatientRepository : IProxyRepository<Patient>, IRepository
    {
        /// <summary>
        /// Gets patients by taxon, filtered by delayed and incomplete tasks
        /// </summary>
        /// <param name="taxon"></param>
        /// <param name="hasDelayedTask"></param>
        /// <param name="hasIncompleteTask"></param>
        /// <returns></returns>
        IList<PatientModel> FindDelayedPatientsBy(string taxon, bool hasIncompleteTask = false, IList<Guid> scheduleSettings = null);

        /// <summary>
        /// Lists patients by search criteria
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageableSet<PatientModel> Search(SearchPatientModel model, IList<Guid> schedulePermissions, int page = 1, int pageSize = 10);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientRepository : IPatientRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        public PatientRepository(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region IPatientRepository Members.

        /// <inheritdoc />
        public IList<PatientModel> FindDelayedPatientsBy(string taxon, bool hasIncompleteTask = false, IList<Guid> scheduleSettings = null)
        {
            Schedule scheduleAlias = null;
            var tasks = QueryOver.Of<Task>()
                .Where(x => x.IsActive)
                  .And(x => x.Delayed)
                  .And(x => !x.DelayHandled)
                .JoinAlias(x => x.Schedule, () => scheduleAlias) //// FIXME: If schedule is active then we wont see tasks created but then deleted.
                    .WhereRestrictionOn(() => scheduleAlias.ScheduleSettings.Id).IsIn(scheduleSettings.ToArray())
                .Select(x => x.Patient.Id);
            if (hasIncompleteTask)
            {
                tasks = tasks.Where(x => !x.IsCompleted);
            }
            PatientModel patientAlias = null;
            Taxon taxonAlias = null;
            var patients = this.context.QueryOver<Patient>()
                .Where(x => x.IsActive)
                  .And(x => x.Deceased == false)
                .Left.JoinAlias(x => x.Taxon, () => taxonAlias, () => taxonAlias.IsActive)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                    .IsLike(taxon, MatchMode.Start))
                .WithSubquery.WhereProperty(x => x.Id).In(tasks)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Patient>(x => x.Id).WithAlias(() => patientAlias.Id))
                    .Add(Projections.Property<Patient>(x => x.FullName).WithAlias(() => patientAlias.FullName))
                    .Add(Projections.Property<Patient>(x => x.PersonalIdentityNumber).WithAlias(() => patientAlias.PersonalIdentityNumber))
                    .Add(Projections.SqlProjection("substring((SELECT '.' + convert(nvarchar(255),TaxonId) FROM SeniorAlerts Where PatientId = {alias}.Id FOR XML PATH('')), 2, 1000) as SeniorAlerts", new[] { "SeniorAlerts" }, new IType[] { NHibernateUtil.String }).WithAlias(() => patientAlias.ProfileAssements))
                    .Add(Projections.Property<Patient>(x => x.Taxon).WithAlias(() => patientAlias.Taxon))
                    .Add(Projections.Property<Patient>(x => x.IsActive).WithAlias(() => patientAlias.IsActive))
                    .Add(Projections.Property<Patient>(x => x.Deceased).WithAlias(() => patientAlias.IsDeceased))
                    .Add(Projections.Property<Patient>(x => x.Identifier).WithAlias(() => patientAlias.Identifier))
                    .Add(Projections.Constant(true).WithAlias(() => patientAlias.HasUnattendedTasks)))
                .OrderByAlias(() => patientAlias.LastName).Desc
                .TransformUsing(Transformers.AliasToBean<PatientModel>());
            return patients.List<PatientModel>();
        }

        /// <inheritdoc />
        public PageableSet<PatientModel> Search(SearchPatientModel model, IList<Guid> schedulePermissions, int page = 1, int pageSize = 10)
        {
            Patient patient = null;
            var query = this.context.QueryOver<Patient>(() => patient)
                .Where(x => x.IsActive == model.IsActive);
            if (model.IsActive)
            {
                query.Where(x => x.Deceased == model.IsDeceased);
            }
            if (model.SearchQuery.IsNotEmpty())
            {
                Expression<Func<Patient, object>> expression = x => x.FullName;
                if (model.SearchQuery.First(2).Is(Char.IsNumber))
                {
                    expression = x => x.PersonalIdentityNumber.Value;
                }
                query.Where(Restrictions.On<Patient>(expression).IsLike(model.SearchQuery, MatchMode.Anywhere)).OrderBy(x => x.LastName);
            }
            if (model.TaxonFilter.IsNotEmpty())
            {
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(model.TaxonFilter, MatchMode.Start));
            }
            var hasUnattendedTasksQuery = QueryOver.Of<Task>()
                .Where(x => x.Patient.Id == patient.Id)
                  .And(x => x.IsActive && x.Delayed && x.DelayHandled == false)
                .JoinQueryOver<Schedule>(x => x.Schedule) //// Join with IsActive to optimize query however check on top.
                .WhereRestrictionOn(x => x.ScheduleSettings.Id).IsIn(schedulePermissions.ToArray())
                  .And(x => x.IsActive)
                .Select(Projections.Property<Task>(x => x.Patient.Id));
            PatientModel dto = null;
            query.Select(
                    Projections.ProjectionList()
                        .Add(Projections.Group<Patient>(x => x.Id).WithAlias(() => dto.Id))
                        .Add(Projections.Group<Patient>(x => x.IsActive).WithAlias(() => dto.IsActive))
                        .Add(Projections.Group<Patient>(x => x.FirstName).WithAlias(() => dto.FirstName))
                        .Add(Projections.Group<Patient>(x => x.LastName).WithAlias(() => dto.LastName))
                        .Add(Projections.Group<Patient>(x => x.FullName).WithAlias(() => dto.FullName))
                        .Add(Projections.Group<Patient>(x => x.PersonalIdentityNumber).WithAlias(() => dto.PersonalIdentityNumber))
                        .Add(Projections.Group<Patient>(x => x.Deceased).WithAlias(() => dto.IsDeceased))
                        .Add(Projections.Group<Patient>(x => x.Identifier).WithAlias(() => dto.Identifier))
                        .Add(Projections.Group<Patient>(x => x.Taxon).WithAlias(() => dto.Taxon))
                        .Add(Projections.SqlProjection("substring((SELECT '.' + convert(nvarchar(255),TaxonId) FROM SeniorAlerts Where PatientId = {alias}.Id FOR XML PATH('')), 2, 1000) as SeniorAlerts", new[] { "SeniorAlerts" }, new IType[] { NHibernateUtil.String }).WithAlias(() => dto.ProfileAssements))
                        .Add(Projections.Conditional(Subqueries.PropertyIn("Id", hasUnattendedTasksQuery.DetachedCriteria), Projections.Constant(true), Projections.Constant(false)).WithAlias(() => dto.HasUnattendedTasks)));
            query.OrderByAlias(() => dto.HasUnattendedTasks).Desc
                .ThenByAlias(() => dto.LastName).Asc
                .TransformUsing(Transformers.AliasToBean<PatientModel>());
            var start = page < 1 ? 1 : page;
            var first = (start - 1) * pageSize;
            var items = query.Skip(first).Take(pageSize).List<PatientModel>();
            return new PageableSet<PatientModel>
            {
                CurrentPage = (long) start,
                NextPage    = (long) start++,
                PageSize    = (long) pageSize,
                TotalCount  = (long) query.RowCount(),
                Entities    = items
            };
        }

        #endregion

        #region IProxyRepository<Patient> Members.

        /// <inheritdoc />
        public Patient Load(Guid id)
        {
            return this.context.Session.Load<Patient>(id);
        }

        #endregion
    }
}