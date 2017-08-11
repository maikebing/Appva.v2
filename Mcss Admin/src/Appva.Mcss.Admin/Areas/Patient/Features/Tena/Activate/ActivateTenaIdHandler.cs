// <copyright file="ActivateTenaIdHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ActivateTenaIdHandler : RequestHandler<ActivateTenaId, ListTena>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateTenaIdHandler"/> class.
        /// </summary>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="auditing">The <see cref="IAuditService"/>.</param>
        public ActivateTenaIdHandler(ITenaService tenaService, IPatientService patientService, IPersistenceContext persistence, IAuditService auditing)
        {
            this.tenaService = tenaService;
            this.patientService = patientService;
            this.persistence = persistence;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListTena Handle(ActivateTenaId message)
        {
            var patient = this.patientService.Get(message.Id);

            if (this.tenaService.GetDataFromTena(message.ExternalId).Key.StatusCode == System.Net.HttpStatusCode.OK
                && this.tenaService.HasUniqueExternalId(message.ExternalId))
            {
                patient.TenaId = message.ExternalId;
                this.persistence.Update(patient);
                this.auditing.Update(patient, "aktiverade TENA Identifi");
            }

            return new ListTena
            {
                Id = patient.Id
            };
        }

        #endregion
    }
}