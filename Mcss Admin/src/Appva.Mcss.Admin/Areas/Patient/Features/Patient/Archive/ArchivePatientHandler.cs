// <copyright file="ArchivePatientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Patient.Archive
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Patient.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ArchivePatientHandler : RequestHandler<ArchivePatient, ListPatient>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchivePatientHandler"/> class.
        /// </summary>
        public ArchivePatientHandler(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListPatient Handle(ArchivePatient message)
        {
            this.patientService.Archive(message.Id);

            return new ListPatient
            {
                IsActive    = message.IsActive,
                IsDeceased  = message.IsDeceased,
                Page        = message.Page,
                SearchQuery = message.SearchQuery
            };
        }

        #endregion
    }
}