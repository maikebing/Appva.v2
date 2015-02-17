// <copyright file="SequenceRepository.cs" company="Appva AB">
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
    using Appva.Persistence;
    using Appva.Repository;
    using Mcss.Domain.Entities;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// The sequence repository.
    /// </summary>
    public interface ISequenceRepository : IRepository<Sequence>
    {
        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="patientId">TODO: patientId</param>
        /// <param name="isNeedBased">TODO: isNeedBased</param>
        /// <param name="date">TODO: date</param>
        /// <returns>TODO: returns</returns>
        IList<Sequence> ListByPatientId(Guid patientId, bool isNeedBased = false, DateTime? date = null);

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="taxonId">TODO: taxonId</param>
        /// <param name="isNeedBased">TODO: isNeedBased</param>
        /// <returns>TODO: returns</returns>
        IList<Sequence> ListByTaxonId(Guid taxonId, bool isNeedBased = false);
    }

    /// <summary>
    /// Implementation of <see cref="ISequenceRepository"/>.
    /// </summary>
    public class SequenceRepository : Repository<Sequence>, ISequenceRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public SequenceRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region ISequenceRepository Members.

        /// <inheritdoc />
        public IList<Sequence> ListByPatientId(Guid patientId, bool isNeedBased = false, DateTime? date = null)
        {
            var query = Where(x => x.Patient.Id == patientId)
                .And(x => x.Active == true)
                .And(x => x.OnNeedBasis == isNeedBased);
            if (date != null && date.HasValue)
            {
                query.Where(x => x.StartDate <= date)
                    .And(x => x.EndDate == null || x.EndDate >= date);
            }
            query.JoinQueryOver<Schedule>(x => x.Schedule)
                .Where(x => x.Active == true);
            return query.List();
        }

        /// <inheritdoc />
        public IList<Sequence> ListByTaxonId(Guid taxonId, bool isNeedBased = false)
        {
            Schedule schedule = null;
            return Where(x => x.Active == true)
                .And(x => x.Active == true)
                .And(x => x.OnNeedBasis == isNeedBased)
                .JoinAlias(x => x.Schedule, () => schedule)
                    .Where(() => schedule.Active)
                .JoinQueryOver<Patient>(x => x.Patient)
                    .Where(x => x.Active == true)
                    .And(x => x.Deceased == false)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxonId.ToString(), MatchMode.Anywhere))
                        .List();
        }

        #endregion
    }
}