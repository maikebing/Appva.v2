// <copyright file="CreateMedicationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Medication.Handlers
{
    #region Imports.

    using Appva.Core.Resources;
    using Appva.Core.Utilities;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Extensions;
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
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IInvetoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor.

        public CreateMedicationPublisher(
            IMedicationService medicationService,
            IScheduleService scheduleService,
            ITaxonomyService taxonService,
            IPatientService patientService,
            ISequenceService sequenceService,
            IRoleService roleService,
            IInventoryService inventoryService)
        {
            this.medicationService  = medicationService;
            this.scheduleService    = scheduleService;
            this.taxonService       = taxonService;
            this.patientService     = patientService;
            this.sequenceService    = sequenceService;
            this.roleService        = roleService;
            this.inventoryService   = inventoryService;
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
            throw new NotImplementedException();
            /*
            var patient = this.patientService.Get(message.Id);
            var schedule = this.scheduleService.Find(message.ScheduleId);
            var delegation = this.taxonService.Load(message.Delegation.GetValueOrDefault());

            var medications = new List<Medication>() { };
            
            //// If the created sequence has an ordination-id it is an original packaged drug. 
            //// Else it will be a sequence for dispenzed drugs and therfor we list all dispensed drugs an connect to the sequence. 
            if (message.OrdinationId != Int64.MinValue)
            {
                var medication = await this.medicationService.Find(message.OrdinationId, message.Id);
                medication = this.medicationService.SaveOrUpdate(medication);
                medications.Add(medication);
            }
            else
            {
                var list = await this.medicationService.List(message.Id);
                medications = list.Where(x => x.Type == OrdinationType.Dispensed && x.EndsAt.GetValueOrDefault(DateTime.MaxValue) > DateTime.Now).ToList();
                IList<Medication> persistedMedications = new List<Medication>();
                foreach (var m in medications)
                {
                    persistedMedications.Add(this.medicationService.SaveOrUpdate(m));
                }
            }

            //// Sequence creation
            DateTime start = DateTimeUtilities.Now();
            DateTime? end = null;
            if (message.Dates.IsNotEmpty() && message.Interval == 0)
            {
                DateTimeUtils.GetEarliestAndLatestDateFrom(message.Dates.Split(','), out start, out end);
            }
            if (message.Interval > 0)
            {
                message.Dates = null;
            }
            if (message.OnNeedBasis)
            {
                if (message.OnNeedBasisStartDate.HasValue)
                {
                    start = message.OnNeedBasisStartDate.Value;
                }
                if (message.OnNeedBasisEndDate.HasValue)
                {
                    end = message.OnNeedBasisEndDate.Value;
                }
            }
            else
            {
                if (message.StartDate.HasValue)
                {
                    start = message.StartDate.Value;
                }
                if (message.EndDate.HasValue)
                {
                    end = message.EndDate.Value;
                }
            }

            Inventory inventory = null;
            if (schedule.ScheduleSettings.HasInventory)
            {
                if (message.CreateNewInventory)
                {
                    message.Inventory = this.inventoryService.Create(message.Name, null, null, schedule.Patient);
                }
                inventory = this.inventoryService.Find(message.Inventory.GetValueOrDefault());
            }

            this.sequenceService.Create(
                patient: patient,
                startDate: start,
                endDate: end,
                schedule: schedule,
                description: message.Description,
                canRaiseAlert: null,
                interval: message.Interval.GetValueOrDefault(),
                name: message.Name,
                rangeInMinutesAfter: message.RangeInMinutesAfter,
                rangeInMinutesBefore: message.RangeInMinutesBefore,
                times: string.Join(",", message.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray()),
                onNeedBasis: message.OnNeedBasis,
                medications: medications,
                taxon: message.Delegation.HasValue ? this.taxonService.Load(message.Delegation.GetValueOrDefault()) : null,
                requiredRole: message.Nurse ? this.roleService.Find(RoleTypes.Nurse) : null,
                dates: message.Dates,
                inventory: inventory);

            return new DetailsMedicationRequest { Id = patient.Id, OrdinationId = medications.FirstOrDefault().OrdinationId };
            */
        }

        #endregion
    }
}