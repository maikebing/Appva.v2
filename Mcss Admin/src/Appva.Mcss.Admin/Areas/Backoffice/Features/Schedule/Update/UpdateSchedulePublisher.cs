// <copyright file="UpdateSchedulePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSchedulePublisher : RequestHandler<UpdateScheduleModel, Identity<DetailsScheduleModel>>
    {
        #region Fields.

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
        /// Initializes a new instance of the <see cref="UpdateSchedulePublisher"/> class.
        /// </summary>
        public UpdateSchedulePublisher(IScheduleService scheduleService, ITaxonomyService taxonomyService)
        {
            this.scheduleService = scheduleService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override Identity<DetailsScheduleModel> Handle(UpdateScheduleModel message)
        {
            var schedule = this.scheduleService.GetScheduleSettings(message.Id);
            schedule.Name = message.Name;
            schedule.AlternativeName = message.AlternativeName;
            schedule.CanRaiseAlerts = message.CanRaiseAlerts;
            schedule.CountInventory = message.CountInventory;
            schedule.HasInventory = message.HasInventory;
            schedule.HasSetupDrugsPanel = message.HasSetupDrugsPanel;
            schedule.IsPausable = message.IsPausable;
            schedule.NurseConfirmDeviation = message.NurseConfirmDeviation;
            schedule.NurseConfirmDeviationMessage = message.NurseConfirmDeviationMessage;
            schedule.OrderRefill = message.OrderRefill;
            schedule.SpecificNurseConfirmDeviation = message.SpecificNurseConfirmDeviation;
            schedule.DelegationTaxon = message.DelegationTaxon.HasValue ? this.taxonomyService.Load(message.DelegationTaxon.Value) : null;

            this.scheduleService.UpdateScheduleSetting(schedule);

            return new Identity<DetailsScheduleModel> { Id = message.Id };
        }

        #endregion
    }
}