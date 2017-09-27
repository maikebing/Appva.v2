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
        IRepository
    {

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

        #endregion
    }
}