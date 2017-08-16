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

    using System.Collections.Generic;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;

    #endregion

    public interface ITenaRepository : IRepository
    {
        /// <summary>
        /// If the provided external id is unique.
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
        /// If the StartDate is conflicting with previous EndDates
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        void CreateNewTenaObserverPeriod(Patient patient, DateTime startdate, DateTime enddate);


        TenaObservationPeriod GetTenaPeriod(Guid periodId);
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
            if(this.persistence.QueryOver<TenaObservationPeriod>().Where(x => x.Patient.Id == patientId).Where(y => y.EndDate > startdate).RowCount() > 0)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public void CreateNewTenaObserverPeriod(Patient patient, DateTime startdate, DateTime enddate)
        {
            var period = new TenaObservationPeriod
            {
                Patient = patient,
                StartDate = startdate,
                EndDate = enddate
            };
            this.persistence.Save(period);
        }

        /// <inheritdoc />
        public TenaObservationPeriod GetTenaPeriod(Guid periodId)
        {
            var period = this.persistence.QueryOver<TenaObservationPeriod>().Where(x => x.Id == periodId).SingleOrDefault();

            return period;
        }
        #endregion
    }
}
