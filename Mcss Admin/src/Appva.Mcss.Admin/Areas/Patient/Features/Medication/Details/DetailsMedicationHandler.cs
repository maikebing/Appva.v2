// <copyright file="DetailsMedicationHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DetailsMedicationHandler : AsyncRequestHandler<DetailsMedicationRequest, DetailsMedicationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IMedicationService"/>.
        /// </summary>
        private readonly IMedicationService medicationService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsMedicationHandler"/> class.
        /// </summary>
        /// <param name="medicationService">The medication service.</param>
        /// <param name="patientService">The patient service.</param>
        /// <param name="patientTransformer">The patient transformer.</param>
        public DetailsMedicationHandler(
            IMedicationService medicationService,
            IPatientService patientService,
            IPatientTransformer patientTransformer)
        {
            this.medicationService  = medicationService;
            this.patientService     = patientService;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override async Task<DetailsMedicationModel> Handle(DetailsMedicationRequest message)
        {
            var patient= this.patientService.Get(message.Id);
            var patientModel = this.patientTransformer.ToPatient(patient);
            var medication   = await this.medicationService.Find(message.OrdinationId, message.Id);
            var sequences    = this.medicationService.GetSequenceInformationFor(new List<Medication> { medication });
            return new DetailsMedicationModel {
                Medication  = medication,
                Patient     = patientModel,
                Sequences   = sequences.FirstOrDefault().Value
            };
        }

        #endregion
    }
}