// <copyright file="UpdateTenaObservationPeriodPublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateTenaObservationPeriodPublisher : RequestHandler<UpdateTenaObservationPeriodModel, ListTena>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITenaService"/>
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTenaObservationPeriodPublisher"/> class.
        /// </summary>
        public UpdateTenaObservationPeriodPublisher(ITenaService tenaService)
        {
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListTena Handle(UpdateTenaObservationPeriodModel message)
        {
            var period = this.tenaService.UpdateTenaPbservationPeriod(
                message.Id, 
                message.StartsAt, 
                message.EndsAt, 
                message.Instruction);

            return new ListTena
            {
                Id       = period.Patient.Id,
                PeriodId = period.Id
            };
        }

        #endregion

        
    }
}