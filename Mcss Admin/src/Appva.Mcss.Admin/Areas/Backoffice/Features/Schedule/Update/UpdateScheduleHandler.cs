// <copyright file="UpdateScheduleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateScheduleHandler : RequestHandler<Identity<UpdateScheduleModel>, UpdateScheduleModel>
    {
        #region Field.

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateScheduleHandler"/> class.
        /// </summary>
        public UpdateScheduleHandler(IScheduleService scheduleService, ITaxonomyService taxonomyService)
        {
            this.scheduleService = scheduleService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateScheduleModel Handle(Identity<UpdateScheduleModel> message)
        {
            var schedule = this.scheduleService.GetScheduleSettings(message.Id);

            return new UpdateScheduleModel
            {
                Id                            = schedule.Id,
                AlternativeName               = schedule.AlternativeName,
                CanRaiseAlerts                = schedule.CanRaiseAlerts,
                CountInventory                = schedule.CountInventory,
                DelegationTaxon               = schedule.DelegationTaxon != null ? schedule.DelegationTaxon.Id : (Guid?)null,
                HasInventory                  = schedule.HasInventory,
                HasSetupDrugsPanel            = schedule.HasSetupDrugsPanel,
                IsPausable                    = schedule.IsPausable,
                Name                          = schedule.Name,
                NurseConfirmDeviation         = schedule.NurseConfirmDeviation,
                DeviationMessage              = new ConfirmDeviationMessage(schedule.NurseConfirmDeviationMessage, schedule.SpecificNurseConfirmDeviation),
                OrderRefill                   = schedule.OrderRefill,
                Delegations                   = this.taxonomyService.Roots(TaxonomicSchema.Delegation).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
            };
        }

        #endregion
    }
}