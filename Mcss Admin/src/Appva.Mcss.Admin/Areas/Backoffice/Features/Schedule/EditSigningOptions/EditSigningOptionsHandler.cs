// <copyright file="EditSigningOptionsHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditSigningOptionsHandler : RequestHandler<Identity<EditSigningOptionsModel>, EditSigningOptionsModel>
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
        /// Initializes a new instance of the <see cref="EditSigningOptionsHandler"/> class.
        /// </summary>
        public EditSigningOptionsHandler(IScheduleService scheduleService, ITaxonomyService taxonomyService)
        {
            this.scheduleService = scheduleService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditSigningOptionsModel Handle(Identity<EditSigningOptionsModel> message)
        {
            var schedule = this.scheduleService.GetScheduleSettings(message.Id);
            var statuses = this.taxonomyService.List(TaxonomicSchema.SignStatus);

            return new EditSigningOptionsModel
            {
                Options = statuses.Select(x => new Tickable 
                        { 
                            Id    = x.Id,
                            Label = x.Name,
                            HelpText = x.Path,
                            IsSelected = schedule.StatusTaxons.Any(s => s.Id == x.Id)

                        }).ToList()
            };
        }

        #endregion
    }
}