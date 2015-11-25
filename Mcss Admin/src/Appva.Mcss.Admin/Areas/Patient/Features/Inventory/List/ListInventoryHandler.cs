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
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListInventoryHandler : RequestHandler<ListInventory, ListInventoryModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

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
            IPatientService patientService, 
            IAccountService accountService, 
            IIdentityService identityService,
            IInventoryService inventoryService,
            IAuditService auditing, 
            IPatientTransformer transformer)
        {
            this.patientService = patientService;
            this.accountService = accountService;
            this.identityService = identityService;
            this.inventoryService = inventoryService;
            this.auditing = auditing;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListInventoryModel Handle(ListInventory message)
        {
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

            var inventories = this.inventoryService.Search(message.Id);
            if (inventories == null || inventories.Count < 1)
            {
                return new ListInventoryModel
                {
                    Patient = this.transformer.ToPatient(patient)
                };
            }

            var currentInventory = message.InventoryId.HasValue ?
                inventories.Where(x => x.Id == message.InventoryId.Value).FirstOrDefault() :
                inventories.FirstOrDefault();

            var transactions = this.inventoryService.ListTransactionsFor(currentInventory.Id, startDate, endDate, message.Page.GetValueOrDefault(1), 50);
            this.auditing.Read(
                    patient,
                    "läste saldolista sida {0} för boende {1} (REF: {2}).",
                    transactions.CurrentPage,
                    patient.FullName,
                    patient.Id);
            return new ListInventoryModel
            {
                Patient = this.transformer.ToPatient(patient),
                ActiveInventories = inventories.Where(x => x.IsActive).ToDictionary<Inventory,Guid,string>(k => k.Id, e => e.Description),
                InactiveInventories = inventories.Where(x => !x.IsActive).ToDictionary<Inventory, Guid, string>(k => k.Id, e => e.Description),
                Inventory = currentInventory,
                Transactions = transactions.Entities,
                TotalTransactionCount = (int)transactions.TotalCount,
                PageSize = (int)transactions.PageSize,
                Page = (int)transactions.CurrentPage,
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