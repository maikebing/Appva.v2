// <copyright file="UpdateCategoryPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateCategoryPublisher : RequestHandler<UpdateCategoryModel, Identity<CategoryDetailsModel>>
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
        public UpdateCategoryPublisher(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override Identity<CategoryDetailsModel> Handle(UpdateCategoryModel message)
        {
            if (message.DeviationMessage.IsNull())
            {
                message.DeviationMessage = new ConfirmDeviationMessage();
            }

            var category = this.scheduleService.GetScheduleSettings(message.Id);
            category.Absence = message.Absence;
            category.AlternativeName = message.Name;
            category.Color = message.Color;
            category.Name = message.Name;
            category.NurseConfirmDeviation = message.NurseConfirmDeviation;
            category.NurseConfirmDeviationMessage = message.DeviationMessage.ToHtmlString();
            category.SpecificNurseConfirmDeviation = message.DeviationMessage.IncludeListOfNurses;

            this.scheduleService.UpdateScheduleSetting(category);

            return new Identity<CategoryDetailsModel> { Id = category.Id };
        }

        #endregion
    }
}