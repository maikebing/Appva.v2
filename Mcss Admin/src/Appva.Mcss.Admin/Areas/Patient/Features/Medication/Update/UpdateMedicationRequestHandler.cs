// <copyright file="UpdateMedicationRequestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMedicationRequestHandler : AsyncRequestHandler<UpdateMedicationRequest,UpdateMedicationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISequenceService"/>
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IMedicationService"/>
        /// </summary>
        private readonly IMedicationService medicationService;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="IInventoryService"/>
        /// </summary>
        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor.

        public UpdateMedicationRequestHandler(
            IMedicationService medicationService,
            ISequenceService sequenceService,
            IDelegationService delegationService,
            IInventoryService inventoryService)
        {
            this.medicationService  = medicationService;
            this.sequenceService    = sequenceService;
            this.delegationService  = delegationService;
            this.inventoryService   = inventoryService;
        }

        #endregion

        #region Const.

        private static IList<int> times = new List<int> {
            6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5
        };

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override async Task<UpdateMedicationModel> Handle(UpdateMedicationRequest message)
        {
            var medication = await this.medicationService.Find(message.OrdinationId, message.Id);
            var sequence   = this.sequenceService.Find(message.SequenceId);

             var delegations = sequence.Schedule.ScheduleSettings.DelegationTaxon != null ?
                this.delegationService.ListDelegationTaxons(byRoot: sequence.Schedule.ScheduleSettings.DelegationTaxon.Id, includeRoots: false)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }) :
                null;

            return new UpdateMedicationModel
            {
                PreviousSequenceId = message.SequenceId,
                OrdinationId = message.OrdinationId,
                CreateNewInventory = false,
                Dates = sequence.Dates,
                Delegation = sequence.Taxon != null ? sequence.Taxon.Id : null as Guid?,
                Delegations = delegations,
                Description = sequence.Description,
                Interval = sequence.Interval,
                Inventories = sequence.Schedule.ScheduleSettings.HasInventory ? this.inventoryService.Search(message.Id, true).Select(x => new SelectListItem() { Text = x.Description, Value = x.Id.ToString() }) : null,
                Inventory = sequence.Inventory != null ? sequence.Inventory.Id : null as Guid?,
                Name = sequence.Name,
                Nurse = sequence.Role != null,
                OnNeedBasis = sequence.OnNeedBasis,
                Patient = sequence.Patient,
                PatientId = sequence.Patient.Id,
                RangeInMinutesAfter = sequence.RangeInMinutesAfter,
                RangeInMinutesBefore = sequence.RangeInMinutesBefore,
                Schedule = sequence.Schedule,
                ScheduleId = sequence.Schedule.Id,
                Times = this.GetTimes(times, sequence.Times.Split(',').Select(x => Convert.ToInt32(x)).ToList()),
                OnNeedBasisStartDate = sequence.OnNeedBasis ? medication.OrdinationStartsAt : (DateTime?)null,
                OnNeedBasisEndDate = sequence.OnNeedBasis ? medication.EndsAt : (DateTime?)null,
                StartDate = !sequence.OnNeedBasis ? medication.OrdinationStartsAt : (DateTime?)null,
                EndDate = !sequence.OnNeedBasis ? medication.EndsAt : (DateTime?)null,
            };
        }
      
        #endregion

        #region Private helpers.

        /// <summary>
        /// Gets the times.
        /// </summary>
        /// <param name="times">The times.</param>
        /// <param name="selected">The selected.</param>
        /// <returns></returns>
        private IList<CheckBoxViewModel> GetTimes(IList<int> times, IList<int> selected)
        {
            return times.Select(x => new CheckBoxViewModel()
            {
                Id = x,
                Checked = selected.Contains(x)
            }).ToList();
        }

        #endregion
    }
}