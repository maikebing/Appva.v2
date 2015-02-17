// <copyright file="PatientRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Persistence;
    using Appva.Repository;
    using Mcss.Domain.Entities;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// The patient repository.
    /// </summary>
    public interface IPatientRepository : IRepository<Patient>
    {
        /// <summary>
        /// Returns a collection of <see cref="Patient"/> by search-query.
        /// </summary>
        /// <param name="query">TODO: query</param>
        /// <param name="taxonId">TODO: taxonId</param>
        /// <param name="status">TODO: status</param>
        /// <param name="deceased">TODO: deceased</param>
        /// <param name="count">TODO: count</param>
        /// <param name="page">TODO: page</param>
        /// <returns>TODO: returns</returns>
        IList<Patient> Search(string query, Guid? taxonId, string status, bool deceased = false, int count = 200, int page = 0);

        /// <summary>
        /// Returns a collection of <see cref="Patient"/> <see cref="Guid"/> with alarms.
        /// </summary>
        /// <param name="patients">The list of patients</param>
        /// <returns>TODO: returns</returns>
        IList<Guid> GetPatientsWithAlarm(IList<Patient> patients);

        /// <summary>
        /// Checks if a patient has any ongoning alarms
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>True if the patient has an alarm</returns>
        bool PatientHasAlarm(Guid id);
    }

    /// <summary>
    /// Implementation of <see cref="IPatientRepository"/>.
    /// </summary>
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public PatientRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region IPatientRepository Members

        /// <inheritdoc />
        public IList<Patient> Search(string query, Guid? taxonId, string status, bool deceased, int count = 200, int page = 0)
        {
            var sqlQuery = Where(x => x.Active && x.Deceased == deceased);
            if (query.IsNotEmpty())
            {
                sqlQuery.Where(x => x.FullName.IsLike(query, MatchMode.Anywhere) || x.UniqueIdentifier.IsLike(query, MatchMode.Anywhere));
            }
            if (taxonId.HasValue && taxonId.GetValueOrDefault() != Guid.Empty)
            {
                sqlQuery.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(x => x.Path.IsLike(taxonId.GetValueOrDefault().ToString(), MatchMode.Anywhere));
            }
            return sqlQuery.Skip(page * count).Take(200).List();
        }

        /// <inheritdoc />
        public IList<Guid> GetPatientsWithAlarm(IList<Patient> patients)
        {
            var patientIds = patients.Select(x => x.Id).ToArray();
            var query = Where(x => x.Active)
                .AndRestrictionOn(x => x.Id).IsIn(patientIds)
                .JoinQueryOver<Task>(x => x.Tasks)
                    .Where(x => x.Delayed && !x.DelayHandled && !x.IsCompleted && x.Active)
                .Select(Projections.Distinct(Projections.Property<Patient>(x => x.Id)));
            return query.List<Guid>();
        }

        /// <inheritdoc />
        public bool PatientHasAlarm(Guid guid)
        {
            Task task = null;
            var patient = Where(x => x.Id == guid)
                .JoinAlias(x => x.Tasks, () => task)
                    .Where(() => task.Delayed && !task.DelayHandled && !task.IsCompleted && task.Active)
                .Select(Projections.Alias(Projections.Property<Task>(x => task.Id), "task"))
                .Take(1).SingleOrDefault<Guid>();
            return patient != null;
        }

        #endregion
    }
}