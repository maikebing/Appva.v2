﻿// <copyright file="VerifyUniquePatientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class VerifyUniquePatientHandler : RequestHandler<VerifyUniquePatient, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyUniquePatientHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/> implementation</param>
        public VerifyUniquePatientHandler(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override bool Handle(VerifyUniquePatient message)
        {
            if (! message.Id.HasValue)
            {
                return ! this.patientService.PatientWithPersonalIdentityNumberExist(message.UniqueIdentifier);
            }
            var patient = this.patientService.Get(message.Id.Value);
            return patient != null && patient.PersonalIdentityNumber.Equals(message.UniqueIdentifier); //FIXME: Should check both whit and whitout "-"
        }

        #endregion
    }
}