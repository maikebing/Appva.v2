// <copyright file="InactivatePatientHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InactivatePatientHandler : RequestHandler<InactivatePatient, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivatePatientHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/> implementation</param>
        public InactivatePatientHandler(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override bool Handle(InactivatePatient message)
        {
            var patient = this.patientService.Get(message.Id);
            this.patientService.Inactivate(patient);
            return true;
        }

        #endregion
    }
}