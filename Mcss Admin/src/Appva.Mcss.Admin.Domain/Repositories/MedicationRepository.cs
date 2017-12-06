// <copyright file="MedicationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public interface IMedicationRepository :
        ISaveRepository<Medication>,
        IUpdateRepository<Medication>,
        IRepository
    {
        /// <summary>
        /// Finds the specified by ordination identifier.
        /// </summary>
        /// <param name="byOrdinationId">The by ordination identifier.</param>
        /// <returns></returns>
        Medication Find(long byOrdinationId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MedicationRepository : IMedicationRepository
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationRepository"/> class.
        /// </summary>
        public MedicationRepository(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region ISaveRepository members.

        /// <inheritdoc />
        public void Save(Medication entity)
        {
            this.persistence.Save<Medication>(entity);
        }

        /// <inheritdoc />
        public Medication Find(long byOrdinationId)
        {
            return this.persistence.QueryOver<Medication>()
                .Where(x => x.OrdinationId == byOrdinationId)
                .SingleOrDefault();
        }

        #endregion

        #region IUpdateRepository members.

        /// <inheritdoc />
        public void Update(Medication entity)
        {
            this.persistence.Update<Medication>(entity);
        }

        #endregion
    }
}