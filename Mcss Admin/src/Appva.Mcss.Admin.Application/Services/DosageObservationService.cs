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
    using System.Linq;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Auditing;

    #endregion

    /// <summary>
    /// Interface IDosageObservationService
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IService" />
    public interface IDosageObservationService : IService
    {
        /// <summary>
        /// Creates the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="scaleId">The scale identifier.</param>
        /// <returns>DosageObservation.</returns>
        DosageObservation Create(Patient patient, Guid scaleId);

        /// <summary>
        /// Updates the specified dosage observation.
        /// </summary>
        /// <param name="dosageObservation">The dosage observation.</param>
        /// <param name="scaleId">The scale identifier.</param>
        void Update(DosageObservation observation, Guid scaleId);
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

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService audit;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DosageObservationService"/> class.
        /// </summary>
        /// <param name="dosageRepository">The <see cref="IDosageObservationRepository"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="audit">The <see cref="IAuditService"/>.</param>
        public DosageObservationService(IDosageObservationRepository dosageRepository, ISettingsService settingsService, IAuditService audit)
        {
            this.dosageRepository = dosageRepository;
            this.settingsService  = settingsService;
            this.audit            = audit;
        }

        #endregion

        #region IDosageObservationRepository Members.

        /// <inheritdoc />
        public DosageObservation Create(Patient patient, Guid scaleId)
        {
            var scale       = this.settingsService.Find(ApplicationSettings.InventoryUnitsWithAmounts).SingleOrDefault(x => x.Id == scaleId);
            var observation = new DosageObservation(patient, "Given mängd", "DosageObservation", scale);
            this.dosageRepository.Save(observation);
            this.audit.Create(patient, "skapade given mängd observation (ref, {0})", observation.Id);
            return observation;
        }

        /// <inheritdoc />
        public void Update(DosageObservation observation, Guid scaleId)
        {
            var scale = this.settingsService.Find(ApplicationSettings.InventoryUnitsWithAmounts).SingleOrDefault(x => x.Id == scaleId);
            observation.Update(scale);
            this.audit.Update(observation.Patient, "ändrade given mängd observation (ref, {0})", observation.Id);
            this.dosageRepository.Update(observation);
        }

        #endregion
    }
}
