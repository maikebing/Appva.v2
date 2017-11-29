// <copyright file="DosageObservationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using System.Linq;
    using Appva.Mcss.Admin.Domain;

    #endregion

    /// <summary>
    /// Interface IDosageObservationService
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IService" />
    public interface IDosageObservationService : IService
    {
        /// <summary>
        /// Creates the dosage observation for patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="scaleId">The scale identifier.</param>
        DosageObservation Create(Patient patient, Guid scaleId);

        /// <summary>
        /// Updates the specified dosage observation.
        /// </summary>
        /// <param name="dosageObservation">The dosage observation.</param>
        /// <param name="scaleId">The scale identifier.</param>
        void Update(DosageObservation dosageObservation, Guid scaleId);
    }

    /// <summary>
    /// Class DosageObservationService. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IDosageObservationService" />
    public sealed class DosageObservationService : IDosageObservationService
    {
        #region Variables.

        /// <summary>
        /// The dosage repository
        /// </summary>
        private readonly IDosageObservationRepository dosageRepository;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DosageObservationService"/> class.
        /// </summary>
        /// <param name="dosageRepository">The dosage repository.</param>
        public DosageObservationService(IDosageObservationRepository dosageRepository, ISettingsService settingsService)
        {
            this.dosageRepository = dosageRepository;
            this.settingsService = settingsService;
        }

        #endregion

        #region IDosageObservationRepository Members.

        /// <inheritdoc />
        public DosageObservation Create(Patient patient, Guid scaleId)
        {
            var scale = this.settingsService.Find(ApplicationSettings.InventoryUnitsWithAmounts).SingleOrDefault(x => x.Id == scaleId);
            var observation = new DosageObservation(patient, "Given mängd", "DosageObservation", scale);
            this.dosageRepository.Save(observation);
            //// UNRESOLVED: Do audit logging here
            return observation;
        }

        /// <inheritdoc />
        public void Update(DosageObservation dosageObservation, Guid scaleId)
        {
            var scale = this.settingsService.Find(ApplicationSettings.InventoryUnitsWithAmounts).SingleOrDefault(x => x.Id == scaleId);
            dosageObservation.Update(scale);
            this.dosageRepository.Update(dosageObservation);
        }

        #endregion
    }
}
