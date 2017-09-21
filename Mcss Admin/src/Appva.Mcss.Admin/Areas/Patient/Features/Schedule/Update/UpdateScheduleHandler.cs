// <copyright file="UpdateSequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.se">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateScheduleHandler : RequestHandler<UpdateSchedule, UpdateScheduleForm>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IPatientService patientService;


        #endregion

        #region Constructor

        public UpdateScheduleHandler(IPersistenceContext context, IPatientService patientService)
        {
            this.context = context;
            this.patientService = patientService;
        }

        #endregion

        public override UpdateScheduleForm Handle(UpdateSchedule message)
        {
            //// TODO: Optimize. Query ScheduleSettings instead to save roundtrips to database

            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            var patient = this.patientService.Get(message.Id);
            if (schedule == null || patient == null)
            {
                throw new NotImplementedException("Not Found");
            }
            return new UpdateScheduleForm
            {
                Id = patient.Id,
                ScheduleId = schedule.Id,
                Name = schedule.ScheduleSettings.Name,
                IsCollectingGivenDosage = schedule.ScheduleSettings.IsCollectingGivenDosage
            };
        }
    }
}