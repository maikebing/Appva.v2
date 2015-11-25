// <copyright file="PatientRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
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

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPatientRepository : IProxyRepository<Patient>, IRepository
    { }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientRepository : IPatientRepository
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        public PatientRepository(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region IProxyRepository<Patient> Members.

        /// <inheritdoc />
        public Patient Load(Guid id)
        {
            return this.persistence.Session.Load<Patient>(id);
        }

        #endregion
    }
}