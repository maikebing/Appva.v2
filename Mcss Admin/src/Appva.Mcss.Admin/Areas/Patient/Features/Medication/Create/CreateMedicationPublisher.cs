// <copyright file="CreateMedicationPublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateMedicationPublisher : AsyncRequestHandler<CreateMedicationModel, DetailsMedicationRequest>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IMedicationService"/>.
        /// </summary>
        private readonly IMedicationService medicationService;

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ISequenceService"/>
        /// </summary>
        private readonly ISequenceService sequenceService;

        #endregion

        #region Constructor.

        public CreateMedicationPublisher(
            IMedicationService medicationService,
            IScheduleService scheduleService,
            ITaxonomyService taxonService,
            IPatientService patientService,
            ISequenceService sequenceService)
        {
            this.medicationService  = medicationService;
            this.scheduleService    = scheduleService;
            this.taxonService       = taxonService;
            this.patientService     = patientService;
            this.sequenceService    = sequenceService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <inheritdoc/>
        public override async Task<DetailsMedicationRequest> Handle(CreateMedicationModel message)
        {
            var patient = this.patientService.Get(message.Id);
            var schedule = this.scheduleService.Find(message.ScheduleId);
            var start = message.OnNeedBasis ? message.OnNeedBasisStartDate.GetValueOrDefault() : message.StartDate.GetValueOrDefault();
            var end = message.OnNeedBasis ? message.OnNeedBasisEndDate : message.EndDate;
            var delegation = this.taxonService.Load(message.Delegation.GetValueOrDefault());

            var medications = new List<Medication>() { };
            
            //// If the created sequence has an ordination-id it is an original packaged drug. 
            //// Else it will be a sequence for dispenzed drugs and therfor we list all dispensed drugs an connect to the sequence. 
            if (message.OrdinationId != Int64.MinValue)
            {
                var medication = await this.medicationService.Find(message.OrdinationId, message.Id);
                this.medicationService.Save(medication);
                medications.Add(medication);
            }
            else
            {
                var list = await this.medicationService.List(message.Id);
                medications = list.Where(x => x.Type == OrdinationType.Dispensed && x.EndsAt.GetValueOrDefault(DateTime.MaxValue) > DateTime.Now).ToList();
                foreach (var m in medications)
                {
                    this.medicationService.Save(m);
                }
            }

            this.sequenceService.Create(
                patient: patient,
                startDate: start,
                endDate: end,
                schedule: schedule,
                description: message.Description,
                canRaiseAlert: null,
                interval: 0,
                name: message.Name,
                rangeInMinutesAfter: message.RangeInMinutesAfter,
                rangeInMinutesBefore: message.RangeInMinutesBefore,
                times: string.Join(",", message.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray()),
                onNeedBasis: message.OnNeedBasis,
                medications: medications);

            return new DetailsMedicationRequest { Id = patient.Id, OrdinationId = medications.FirstOrDefault().OrdinationId };
        }

        #endregion
    }
}