// <copyright file="PrintModelScheduleHandler.cs" company="Appva AB">
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
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintModelScheduleHandler : RequestHandler<PrintModelSchedule, SchedulePrintPopOverViewModel>
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
        /// Initializes a new instance of the <see cref="PrintModelScheduleHandler"/> class.
        /// </summary>
        public PrintModelScheduleHandler(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        public override SchedulePrintPopOverViewModel Handle(PrintModelSchedule message)
        {
            var patient = this.patientService.Get(message.Id);
            var startDate = message.StartDate.HasValue && !message.StartDate.Equals(patient.CreatedAt) ? message.StartDate.Value.Date : DateTime.Now.FirstOfMonth();
            // TODO: Check this -> endDate.HasValue && !startDate.Equals(patient.CreatedAt) - should it be startDate?
            var endDate = message.EndDate.HasValue && !message.EndDate.Equals(patient.CreatedAt) ? message.EndDate.Value.Date : DateTime.Now.LastOfMonth();
            return new SchedulePrintPopOverViewModel
            {
                Id = patient.Id,
                ScheduleSettingsId = message.ScheduleSettingsId,
                PrintStartDate = startDate,
                PrintEndDate = endDate
            };
        }

        #endregion
    }
}