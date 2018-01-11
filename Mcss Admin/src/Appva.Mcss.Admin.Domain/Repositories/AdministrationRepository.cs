// <copyright file="AdministrationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.se">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IAdministrationRepository : IRepository<Administration>, 
        ISaveRepository<Administration>, 
        IUpdateRepository<Administration>
    {
        /// <summary>
        /// Lists the administrations by patient.
        /// </summary>
        /// <param name="patientId">The patient ID.</param>
        /// <returns>A list of administrations IList&lt;Administration&gt;.</returns>
        IList<Administration> ListByPatient(Guid patientId);
    }

    /// <summary>
    /// Implementation of interface IAdministrationRepository
    /// </summary>
    public sealed class AdministrationRepository : Repository<Administration>, IAdministrationRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AdministrationRepository(IPersistenceContext context) 
            : base(context)
        {
        }

        #endregion

        #region IAdministrationRepository Members.

        /// <inheritdoc />
        public IList<Administration> ListByPatient(Guid patientId)
        {
            return this.Context.QueryOver<Administration>()
                .Where(x => x.IsActive)
                  .And(x => x.Patient.Id == patientId)
                 .List();
        }

        #endregion
    }
}
