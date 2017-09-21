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
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateScheduleFormHandler : RequestHandler<UpdateScheduleForm, ListSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor

        public UpdateScheduleFormHandler(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext context, IIdentityService identity, IAuditService auditing)
        {
            this.patientService = patientService;
            this.context = context;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler overrides

         public override ListSchedule Handle(UpdateScheduleForm message)
        {
            var patient = patientService.Get(message.Id);

            //// TODO: Optimize. Query ScheduleSettings instead to save roundtrips to database

            var schedule = context.Get<Schedule>(message.ScheduleId);
            schedule.ScheduleSettings.IsCollectingGivenDosage = message.IsCollectingGivenDosage;

            this.context.Update<Schedule>(schedule);

            this.auditing.Create(
                patient,
                "uppdaterade lista {0} (REF: {1}).",
                schedule.ScheduleSettings.Name,
                schedule.Id);
            return new ListSchedule
            {
                Id = patient.Id
            };
        }

        #endregion
    }
}