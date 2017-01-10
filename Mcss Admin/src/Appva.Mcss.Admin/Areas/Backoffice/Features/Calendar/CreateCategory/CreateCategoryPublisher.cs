// <copyright file="CreateCategoryPublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateCategoryPublisher : RequestHandler<CreateCategoryModel, bool>
    {
        #region Field.

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryPublisher"/> class.
        /// </summary>
        public CreateCategoryPublisher(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(CreateCategoryModel message)
        {
            var category = new ScheduleSettings
            {
                Absence = message.Absence,
                AlternativeName = message.Name,
                CanRaiseAlerts = true,
                Color = message.Color,
                IsPausable = false,
                MachineName = Guid.NewGuid().ToString(),
                Name = message.Name,
                NurseConfirmDeviation = message.NurseConfirmDeviation,
                NurseConfirmDeviationMessage = message.DeviationMessage.ToHtmlString(),
                OrderRefill = false,
                ScheduleType = ScheduleType.Calendar,
                SpecificNurseConfirmDeviation = message.DeviationMessage.IncludeListOfNurses
            };

            this.scheduleService.SaveScheduleSetting(category);

            return true;
        }

        #endregion
    }
}