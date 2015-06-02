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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListScheduleHandler"/> class.
        /// </summary>
        public ListScheduleHandler(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence, IIdentityService identities)
        {
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
            var roles = account.Roles;
            var list = new List<ScheduleSettings>();
            foreach (var role in roles)
            {
                var ss = role.ScheduleSettings;
                foreach (var schedule in ss)
                {
                    if (schedule.ScheduleType == ScheduleType.Action)
                    {
                        list.Add(schedule);
                    }
                }
            }
            var patient = this.persistence.Get<Patient>(message.Id);
            var query = this.persistence.QueryOver<Schedule>()
                .Where(s => s.Patient.Id == patient.Id && s.IsActive == true)
                .JoinQueryOver<ScheduleSettings>(s => s.ScheduleSettings)
                    .Where(s => s.ScheduleType == ScheduleType.Action);

            if (list.Count > 0)
            {
                query.WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            var schedules = query.List();

            //this.logService.Info("Användare {0} läste signeringslistor för boende {1} (REF: {2}).".FormatWith(account.UserName, patient.FullName, patient.Id), account, patient, LogType.Read);
            return new ScheduleListViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Schedules = schedules
            };
        }

        #endregion
    }
}