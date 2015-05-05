// <copyright file="SchemaPreparationHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Mcss.Web.Mappers;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.Controllers;
using NHibernate.Criterion;
using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class SchemaPreparationHandler : RequestHandler<SchemaPreparation, PrepareSchemaViewModel>
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
        /// Initializes a new instance of the <see cref="SchemaPreparationHandler"/> class.
        /// </summary>
        public SchemaPreparationHandler(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override PrepareSchemaViewModel Handle(SchemaPreparation message)
        {
            var patient = this.patientService.Get(message.Id);
            if (patient == null)
            {
                throw new NullReferenceException("Patient ID " + message.Id + " is not found");
            }
            var schedule = this.persistence.Get<Schedule>(message.ScheduleId);
            if (schedule.ScheduleSettings.HasSetupDrugsPanel)
            {
                throw new Exception("No schema preparation for this schedule ID " + schedule.ScheduleSettings.Id);
            }
            var startDate = message.StartDate ?? DateTime.Now;
            startDate = startDate.FirstDateOfWeek().Date;
            var sequences = this.persistence.QueryOver<PreparedSequence>()
                .Where(x => x.IsActive)
                .And(x => x.CreatedAt < startDate.AddDays(7))
                .And(x => x.Schedule.Id == schedule.Id)
                .List();
            var tasks = this.persistence.QueryOver<PreparedTask>()
                .Where(x => x.Date >= startDate && x.Date < startDate.AddDays(7))
                .And(x => x.Schedule.Id == schedule.Id)
                .List();
            var taskWithInActiveSequence = tasks.Where(x => x.PreparedSequence.IsActive == false).ToList();
            foreach (var task in taskWithInActiveSequence)
            {
                if (! sequences.Any(x => x.Id == task.PreparedSequence.Id))
                {
                    sequences.Add(task.PreparedSequence);
                }
            }
            return new PrepareSchemaViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Schedule = schedule,
                StartDate = startDate,
                Sequences = sequences,
                Week = startDate.GetWeekNumber(),
                Tasks = tasks,
                ArchivedWeek = startDate.IsLessThan(DateTime.Now.FirstDateOfWeek().Date)
            };
        }

        #endregion
    }
}