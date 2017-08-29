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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Patient.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Repository;
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

        #region Constants.

        private readonly IList<MedicationModel> MockedMedications = new List<MedicationModel> {
            new MedicationModel {
                Name = "Metoprolol Sandoz",
                Description = "Mot kärlkramp",
                Dosage = "Test",
                Form = "depottablett",
                PackageType = PackageType.Dispenzed,
                ScheduleText = "08:00",
                StrenghtText = "50 mg",
                StartDate = new DateTime(2017,6,29)
            },
            new MedicationModel {
                Name = "Felodipin Teva",
                Description = "Högt blodtryck",
                Dosage = "Test",
                Form = "depottablett",
                PackageType = PackageType.Dispenzed,
                ScheduleText = "08:00",
                StrenghtText = "2,5 mg",
                StartDate = new DateTime(2017,07,13)
            },
            new MedicationModel {
                Name = "Enalapril Sandoz",
                Description = "Högt blodtryck",
                Dosage = "Test",
                Form = "tablett",
                PackageType = PackageType.Dispenzed,
                ScheduleText = "08:00",
                StrenghtText = "5 mg",
                StartDate = new DateTime(2017,07,13)
            },
            new MedicationModel {
                Name = "Prednisolon Pfizer",
                Description = "tablett",
                Dosage = "Test",
                Form = "tablett",
                PackageType = PackageType.Dispenzed,
                ScheduleText = "08:00, 14:00, 20:00",
                StrenghtText = "5 mg",
                StartDate = new DateTime(2017,7,18)
            },
            new MedicationModel {
                Name = "Alvedon",
                Description = "Test",
                Dosage = "Test",
                Form = "filmdragerad tablett",
                PackageType = PackageType.Dispenzed,
                ScheduleText = "08:00, 14:00, 20:00",
                StrenghtText = "500 mg",
                StartDate = new DateTime(2017, 07, 25),
                EndDate = new DateTime(2017, 08, 29)
            },
            new MedicationModel {
                Name = "Licensläkemedel",
                Description = "Mot parkinson",
                Dosage = "Test",
                Form = "injektionsvätska",
                PackageType = PackageType.Scheduled,
                ScheduleText = "10 E till kvällen",
                StrenghtText = "suspension 100 IE/ml (10 milliliter)",
                StartDate = new DateTime(2017, 7, 13)
            },
            new MedicationModel {
                Name = "Humulin NPH",
                Description = ".",
                Dosage = "Test",
                Form = "Tablett",
                PackageType = PackageType.Scheduled,
                ScheduleText = "08:00, 12:00",
                StrenghtText = "50mg",
                StartDate = new DateTime(2017, 8, 25)
            },
            new MedicationModel {
                Name = "Absenor Depot",
                Description = "Mot Extemporetester",
                Dosage = "Test",
                Form = "",
                PackageType = PackageType.Scheduled,
                ScheduleText = "08:00, 20:00",
                StrenghtText = "300 mg",
                StartDate = new DateTime(2017, 7, 13)
            }
        };

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
                Patient = this.patientTransformer.ToPatient(patient),
                ScheduleID = new Guid("369567F9-2DAA-4369-BC4B-A0F90166F130"),
                Medications = new PageableSet<MedicationModel>
                {
                    Entities = this.MockedMedications
                }
            };
        }

        #endregion
    }
}