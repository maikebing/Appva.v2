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
        /// Gets the value list by date.
        /// </summary>
        /// <param name="observationId">The Measurement Observation Id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>IList&lt;ObservationItem&gt;<see cref="ObservationItem"/>.</returns>
        IList<ObservationItem> GetValueListByDate(Guid observationId, DateTime startDate, DateTime endDate);

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

        /// <summary>
        /// Gets the delegations list.
        /// </summary>
        /// <returns>IList&lt;Models.ITaxon&gt;.</returns>
        IList<Models.ITaxon> GetDelegationsList();

        /// <summary>
        /// Gets the measurement observation.
        /// </summary>
        /// <param name="observationId">The measurement observation identifier.</param>
        /// <returns>MeasurementObservation.</returns>
        MeasurementObservation GetMeasurementObservation(Guid observationId);

        /// <summary>
        /// Deletes the measurement observation.
        /// </summary>
        /// <param name="observation">The observation.</param>
        void DeleteMeasurementObservation(MeasurementObservation observation);

        /// <summary>
        /// Gets the value list.
        /// </summary>
        /// <param name="observationId">The observation identifier.</param>
        /// <returns>IList&lt;ObservationItem&gt;.</returns>
        IList<ObservationItem> GetValueList(Guid observationId);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="valueId">The value identifier.</param>
        /// <returns>ObservationItem.</returns>
        ObservationItem GetValue(Guid valueId);

        /// <summary>
        /// Creates the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void CreateValue(ObservationItem value);

        /// <summary>
        /// Updates the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void UpdateValue(ObservationItem value);

        /// <summary>
        /// Deletes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void DeleteValue(ObservationItem value);

        /// <summary>
        /// Deletes a measurement value list.
        /// </summary>
        /// <param name="items">The items<see cref="ObservationItem"/>.</param>
        void DeleteMeasurementValueList(IList<ObservationItem> items);


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

        /// <summary>
        /// The delegation service
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The observationitem repository
        /// </summary>
        private readonly IObservationItemRepository itemRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementService"/> class.
        /// </summary>
        /// <param name="context">The PersistenceContext<see cref="IPersistenceContext"/>.</param>
        /// <param name="measurementRepository">The MeasurementRepository<see cref="IMeasurementRepository"/>.</param>
        /// <param name="patientRepository">The PatientRepository<see cref="IPatientRepository"/>.</param>
        /// <param name="taxonRepository">The TaxonRepository<see cref="ITaxonRepository"/>.</param>
        /// <param name="delegationService">The DelegationService<see cref="IDelegationService"/>.</param>
        public MeasurementService(
            IPersistenceContext context, 
            IMeasurementRepository measurementRepository, 
            IPatientRepository patientRepository, 
            ITaxonRepository taxonRepository,
            IDelegationService delegationService,
            IObservationItemRepository itemRepository)
        {
            this.context = context;
            this.measurementRepository = measurementRepository;
            this.patientRepository = patientRepository;
            this.taxonRepository = taxonRepository;
            this.delegationService = delegationService;
            this.itemRepository = itemRepository;
        }

        #endregion

        #region IMeasurementRepository members

        /// <inheritdoc />
        public MeasurementObservation Get(Guid observationId)
        {
            return this.measurementRepository.Get(observationId);
        }

        /// <inheritdoc />
        public MeasurementObservation GetMeasurementObservation(Guid observationId)
        {
            return this.measurementRepository.Get(observationId);
        }

        /// <inheritdoc />
        public IList<MeasurementObservation> GetMeasurementObservationsList(Guid patientId)
        {
            return this.measurementRepository.List(patientId);
        }

        /// <inheritdoc />
        public void CreateMeasurementObservation(MeasurementObservation observation)
        {
            this.measurementRepository.Create(observation);
        }

        /// <inheritdoc />
        public void Update(MeasurementObservation observation)
        {
            this.measurementRepository.Update(observation);
        }

        /// <inheritdoc />
        public void DeleteMeasurementObservation(MeasurementObservation observation)
        {
            this.measurementRepository.Delete(observation);
        }
        #endregion

        #region IPatientRepository members

        /// <inheritdoc />
        public Patient GetPatient(Guid id)
        {
            return this.patientRepository.Load(id);
        }

        #endregion

        #region ITaxonRepository members

        /// <inheritdoc />
        public Taxon GetTaxon(Guid taxonId)
        {
            var x = this.delegationService.ListDelegationTaxons();
            return this.taxonRepository.Get(taxonId);
        }

        #endregion

        #region IDelegationService members

        /// <inheritdoc />
        public IList<Models.ITaxon> GetDelegationsList()
        {
            return this.delegationService.ListDelegationTaxons();
        }


        #endregion

        #region IObservationItemRepository Members

        /// <inheritdoc />
        public IList<ObservationItem> GetValueList(Guid observationId)
        {
            return this.itemRepository.List(observationId);
        }

        /// <inheritdoc />
        public IList<ObservationItem> GetValueListByDate(Guid observationId, DateTime startDate, DateTime endDate)
        {
            return this.itemRepository.ListByDate(observationId, startDate, endDate);
        }

        /// <inheritdoc />
        public ObservationItem GetValue(Guid valueId)
        {
            return this.itemRepository.Get(valueId);
        }

        /// <inheritdoc />
        public void CreateValue(ObservationItem value)
        {
            this.itemRepository.Create(value);
        }

        /// <inheritdoc />
        public void UpdateValue(ObservationItem value)
        {
            this.itemRepository.Update(value);
        }

        /// <inheritdoc />
        public void DeleteValue(ObservationItem value)
        {
            this.itemRepository.Delete(value);
        }

        /// <inheritdoc />
        public void DeleteMeasurementValueList(IList<ObservationItem> items)
        {
            this.itemRepository.DeleteAll(items);
        }

        #endregion
    }
}
