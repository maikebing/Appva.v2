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
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

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
            return this.Context
                .QueryOver<Observation>()
                    .Where(x => x.IsActive)
                      .And(x => x.Patient.Id == patientId)
                .List();
        }

        #endregion
    }
}