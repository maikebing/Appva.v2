// <copyright file="MeasurementRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
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
    public interface IMeasurementRepository : 
        IRepository<MeasurementObservation>, 
        ISaveRepository<MeasurementObservation>, 
        IUpdateRepository<MeasurementObservation>
    {
        /// <summary>
        /// Gets the measurement categories.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns>IList&lt;MeasurementObservation&gt;.</returns>
        IList<MeasurementObservation> ListByPatient(Guid patientId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class MeasurementRepository : Repository<MeasurementObservation>, IMeasurementRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="T:Appva.Persistence.IPersistenceContext" />.</param>
        public MeasurementRepository(IPersistenceContext context) 
            : base(context)
        {
        }

        #endregion

        #region IMeasurementRepository Members.

        /// <inheritdoc />
        public IList<MeasurementObservation> ListByPatient(Guid patientId)
        {
            return this.Context.QueryOver<MeasurementObservation>()
                  .Where(x => x.IsActive)
                    .And(x => x.Patient.Id == patientId)
                .OrderBy(x => x.Name).Asc
                   .List();
        }

        #endregion
    }
}
