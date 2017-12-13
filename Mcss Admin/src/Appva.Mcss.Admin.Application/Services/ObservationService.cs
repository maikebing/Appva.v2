// <copyright file="ObservationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// Interface IObservationService
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IService" />
    public interface IObservationService : IService
    {
        /// <summary>
        /// Creates the specified observation.
        /// </summary>
        /// <param name="observation">The observation.</param>
        void Create(Observation observation);

        /// <summary>
        /// Updates the specified observation.
        /// </summary>
        /// <param name="observation">The observation.</param>
        void Update(Observation observation);

        /// <summary>
        /// Gets the specified observation identifier.
        /// </summary>
        /// <param name="observationId">The observation identifier.</param>
        /// <returns>Observation.</returns>
        Observation Get(Guid observationId);

        /// <summary>
        /// Lists the by patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IList&lt;Observation&gt;.</returns>
        IList<Observation> ListByPatient(Guid id);
    }

    /// <summary>
    /// Class ObservationService. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IObservationService" />
    public sealed class ObservationService : IObservationService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IObservationRepository"/>
        /// </summary>
        private readonly IObservationRepository observationRepository;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationService"/> class.
        /// </summary>
        /// <param name="observationRepository">The <see cref="IObservationRepository"/>.</param>
        /// <param name="auditService">The <see cref="IAuditService"/>.</param>
        public ObservationService(IObservationRepository observationRepository, IAuditService auditService)
        {
            this.observationRepository = observationRepository;
            this.auditService = auditService;
        }

        /// <inheritdoc />
        public void Create(Observation observation)
        {
            this.observationRepository.Save(observation);
            this.auditService.Create(observation.Patient, "skapade mätning (ref, {0})", observation.Id);
        }

        /// <inheritdoc />
        public Observation Get(Guid observationId)
        {
            return this.observationRepository.Get(observationId);
        }

        /// <inheritdoc />
        public IList<Observation> ListByPatient(Guid id)
        {
            return this.observationRepository.ListByPatient(id).OrderBy(x => x.Name).ToList();
        }

        /// <inheritdoc />
        public void Update(Observation observation)
        {
            this.observationRepository.Update(observation);
            this.auditService.Update(observation.Patient, "ändrade mätning (ref, {0})", observation.Id);
        }

        #endregion
    }
}
