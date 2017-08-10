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
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateTenaIdHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        public ActivateTenaIdHandler(IPersistenceContext persistence, IPatientService patientService)
        {
            this.persistence = persistence;
            this.patientService = patientService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListTena Handle(ActivateTenaId message)
        {
            var patient = this.patientService.Get(message.Id);

            if(patient != null)
            {
                patient.TenaId = message.ExternalId;
                this.persistence.Update(patient);
            }

            return new ListTena
            {
                Id = patient.Id
            };
        }

        #endregion
    }
}