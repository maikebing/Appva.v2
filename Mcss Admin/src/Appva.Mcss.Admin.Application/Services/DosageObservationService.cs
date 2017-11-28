// <copyright file="DosageObservationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// Interface IDosageObservationService
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IService" />
    public interface IDosageObservationService : IService
    {
        /// <summary>
        /// Saves the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="scale">The scale.</param>
        void Save(Patient patient, string name, string description, string scale);
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

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DosageObservationService"/> class.
        /// </summary>
        /// <param name="dosageRepository">The dosage repository.</param>
        public DosageObservationService(IDosageObservationRepository dosageRepository)
        {
            this.dosageRepository = dosageRepository;
        }

        #endregion

        #region IDosageObservationRepository Members.

        /// <inheritdoc />
        public void Save(Patient patient, string name, string description, string scale)
        {
            this.dosageRepository.Save(new DosageObservation(patient, name, description, scale));
        }

        #endregion
    }
}
