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

    #endregion

    /// <summary>
    /// The <see cref="TenaRepository"/> repository.
    /// </summary>
    public interface ITenaRepository : IRepository<Observation>
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
        /// Creates a new Tena Observation Period
        /// </summary>
        /// <param name="patient">The Patient Entity.</param>
        /// <param name="startdate">Periods starting date.</param>
        /// <param name="enddate">Periods ending date.</param>
        void CreateNewTenaObserverPeriod(Patient patient, DateTime startdate, DateTime enddate);

        /// <summary>
        /// Get a specific Tena Observation Period from DB
        /// </summary>
        /// <param name="periodId">The selected Observation Period.</param>
        /// <returns>Returns a <see cref="TenaObservationPeriod"/>.</returns>
        TenaObservationPeriod GetTenaPeriod(Guid periodId);

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
    public sealed class TenaRepository : ITenaRepository
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
        public void CreateNewTenaObserverPeriod(Patient patient, DateTime startdate, DateTime enddate)
        {
            var period = new TenaObservationPeriod(startdate, enddate, patient, "Mätperiod", "Tena mätperiod");

            this.persistence.Save(period);
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
        public Observation Get(object id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Observation Load(object id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
