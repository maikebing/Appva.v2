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

    #endregion

    /// <summary>
    /// The <see cref="TenaRepository"/> repository.
    /// </summary>
    public interface ITenaObservationPeriodRepository : 
        ISaveRepository<TenaObservationPeriod>,
        IUpdateRepository<TenaObservationPeriod>,
        IRepository<TenaObservationPeriod>
    {
        /// <summary>
        /// Checks if externalId is Unique
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool HasUniqueExternalId(string externalId);

        /// <summary>
        /// If the StartDate is conflicting with previous EndDates
        /// </summary>
        /// <param name="patientId">The external patient id.</param>
        /// <param name="startdate">The selected start date</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool HasConflictingDate(Guid patientId, DateTime startdate);

        /// <summary>
        /// Get a specific Tena Observation Period from DB
        /// </summary>
        /// <param name="periodId">The selected Observation Period.</param>
        /// <returns>Returns a <see cref="TenaObservationPeriod"/>.</returns>
        TenaObservationPeriod GetTenaPeriod(Guid periodId);
        
        /// <summary>
        /// Lists the tena periods.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        IList<TenaObservationPeriod> ListTenaPeriods(Guid patientId);

        /// <summary>
        /// Get the TenaId from DB
        /// </summary>
        /// <param name="patientId">The Patient Id.</param>
        /// <returns>Returns a <see cref="string"/>.</returns>
        string GetTenaId(Guid patientId);


    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenaRepository : ITenaObservationPeriodRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public TenaRepository(IPersistenceContext context)
        {
            this.persistence = context;
        }

        #endregion

        #region IRepository members.

        /// <inheritdoc />
        public TenaObservationPeriod Get(object id)
        {
            return this.persistence.Get<TenaObservationPeriod>(id);
        }

        /// <inheritdoc />
        public TenaObservationPeriod Load(object id)
        {
            return this.persistence.Session.Load<TenaObservationPeriod>(id);
        }

        #endregion

        #region ISaveRepository members.

        /// <inheritdoc />
        public void Save(TenaObservationPeriod entity)
        {
            this.persistence.Save<TenaObservationPeriod>(entity);
        }

        #endregion

        #region IUpdateRepository members.

        /// <inheritdoc />
        public void Update(TenaObservationPeriod entity)
        {
            this.persistence.Update<TenaObservationPeriod>(entity);
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
            if (this.persistence.QueryOver<TenaObservationPeriod>().Where(x => x.Patient.Id == patientId).Where(y => y.EndDate > startdate).RowCount() > 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public TenaObservationPeriod GetTenaPeriod(Guid periodId)
        {
            return this.persistence.QueryOver<TenaObservationPeriod>().Where(x => x.Id == periodId).SingleOrDefault();
        }

        /// <inheritdoc />
        public string GetTenaId(Guid patientId)
        {
            var patient = this.persistence.QueryOver<Patient>().Where(x => x.Id == patientId).SingleOrDefault();
            return patient.TenaId;
        }

        /// <inheritdoc />
        /// 
        public IList<TenaObservationPeriod> ListTenaPeriods(Guid patientId)
        {
            return this.persistence.QueryOver<TenaObservationPeriod>()
                .Where(x => x.IsActive == true)
                .And(x => x.Patient.Id == patientId)
                .List();
        }

        #endregion


       
    }
}
