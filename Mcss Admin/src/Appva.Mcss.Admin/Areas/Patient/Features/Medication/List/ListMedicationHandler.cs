// <copyright file="ListMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Medication.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Patient.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListMedicationHandler : AsyncRequestHandler<ListMedicationRequest,ListMedicationModel>
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
        /// Initializes a new instance of the <see cref="ListMedicationHandler"/> class.
        /// </summary>
        public ListMedicationHandler(
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
        public override async Task<ListMedicationModel> Handle(ListMedicationRequest message)
        {
            var patient = this.patientService.Get(message.Id);
            var patientModel = this.patientTransformer.ToPatient(patient);

            var list = await this.medicationService.List(message.Id);
            var sequences = this.medicationService.GetSequenceInformationFor(list);
            return new ListMedicationModel
            {
                Patient = patientModel,
                DispensedMedications = list.Where(x => x.Type == Domain.Entities.OrdinationType.Dispensed).ToList(),
                OriginalPackageMedications = list.Where(x => x.Type != Domain.Entities.OrdinationType.Dispensed).ToList(),
                Sequences = sequences
            };
        }

        #endregion

        
    }
}