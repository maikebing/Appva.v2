// <copyright file="EditSigningOptionsPublisher.cs" company="Appva AB">
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
    internal sealed class EditSigningOptionsPublisher : RequestHandler<EditSigningOptionsModel, Identity<DetailsScheduleModel>>
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
        /// Initializes a new instance of the <see cref="EditSigningOptionsPublisher"/> class.
        /// </summary>
        public EditSigningOptionsPublisher(IScheduleService scheduleService, ITaxonomyService taxonomyService)
        {
            this.scheduleService = scheduleService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override Identity<DetailsScheduleModel> Handle(EditSigningOptionsModel message)
        {
            var schedule = this.scheduleService.GetScheduleSettings(message.Id);
            schedule.StatusTaxons = message.Options.Where(x => x.IsSelected).Select(x => this.taxonomyService.Load(new Guid(x.Id))).ToList();
            this.scheduleService.UpdateScheduleSetting(schedule);

            return new Identity<DetailsScheduleModel> { Id = message.Id };
        }

        #endregion
    }
}