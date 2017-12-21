﻿// <copyright file="ObservationService.cs" company="Appva AB">
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
    using Appva.Mcss.Domain.Unit;

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

        /// <summary>
        /// Lists the measurement in a specific observation.
        /// Optional, filter by date
        /// </summary>
        /// <param name="observationId">The observation identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>IList&lt;ObservationItem&gt;.</returns>
        IList<ObservationItem> ListMeasurements(Guid observationId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Creates the item.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <param name="value">The value.</param>
        void NewMeasurement(Observation observation, string value);
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
        /// The <see cref="IObservationItemRepository"/>.
        /// </summary>
        private readonly IObservationItemRepository observationItemRepository;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationService"/> class.
        /// </summary>
        /// <param name="observationRepository">The <see cref="IObservationRepository"/>.</param>
        /// <param name="auditService">The <see cref="IAuditService"/>.</param>
        public ObservationService(IObservationRepository observationRepository, IObservationItemRepository observationItemRepository, IAuditService auditService, IAccountService accountService)
        {
            this.observationRepository = observationRepository;
            this.observationItemRepository = observationItemRepository;
            this.auditService = auditService;
            this.accountService = accountService;
        }

        #endregion

        #region IObservationRepository Members.

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

        /// <inheritdoc />
        public void NewMeasurement(Observation observation, string value)
        {
            observation.TakeMeasurement(this.accountService.CurrentPrincipal(), value);
            this.auditService.Create(observation.Patient, "skapade mätvärde (ref, {0})", observation.Items.Last().Id);
        }

        /// <inheritdoc />
        public IList<ObservationItem> ListMeasurements(Guid observationId, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                return this.observationItemRepository.ListByDate(observationId, startDate.Value, endDate.HasValue ? endDate.Value : DateTime.UtcNow);
            }
            return this.observationItemRepository.List(observationId);
        }

        #endregion
    }
}
