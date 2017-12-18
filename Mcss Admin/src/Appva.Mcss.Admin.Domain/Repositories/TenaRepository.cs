// <copyright file="TenaRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
using System.Collections.Generic;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// The <see cref="TenaObservationPeriodRepository"/> repository.
    /// </summary>
    public interface ITenaObservationPeriodRepository : 
        ISaveRepository<TenaObservation>,
        IUpdateRepository<TenaObservation>,
        IRepository<TenaObservation>
    {
        /// <summary>
        /// Checks if externalId is Unique
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool HasUniqueExternalId(string externalId);

        /// <summary>
        /// Get a specific Tena Observation Period from DB
        /// </summary>
        /// <param name="periodId">The selected Observation Period.</param>
        /// <returns>Returns a <see cref="TenaObservation"/>.</returns>
        TenaObservation GetTenaPeriod(Guid periodId);
        
        /// <summary>
        /// Lists the tena periods.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        IList<TenaObservation> ListTenaPeriods(Guid patientId);

        /// <summary>
        /// Get the TenaId from DB
        /// </summary>
        /// <param name="patientId">The Patient Id.</param>
        /// <returns>Returns a <see cref="string"/>.</returns>
        string GetTenaId(Guid patientId);

        /// <summary>
        /// Determines whether the specified patient identifier is unique.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="startsAt">The starts at.</param>
        /// <param name="endsAt">The ends at.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified patient identifier is unique; otherwise, <c>false</c>.
        /// </returns>
        bool IsUnique(Guid patientId, DateTime startsAt, DateTime endsAt, Guid? id);


    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenaObservationPeriodRepository : Repository<TenaObservation>, ITenaObservationPeriodRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriodRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public TenaObservationPeriodRepository(IPersistenceContext context) : base(context)
        {
            this.persistence = context;
        }

        #endregion

        #region ITenarepository members.

        /// <inheritdoc />
        public bool HasUniqueExternalId(string externalId)
        {
            return this.persistence.QueryOver<Patient>()
                .Where(x => x.TenaId == externalId)
                    .RowCount() == 0;
        }

        /// <inheritdoc />
        public bool HasConflictingDate(Guid patientId, DateTime startdate)
        {
            if (this.persistence.QueryOver<TenaObservation>().Where(x => x.Patient.Id == patientId).Where(y => y.EndDate > startdate).RowCount() > 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public TenaObservation GetTenaPeriod(Guid periodId)
        {
            return this.persistence.QueryOver<TenaObservation>().Where(x => x.Id == periodId).SingleOrDefault();
        }

        /// <inheritdoc />
        public string GetTenaId(Guid patientId)
        {
            var patient = this.persistence.QueryOver<Patient>().Where(x => x.Id == patientId).SingleOrDefault();
            return patient.TenaId;
        }

        /// <inheritdoc /> 
        public IList<TenaObservation> ListTenaPeriods(Guid patientId)
        {
            return this.persistence.QueryOver<TenaObservation>()
                .Where(x => x.IsActive == true)
                .And(x => x.Patient.Id == patientId)
                .List();
        }

        /// <inheritdoc />
        public bool IsUnique(Guid patientId, DateTime startsAt, DateTime endsAt, Guid? ignorePeriodId)
        {
            var query = this.persistence.QueryOver<TenaObservation>()
                .Where(x => x.IsActive)
                .And(x => x.Patient.Id == patientId)
                .And(x => (startsAt >= x.StartDate && startsAt <= x.EndDate) || (endsAt >= x.StartDate && endsAt <= x.EndDate));

            if (ignorePeriodId.HasValue)
            {
                query.Where(x => x.Id != ignorePeriodId.GetValueOrDefault());
            }
            return query.RowCount() > 0;
        }

        #endregion       
    }
}
