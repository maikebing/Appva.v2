// <copyright file="MeasurementService.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Domain.VO;

    #endregion

    /// <summary>
    /// Interface IMeasurementService
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IService" />
    public interface IMeasurementService : IService
    {
        /// <summary>
        /// Gets the measurement categories.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns>IList&lt;MeasurementObservation&gt;.</returns>
        IList<MeasurementObservation> ListByPatient(Guid patientId);

        /// <summary>
        /// Creates the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="delegation">The delegation.</param>
        void Create(Patient patient, string name, string description, string scale, Taxon delegation = null);

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
        /// Updates the specified observation.
        /// </summary>
        /// <param name="observation">The observation.</param>
        void Update(MeasurementObservation observation);

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
        /// <param name="observation">The observation.</param>
        /// <param name="account">The account.</param>
        /// <param name="value">The value.</param>
        void CreateValue(MeasurementObservation observation, Account account, string value);
    }

    /// <summary>
    /// Class MeasurementService. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IMeasurementService" />
    public sealed class MeasurementService : IMeasurementService
    {
        #region Variables.

        /// <summary>
        /// The measurement repository
        /// </summary>
        private readonly IMeasurementRepository measurementRepository;

        /// <summary>
        /// The patient repository
        /// </summary>
        private readonly IPatientRepository patientRepository;

        /// <summary>
        /// The observationitem repository
        /// </summary>
        private readonly IObservationItemRepository itemRepository;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementService"/> class.
        /// </summary>
        /// <param name="measurementRepository">The MeasurementRepository<see cref="IMeasurementRepository"/>.</param>
        /// <param name="patientRepository">The PatientRepository<see cref="IPatientRepository"/>.</param>
        /// <param name="itemRepository">The ObservationItemRepository<see cref="IObservationItemRepository"/>.</param>
        public MeasurementService(
            IMeasurementRepository measurementRepository, 
            IPatientRepository patientRepository, 
            IObservationItemRepository itemRepository)
        {
            this.measurementRepository = measurementRepository;
            this.patientRepository     = patientRepository;
            this.itemRepository        = itemRepository;
        }

        #endregion

        #region IMeasurementRepository Members.

        /// <inheritdoc />
        public MeasurementObservation Get(Guid observationId)
        {
            return this.measurementRepository.Get(observationId);
        }

        /// <inheritdoc />
        public IList<MeasurementObservation> ListByPatient(Guid patientId)
        {
            return this.measurementRepository.ListByPatient(patientId);
        }

        /// <inheritdoc />
        public void Update(MeasurementObservation observation)
        {
            //// UNRESOLVED: audit logging
            this.measurementRepository.Update(observation);
        }

        /// <inheritdoc />
        public void Create(Patient patient, string name, string description, string scale, Taxon delegation = null)
        {
            //// UNRESOLVED: audit logging
            this.measurementRepository.Save(new MeasurementObservation(patient, name, description, scale, delegation));
        }

        #endregion

        #region IObservationItemRepository Members.

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
        public void CreateValue(MeasurementObservation observation, Account account, string value)
        {
            var measurement = new Measurement(value);
            var data = new List<SignedData> { SignedData.New(new Domain.VO.Base64Binary(value)) };
            var signature = Signature.New(account, data);

            //// UNRESOLVED: audit logging
            this.itemRepository.Create(ObservationItem.New(observation, measurement, signature: signature));
        }

        #endregion
    }
}
