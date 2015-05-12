// <copyright file="PrintSequenceHandler.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintSequenceHandler : RequestHandler<PrintSequence, PrintViewModel>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ISchedulerService"/>.
        /// </summary>
        private readonly IScheduleService scheduleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintSequenceHandler"/> class.
        /// </summary>
        public PrintSequenceHandler(IPersistenceContext context, IScheduleService scheduleService)
        {
            this.context = context;
            this.scheduleService = scheduleService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override PrintViewModel Handle(PrintSequence message)
        {
            var patient = this.context.Get<Patient>(message.Id);
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            var sequences = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                .And(x => x.Schedule.Id == schedule.Id);
            if (! message.OnNeedBasis)
            {
                sequences = sequences.AndNot(x => x.OnNeedBasis);
            }
            if (! message.StandardSequences)
            {
                sequences = sequences.And(x => x.OnNeedBasis);
            }
            var printable = this.scheduleService.PrintSchedule(message.StartDate, message.EndDate, sequences.List());
            var statusTaxons = schedule.ScheduleSettings.StatusTaxons.Count == 0 ? this.context.QueryOver<Taxon>().Where(x => x.IsActive && x.IsRoot).JoinQueryOver<Taxonomy>(x => x.Taxonomy).Where(x => x.MachineName == "SST").List() : schedule.ScheduleSettings.StatusTaxons.ToList();
            return new PrintViewModel
            {
                From = message.StartDate,
                To = message.EndDate,
                Patient = patient,
                Schedule = schedule.ScheduleSettings,
                StatusTaxons = statusTaxons,
                PrintSchedule = printable,
                EmptySchema = true
            };
        }

        #endregion
    }
}