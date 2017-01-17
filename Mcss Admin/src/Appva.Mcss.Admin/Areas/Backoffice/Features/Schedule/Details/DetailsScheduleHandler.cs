// <copyright file="DetailsScheduleHandler.cs" company="Appva AB">
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
    internal sealed class DetailsScheduleHandler : RequestHandler<Identity<DetailsScheduleModel>, DetailsScheduleModel>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsScheduleHandler"/> class.
        /// </summary>
        public DetailsScheduleHandler(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <intheritdoc />
        public override DetailsScheduleModel Handle(Identity<DetailsScheduleModel> message)
        {
            return new DetailsScheduleModel
            {
                Schedule = this.scheduleService.GetScheduleSettings(message.Id)
            };
        }

        #endregion
    }
}