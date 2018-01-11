// <copyright file="AdministrationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.se">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// Interface IAdministrationService
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IService" />
    public interface IAdministrationService : IService
    {
        /// <summary>
        /// Saves the specified administration.
        /// </summary>
        /// <param name="administration">The administration.</param>
        void Save(Administration administration);

        /// <summary>
        /// Updates the specified administration.
        /// </summary>
        /// <param name="administration">The administration.</param>
        void Update(Administration administration);
    }

    /// <summary>
    /// Class AdministrationService. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IAdministrationService" />
    public sealed class AdministrationService : IAdministrationService
    {
        #region Variables

        /// <summary>
        /// The <see cref="IAdministrationRepository"/>.
        /// </summary>
        private readonly IAdministrationRepository administrationRepository;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrationService"/> class.
        /// </summary>
        /// <param name="administrationRepository">The <see cref="IAdministrationRepository"/>.</param>
        /// <param name="auditService">The <see cref="IAuditService"/>.</param>
        public AdministrationService(IAdministrationRepository administrationRepository, IAuditService auditService)
        {
            this.administrationRepository = administrationRepository;
            this.auditService = auditService;
        }

        #endregion

        #region IAdministrationRepository Members.

        /// <inheritdoc />
        public void Save(Administration administration)
        {
            this.administrationRepository.Save(administration);
            this.auditService.Create(administration.Patient, "skapade administration (ref, {0})", administration.Id);
        }

        /// <inheritdoc />
        public void Update(Administration administration)
        {
            this.administrationRepository.Update(administration);
            this.auditService.Update(administration.Patient, "ändrade administration (ref, {0})", administration.Id);
        }

        #endregion
    }
}
