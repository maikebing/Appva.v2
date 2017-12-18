// <copyright file="ObservationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IObservationRepository :
        IRepository<Observation>, 
        ISaveRepository<Observation>, 
        IUpdateRepository<Observation>
    {
        /// <summary>
        /// Returns a collection of observations for a patient.
        /// </summary>
        /// <param name="patientId">The patient ID.</param>
        /// <returns>A collection of <see cref="Observation"/>.</returns>
        IList<Observation> ListByPatient(Guid patientId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ObservationRepository : Repository<Observation>, IObservationRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public ObservationRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region IObservationRepository Members.

        /// <inheritdoc />
        public IList<Observation> ListByPatient(Guid patientId)
        {
            var m = this.Context.QueryOver<Observation>()
                .Where(x => x.IsActive)
                  .And(x => x.Patient.Id == patientId)
                  .And(Restrictions.Disjunction()
                    .Add(Restrictions.Eq("class", typeof(BristolStoolScaleObservation)))
                    .Add(Restrictions.Eq("class", typeof(FecesObservation)))
                    .Add(Restrictions.Eq("class", typeof(BodyWeightObservation))))
                .List();
            return m;
        }

        #endregion
    }
}