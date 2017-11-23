// <copyright file="RegisterTenaPatientIdHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RegisterTenaPatientIdHandler : RequestHandler<Identity<RegisterTenaPatientId>, RegisterTenaPatientId>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterTenaPatientIdHandler"/> class.
        /// </summary>
        public RegisterTenaPatientIdHandler(IPatientTransformer patientTransformer, IPatientService patientService)
        {
            this.patientTransformer = patientTransformer;
            this.patientService     = patientService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override RegisterTenaPatientId Handle(Identity<RegisterTenaPatientId> message)
        {
            return new RegisterTenaPatientId
            {
                PatientViewModel = this.patientTransformer.ToPatient(this.patientService.Get(message.Id))
            };
        }

        #endregion
    }
}