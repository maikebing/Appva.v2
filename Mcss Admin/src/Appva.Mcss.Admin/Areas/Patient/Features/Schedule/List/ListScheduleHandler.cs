// <copyright file="ListScheduleHandler.cs" company="Appva AB">
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
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListScheduleHandler : RequestHandler<ListSchedule, ScheduleListViewModel>
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

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListScheduleHandler"/> class.
        /// </summary>
        public ListScheduleHandler(IAuditService auditing, IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence, IIdentityService identities)
        {
            this.auditing = auditing;
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
            this.identities = identities;
        }

        #endregion

        #region RequestHandler Overrides.

        public override ScheduleListViewModel Handle(ListSchedule message)
        {
            var account = this.persistence.Get<Account>(this.identities.PrincipalId);
            var scheduleSettings = TaskService.GetRoleScheduleSettingsList(account);
            var patient = this.persistence.Get<Patient>(message.Id);
            var query = this.persistence.QueryOver<Schedule>()
                .Where(s => s.Patient.Id == patient.Id && s.IsActive == true)
                .JoinQueryOver<ScheduleSettings>(s => s.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray())
                    .And(s => s.ScheduleType == ScheduleType.Action);
            var schedules = query.List();
            this.auditing.Read(
                patient,
                "läste signeringslistor för boende {0} (REF: {1}).",
                patient.FullName, 
                patient.Id);
            return new ScheduleListViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Schedules = schedules
            };
        }

        #endregion
    }
}