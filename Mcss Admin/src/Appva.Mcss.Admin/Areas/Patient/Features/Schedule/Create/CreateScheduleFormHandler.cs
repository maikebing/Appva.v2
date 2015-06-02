// <copyright file="CreateScheduleFormHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateScheduleFormHandler : RequestHandler<CreateScheduleForm, ListSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identities;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateScheduleFormHandler"/> class.
        /// </summary>
        public CreateScheduleFormHandler(IPatientService patientService, IPatientTransformer transformer,
            IPersistenceContext persistence, IIdentityService identities)
        {
            this.identities = identities;
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        public override ListSchedule Handle(CreateScheduleForm message)
        {
            var patient = this.patientService.Get(message.Id);
            var settings = this.persistence.Get<ScheduleSettings>(message.ScheduleSetting);
            var schedule = new Schedule
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true,
                Patient = patient,
                ScheduleSettings = settings
            };
            this.persistence.Save(schedule);
            //var currentUser = Identity();
            //this.logService.Info(string.Format("Användare {0} skapade lista {1} (REF: {2}).", currentUser.UserName, settings.Name, schedule.Id), currentUser, patient, LogType.Write);
            return new ListSchedule
            {
                Id = patient.Id
            };
        }

        #endregion
    }
}