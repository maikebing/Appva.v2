// <copyright file="PrintPreparationHandler.cs" company="Appva AB">
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
    internal sealed class PrintPreparationHandler : RequestHandler<PrintPreparation, PreparePrintViewModel>
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
        /// Initializes a new instance of the <see cref="PrintPreparationHandler"/> class.
        /// </summary>
        public PrintPreparationHandler(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override PreparePrintViewModel Handle(PrintPreparation message)
        {
            var startDate = message.StartDate.Date;
            var endDate = message.EndDate.LastInstantOfDay();
            var patient = this.persistence.Get<Patient>(message.Id);
            var schedule = this.persistence.Get<Schedule>(message.ScheduleId);
            var tasks = this.persistence.QueryOver<PreparedTask>()
                .Where(x => x.Schedule.Id == schedule.Id)
                .And(x => x.Date >= startDate && x.Date <= endDate)
                .List();
            var printSchedule = new Dictionary<DateTime, IDictionary<string, IDictionary<int, string>>>();
            var signatures = new Dictionary<int, IDictionary<string, string>>();
            foreach (var task in tasks)
            {
                var date = task.Date;
                if (!printSchedule.ContainsKey(date.FirstOfMonth()))
                {
                    printSchedule.Add(date.FirstOfMonth(), new Dictionary<string, IDictionary<int, string>>());
                }
                var seqUID = string.Format("{0}:{1}", task.PreparedSequence.Name, task.PreparedSequence.Id);
                if (!printSchedule[date.FirstOfMonth()].ContainsKey(seqUID))
                {
                    printSchedule[date.FirstOfMonth()].Add(seqUID, new Dictionary<int, string>());
                }
                if (!signatures.ContainsKey(date.Month))
                {
                    signatures.Add(date.Month, new Dictionary<string, string>());
                }
                var uid = string.Format("{0}:{1}", task.PreparedBy.FullName, task.PreparedBy.Id);
                if (!signatures[date.Month].ContainsKey(uid))
                {
                    var sign = string.Format("{0}{1}", task.PreparedBy.FirstName.Substring(0, 1), task.PreparedBy.LastName.Substring(0, 1));
                    if (signatures[date.Month].Values.Contains(sign))
                    {
                        var counter = 2;
                        while (signatures[date.Month].Values.Contains(string.Format("{0}{1}", sign, counter)))
                        {
                            counter++;
                        }
                        sign = string.Format("{0}{1}", sign, counter);
                    }
                    signatures[date.Month].Add(uid, sign);
                }
                if (!printSchedule[date.FirstOfMonth()][seqUID].ContainsKey(date.Day))
                {
                    printSchedule[date.FirstOfMonth()][seqUID].Add(date.Day, signatures[date.Month][uid]);
                }
            }
            return new PreparePrintViewModel
            {
                PrintSchedule = printSchedule,
                Schedule = schedule,
                Patient = patient,
                Signatures = signatures
            };
        }

        #endregion
    }
}