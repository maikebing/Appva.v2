// <copyright file="CreateSequneceMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Hip;
    using Appva.Mcss.Admin.Application.Services;
    using System;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateSequneceMedicationHandler : RequestHandler<CreateSequenceMedication, DetailsMedication>
    {
        #region Fields 

        /// <summary>
        /// The <see cref="ISequenceService"/>
        /// </summary>
        private readonly ISequenceService sequences;

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService schedules;

        /// <summary>
        /// The <see cref="ISequenceService"/>
        /// </summary>
        private readonly IPatientService patients;

        /// <summary>
        /// The <see cref="IHipClient"/>
        /// </summary>
        private readonly IHipClient hipClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequneceMedicationHandler"/> class.
        /// </summary>
        public CreateSequneceMedicationHandler(ISequenceService sequences, IHipClient hipClient, IPatientService patients, IScheduleService schedules)
        {
            this.sequences = sequences;
            this.hipClient = hipClient;
            this.patients = patients;
            this.schedules = schedules;
        }

        #endregion

        public override DetailsMedication Handle(CreateSequenceMedication message)
        {
            var patient = this.patients.Get(message.Id);
            var medication = this.hipClient.Medication.GetAsync("191212121212", message.MedicationId).Result.Content;
            var schedule = this.schedules.Get(message.ScheduleId);

            var name = string.Format("{0} {1} {2}", medication.Prescription.Drug.Name, medication.Prescription.Drug.Drug.Strength, medication.Prescription.Drug.Drug.StrengthUnit);
            var description = string.Format("{0}", medication.Prescription.Drug.Comment);

            DateTime? startDate = null;
            if(medication.Prescription.StartOfTreatment.HasValue)
            {
                startDate = DateTimeExtensions.FromJsonInt(medication.Prescription.StartOfTreatment.GetValueOrDefault());
            }
            else if(medication.Prescription.StartOfFirstTreatment.HasValue)
            {
                startDate = DateTimeExtensions.FromJsonInt(medication.Prescription.StartOfFirstTreatment.GetValueOrDefault());
            }
            DateTime? endDate = null;
            if (medication.Prescription.EndOfTreatment.HasValue)
            {
                endDate = DateTimeExtensions.FromJsonInt(medication.Prescription.EndOfTreatment.GetValueOrDefault());
            }

            this.sequences.Create(
                patient,
                startDate.GetValueOrDefault(DateTime.Now),
                endDate,
                schedule,
                description,
                schedule.ScheduleSettings.CanRaiseAlerts,
                0,
                name: name,
                externalId: medication.Id,
                isActive: false);

            return new DetailsMedication 
            {
                Id = message.Id,
                MedicationId = message.MedicationId
            };
        }
    }
}