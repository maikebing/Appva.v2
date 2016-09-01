// <copyright file="ListScheduleHandler.cs" company="Appva AB">
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
    internal sealed class ListScheduleHandler : RequestHandler<Parameterless<ListScheduleModel>, ListScheduleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListScheduleHandler"/> class.
        /// </summary>
        public ListScheduleHandler(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListScheduleModel Handle(Parameterless<ListScheduleModel> message)
        {
            return new ListScheduleModel
            {
                Schedules = this.scheduleService.GetSchedules().Where(x => x.ScheduleType == ScheduleType.Action).ToList()
            };
        }

        #endregion
    }
}