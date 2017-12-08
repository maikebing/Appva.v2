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
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Application.Extensions;
    using Appva.Core.Resources;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMedicationModelHandler : AsyncRequestHandler<UpdateMedicationModel,DetailsMedicationRequest>
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

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonService;

        /// <summary>
        /// The <see cref="IRoleService"/>
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region Constructor.

        public UpdateMedicationModelHandler(
            IMedicationService medicationService,
            ISequenceService sequenceService,
            IDelegationService delegationService,
            IInventoryService inventoryService,
            ITaxonomyService taxonService,
            IRoleService roleService)
        {
            this.medicationService  = medicationService;
            this.sequenceService    = sequenceService;
            this.delegationService  = delegationService;
            this.inventoryService   = inventoryService;
            this.taxonService       = taxonService;
            this.roleService        = roleService;
        }

        #endregion

        #region Const.

        private static IList<int> times = new List<int> {
            6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5
        };

        #endregion

        #region RequestHandler overrides.

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <inheritdoc/>
        public override async Task<DetailsMedicationRequest> Handle(UpdateMedicationModel message)
        {
            var medication       = await this.medicationService.Find(message.OrdinationId, message.Id);
            var previousSequence = this.sequenceService.Find(message.PreviousSequenceId);

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
            
            //// If startdate is today, time must be included.
            start = start.Date == DateTime.Now.Date ? DateTime.Now : start;

            Inventory inventory = null;
            if (previousSequence.Schedule.ScheduleSettings.HasInventory)
            {
                if (message.CreateNewInventory)
                {
                    message.Inventory = this.inventoryService.Create(message.Name, null, null, previousSequence.Schedule.Patient);
                }
                inventory = this.inventoryService.Find(message.Inventory.GetValueOrDefault());
            }

            //// End the previous sequence.
            //// If change should be done from today it must be valid until the exact time to not miss already created events.
            previousSequence.EndDate = start.Date == DateTime.Now.Date ? start : start.AddDays(-1);

            this.sequenceService.Update(previousSequence);

            this.sequenceService.Create(
                patient: previousSequence.Patient,
                startDate: start,
                endDate: end,
                schedule: previousSequence.Schedule,
                description: message.Description,
                canRaiseAlert: null,
                interval: message.Interval.GetValueOrDefault(),
                name: message.Name,
                rangeInMinutesAfter: message.RangeInMinutesAfter,
                rangeInMinutesBefore: message.RangeInMinutesBefore,
                times: string.Join(",", message.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray()),
                onNeedBasis: message.OnNeedBasis,
                medications: new List<Medication>() { medication },
                taxon: message.Delegation.HasValue ? this.taxonService.Load(message.Delegation.GetValueOrDefault()) : null,
                requiredRole: message.Nurse ? this.roleService.Find(RoleTypes.Nurse) : null,
                dates: message.Dates,
                inventory: inventory);
            


            return new DetailsMedicationRequest
            {
                Id = message.Id,
                OrdinationId = message.OrdinationId
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