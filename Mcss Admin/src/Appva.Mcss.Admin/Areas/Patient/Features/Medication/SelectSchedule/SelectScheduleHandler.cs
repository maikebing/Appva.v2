// <copyright file="SelectScheduleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Medication.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SelectScheduleHandler : RequestHandler<SelectScheduleMedicationRequest, SelectScheduleMedicationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        #endregion

        #region Constructor.

        public SelectScheduleHandler()
        {
            
        }

        #endregion

        #region RequestHandler overrides.

        public override SelectScheduleMedicationModel Handle(SelectScheduleMedicationRequest message)
        {
            var schedules = this.scheduleService.List(message.Id);
            return new SelectScheduleMedicationModel
            {
                Id = message.Id,
                OrdinationId = message.OrdinationId,
                Schedules = schedules.Select(x => new SelectListItem { Text = x.ScheduleSettings.Name, Value = x.Id.ToString() }).ToList(),
            };
        }

        #endregion
    }
}