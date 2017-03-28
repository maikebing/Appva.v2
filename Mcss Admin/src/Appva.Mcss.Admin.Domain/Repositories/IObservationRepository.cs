// <copyright file="IObservationRepository.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IObservationRepository :
        IRepository<Observation>, ISaveRepository<Observation>, IUpdateRepository<Observation>
    {
        /// <summary>
        /// Returns a collection of observations for a patient.
        /// </summary>
        /// <param name="patientId">The patient ID.</param>
        /// <returns>A collection of <see cref="Observation"/>.</returns>
        IList<Observation> ListByPatient(Guid patientId);
    }
}