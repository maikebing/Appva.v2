// <copyright file="DetailsScheduleHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DetailsScheduleHandler : RequestHandler<DetailsSchedule, ScheduleDetailsViewModel>
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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsScheduleHandler"/> class.
        /// </summary>
        public DetailsScheduleHandler(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        public override ScheduleDetailsViewModel Handle(DetailsSchedule message)
        {
            var patient = this.patientService.Get(message.Id);
            var schedule = this.persistence.Get<Schedule>(message.ScheduleId);
            var items = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                .And(x => x.Schedule.Id == message.ScheduleId)
                .Fetch(x => x.Inventory).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .List();
            //var account = Identity();
            //this.logService.Info(string.Format("Användare {0} läste signeringslista {1} (REF: {2}) för boende {3} (REF: {4}).", account.UserName, schedule.ScheduleSettings.Name, schedule.Id, patient.FullName, patient.Id), account, patient, LogType.Read);
            return new ScheduleDetailsViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Schedule = schedule,
                ScheduleItems = items
            };
        }

        #endregion
    }
}