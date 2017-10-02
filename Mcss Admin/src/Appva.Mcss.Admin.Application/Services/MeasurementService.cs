// <copyright file="MeasurementService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMeasurementService : IService
    {
        #region Fields

        /// <summary>
        /// Gets the measurement categories.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns>IList&lt;MeasurementObservation&gt;.</returns>
        IList<MeasurementObservation> GetMeasurementObservationsList(Guid patientId);

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Patient.</returns>
        Patient GetPatient(Guid id);

        /// <summary>
        /// Creates the measurement observation.
        /// </summary>
        /// <param name="observation">The observation.</param>
        void CreateMeasurementObservation(MeasurementObservation observation);

        /// <summary>
        /// Gets the specified observation identifier.
        /// </summary>
        /// <param name="observationId">The observation identifier.</param>
        /// <returns>MeasurementObservation.</returns>
        MeasurementObservation Get(Guid observationId);

        /// <summary>
        /// Gets the taxon.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>Taxon.</returns>
        Taxon GetTaxon(Guid guid);

        /// <summary>
        /// Updates the specified observation.
        /// </summary>
        /// <param name="observation">The observation.</param>
        void Update(MeasurementObservation observation);

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MeasurementService : IMeasurementService
    {
        #region Variables

        /// <summary>
        /// The context
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The measurement repository
        /// </summary>
        private readonly IMeasurementRepository measurementRepository;

        /// <summary>
        /// The patient repository
        /// </summary>
        private readonly IPatientRepository patientRepository;

        /// <summary>
        /// The taxon repository
        /// </summary>
        private readonly ITaxonRepository taxonRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementService"/> class.
        /// </summary>
        /// <param name="context">The PersistenceContext<see cref="IPersistenceContext"/>.</param>
        /// <param name="measurementRepository">The MeasurementRepository<see cref="IMeasurementRepository"/>.</param>
        /// <param name="patientRepository">The PatientRepository<see cref="IPatientRepository"/>.</param>
        /// <param name="taxonRepository">The TaxonRepository<see cref="ITaxonRepository"/>.</param>
        public MeasurementService(
            IPersistenceContext context, 
            IMeasurementRepository measurementRepository, 
            IPatientRepository patientRepository, 
            ITaxonRepository taxonRepository)
        {
            this.context = context;
            this.measurementRepository = measurementRepository;
            this.patientRepository = patientRepository;
            this.taxonRepository = taxonRepository;
        }

        #endregion

        #region IMeasurementRepository members

        /// <inheritdoc />
        public MeasurementObservation Get(Guid observationId)
        {
            return this.measurementRepository.Get(observationId);
        }

        /// <inheritdoc />
        public IList<MeasurementObservation> GetMeasurementObservationsList(Guid patientId)
        {
            return this.measurementRepository.GetMeasurementObservationsList(patientId);
        }

        /// <inheritdoc />
        public void CreateMeasurementObservation(MeasurementObservation observation)
        {
            this.measurementRepository.CreateMeasurementObservation(observation);
        }

        /// <inheritdoc />
        public void Update(MeasurementObservation observation)
        {
            this.measurementRepository.UpdateMeasurementObservation(observation);
        }

        #endregion

        #region IPatientRepository members

        /// <inheritdoc />
        public Patient GetPatient(Guid id)
        {
            return this.patientRepository.Load(id);
        }

        #endregion

        #region ITaxonRepository

        /// <inheritdoc />
        public Taxon GetTaxon(Guid taxonId)
        {
            return this.taxonRepository.Get(taxonId);
        }

        #endregion
    }
}
