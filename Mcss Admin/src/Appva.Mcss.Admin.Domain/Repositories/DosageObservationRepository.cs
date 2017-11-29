﻿// <copyright file="DosageObservationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// Interface IDosageObservationRepository
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Domain.IRepository{Appva.Mcss.Admin.Domain.Entities.DosageObservation}" />
    public interface IDosageObservationRepository : 
        IRepository<DosageObservation>, 
        ISaveRepository<DosageObservation>, 
        IUpdateRepository<DosageObservation>
    {
        /// <summary>
        /// Saves the specified dosage observation.
        /// </summary>
        /// <param name="dosageObservation">The dosage observation.</param>
        void Save(DosageObservation dosageObservation);

        /// <summary>
        /// Gets the by sequence.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns>DosageObservation.</returns>
        DosageObservation GetBySequence(Sequence sequence);
    }

    /// <summary>
    /// Implementation of IDosageObservationRepository. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Domain.Repository{Appva.Mcss.Admin.Domain.Entities.DosageObservation}" />
    /// <seealso cref="Appva.Mcss.Admin.Domain.Repositories.IDosageObservationRepository" />
    public sealed class DosageObservationRepository : Repository<DosageObservation>, IDosageObservationRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DosageObservationRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="T:Appva.Persistence.IPersistenceContext" />.</param>
        public DosageObservationRepository(IPersistenceContext context) : base(context)
        {
        }

        #endregion

        #region IDosageRepository Members.

        /// <inheritdoc />
        public DosageObservation GetBySequence(Sequence sequence)
        {
            try
            {
                return this.Context.QueryOver<DosageObservation>()
                .Where(x => x.IsActive)
                  .And(x => x.Id == sequence.Observation.Id)
                .SingleOrDefault();
            }
            catch (System.Exception)
            {
                return null;
            }
            
        }

        #endregion
    }
}
