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

        #endregion

        #region Constructor.

        public UpdateMedicationModelHandler(
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
        public override async Task<DetailsMedicationRequest> Handle(UpdateMedicationModel message)
        {
            var medication       = await this.medicationService.Find(message.OrdinationId, message.PatientId);
            var previousSequence = this.sequenceService.Find(message.PreviousSequenceId);

            //// End the previous sequence.
            previousSequence.EndDate = message.StartDate.HasValue ? message.StartDate.GetValueOrDefault().AddDays(-1) : null as DateTime?;

            return new DetailsMedicationRequest
            {
                Id = message.PatientId,
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