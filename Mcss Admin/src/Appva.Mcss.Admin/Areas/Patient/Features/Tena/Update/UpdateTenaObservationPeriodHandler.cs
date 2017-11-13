// <copyright file="UpdateTenaObservationPeriodHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Patient.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateTenaObservationPeriodHandler : RequestHandler<UpdateTenaObservationPeriod, UpdateTenaObservationPeriodModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITenaService"/>
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTenaObservationPeriodHandler"/> class.
        /// </summary>
        /// <param name="tenaService">The tena service.</param>
        public UpdateTenaObservationPeriodHandler(ITenaService tenaService)
        {
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateTenaObservationPeriodModel Handle(UpdateTenaObservationPeriod message)
        {
            var period = this.tenaService.GetTenaObservationPeriod(message.PeriodId);

            return new UpdateTenaObservationPeriodModel
            {
                Id          = period.Id,
                Instruction = period.Description,
                StartsAt    = period.StartDate,
                EndsAt      = period.EndDate
            };
        }

        #endregion

        
    }
}