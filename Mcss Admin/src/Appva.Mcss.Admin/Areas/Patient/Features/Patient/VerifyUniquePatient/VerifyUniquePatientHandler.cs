// <copyright file="VerifyUniquePatientHandler.cs" company="Appva AB">
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
            try
            {
                var patient = this.patientService.FindByPersonalIdentityNumber(message.PersonalIdentityNumber);
                if (! message.Id.HasValue || patient == null)
                {
                    return patient == null;
                }
                return patient.Id.Equals(message.Id.Value);
            }
            catch (NotUniquePersonalIdentityNumberException)
            {
                return false;
            }
        }

        #endregion
    }
}