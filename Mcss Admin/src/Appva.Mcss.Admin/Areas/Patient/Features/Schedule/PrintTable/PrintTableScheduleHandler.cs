// <copyright file="PrintSchemaScheduleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights PrintTableScheduleHandler.
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
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintTableScheduleHandler : RequestHandler<PrintTableSchedule, ScheduleTablePrintViewModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IScheduleService"/>.
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintTableScheduleHandler"/> class.
        /// </summary>
        public PrintTableScheduleHandler(IPatientService patientService, IScheduleService scheduleService, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.scheduleService = scheduleService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        public override ScheduleTablePrintViewModel Handle(PrintTableSchedule message)
        {
            var patient = this.patientService.Get(message.Id);
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.Patient.Id == patient.Id)
                .And(x => x.Scheduled >= message.StartDate && x.Scheduled <= message.EndDate.LastInstantOfDay())
                .And(x => x.IsActive)
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (message.ScheduleSettingsId.HasValue)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.Id == message.ScheduleSettingsId);
            }
            if (! message.OnNeedBasis)
            {
                query.AndNot(x => x.OnNeedBasis);
            }
            if (! message.StandardSequences)
            {
                query.And(x => x.OnNeedBasis);
            }
            //var account = Identity();
            //LogService.Info(string.Format("Användare {0} skapade utskrift av signeringslista för boende {1} (REF: {2}).", account.UserName, patient.FullName, patient.Id), account, patient, LogType.Read);
            return new ScheduleTablePrintViewModel
            {
                Patient = patient,
                StartDate = message.StartDate,
                EndDate = message.EndDate.LastInstantOfDay(),
                Tasks = query.List<Task>()
            };
        }

        #endregion
    }
}