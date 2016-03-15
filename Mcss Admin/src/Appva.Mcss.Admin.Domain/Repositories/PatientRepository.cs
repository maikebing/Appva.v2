// <copyright file="PatientRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Persistence;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using NHibernate.Type;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public interface IPatientRepository : IRepository
    {
        /// <summary>
        /// Gets patients by taxon, filtered by delayed and incomplete tasks
        /// </summary>
        /// <param name="taxon"></param>
        /// <param name="hasDelayedTask"></param>
        /// <param name="hasIncompleteTask"></param>
        /// <returns></returns>
        IList<PatientModel> FindDelayedPatientsBy(Guid taxon, bool hasIncompleteTask = false, IList<Guid> scheduleSettings = null);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientRepository : IPatientRepository
    {
        #region Fields.

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

        #region IPatientRepository members

        /// <inheritdoc />
        public IList<PatientModel> FindDelayedPatientsBy(Guid taxon,  bool hasIncompleteTask = false, IList<Guid> scheduleSettings = null)
        {
            Schedule scheduleAlias = null;
            var tasks = QueryOver.Of<Task>()
                .Where(x => x.IsActive)
                .And(x => x.Delayed)
                .And(x => !x.DelayHandled)
                .JoinAlias(x => x.Schedule, () => scheduleAlias)
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
                .And(x => !x.Deceased)
                .JoinAlias(x => x.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                    .IsLike(taxon.ToString(), MatchMode.Anywhere))
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
                //.OrderBy(() => patientAlias.LastName).Desc
                .TransformUsing(Transformers.AliasToBean<PatientModel>()); 

            return patients.List<PatientModel>();          
        }

        #endregion
    }
}