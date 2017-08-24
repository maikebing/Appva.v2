// <copyright file="ListMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Patient.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListMedicationHandler : RequestHandler<ListMedicationRequest, ListMedicationModel>
    {
        #region Fields.

        /// <summary>
        /// The patient service
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The patient transformer
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMedicationHandler" /> class.
        /// </summary>
        /// <param name="patientService">The patient service.</param>
        public ListMedicationHandler(IPatientService patientService, IPatientTransformer patientTransformer)
        {
            this.patientService     = patientService;
            this.patientTransformer = patientTransformer;
        }

        #endregion


        #region RequestHandler overrides

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ListMedicationModel Handle(ListMedicationRequest message)
        {
            var patient = this.patientService.Get(message.Id);
            return new ListMedicationModel
            {
                Patient = this.patientTransformer.ToPatient(patient)
            };
        }

        #endregion
    }
}