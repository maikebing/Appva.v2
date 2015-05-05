// <copyright file="ListInventoryHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListInventoryHandler : RequestHandler<ListInventory, ListInventoryViewModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAlertHandler"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IPatientService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="ILogService"/> implementation</param>
        public ListInventoryHandler(
            IPatientService patientService, ITaskService taskService, ILogService logService, IPersistenceContext persistence,
            IPatientTransformer transformer)
        {
            this.patientService = patientService;
            this.taskService = taskService;
            this.logService = logService;
            this.persistence = persistence;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListInventoryViewModel Handle(ListInventory message)
        {
            var page = message.Page ?? 1;
            var pageSize = 50;
            var patient = this.patientService.Get(message.Id);
            var startDate = message.StartDate ?? DateTime.Now.FirstOfMonth();
            var endDate = message.EndDate ?? DateTime.Now.LastOfMonth().LastInstantOfDay();
            if (message.Year.HasValue)
            {
                startDate = DateTimeUtilities.January(1, message.Year.Value);
                endDate = DateTimeUtilities.December(31, message.Year.Value).LastInstantOfDay();
            }
            if (message.Month.HasValue)
            {
                if (! message.Year.HasValue)
                {
                    message.Year = DateTime.Now.Year;
                }
                startDate = new DateTime(message.Year.Value, message.Month.Value, 1);
                endDate = new DateTime(message.Year.Value, message.Month.Value, 
                    DateTime.DaysInMonth(message.Year.Value, message.Month.Value)).LastInstantOfDay();
            }
            var inventories = this.persistence.QueryOver<Sequence>()
                .Where(x => x.Patient.Id == message.Id)
                .JoinQueryOver<Inventory>(x => x.Inventory)
                    .Where(x => x.IsActive)
                .List()
                .Select(x => x.Inventory).ToList();
            if (inventories == null || inventories.Count < 1)
            {
                return new ListInventoryViewModel
                {
                    Patient = this.transformer.ToPatient(patient)
                };
            }
            var inventory = this.persistence.QueryOver<Inventory>()
                .Where(x => x.Id == message.InventoryId.GetValueOrDefault(inventories.FirstOrDefault().Id))
                .SingleOrDefault();
            var transactions = this.persistence.QueryOver<InventoryTransactionItem>()
                .Where(x => x.IsActive)
                .And(x => x.Inventory == inventory)
                .And(x => x.CreatedAt >= startDate)
                .And(x => x.CreatedAt <= endDate)
                .OrderBy(x => x.CreatedAt).Desc;
            /*var user = this.Identity();
            this.logService.Info(
                    "Användare {0} läste saldolista sida {1} för boende {2} (REF: {3})."
                    .FormatWith(
                        user.UserName,
                        page,
                        patient.FullName,
                        patient.Id),
                    user,
                    patient,
                    LogType.Read);*/
            return new ListInventoryViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Inventories = inventories,
                Inventory = inventory,
                Transactions = transactions.Skip((page - 1) * pageSize).Take(pageSize).Future().ToList(),
                TotalTransactionCount = transactions.ToRowCountQuery().FutureValue<int>().Value,
                PageSize = pageSize,
                Page = page,
                StartDate = startDate,
                EndDate = endDate,
                Years = DateTimeUtils.GetYearSelectList(patient.CreatedAt.Year, startDate.Year == endDate.Year ? startDate.Year : 0),
                Months = startDate.Month == endDate.Month ? DateTimeUtils.GetMonthSelectList(startDate.Month) : DateTimeUtils.GetMonthSelectList(),
                Year = message.Year,
                Month = message.Month
            };
        }

        #endregion
    }
}