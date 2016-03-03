// <copyright file="RecountInventoryItemHandler.cs" company="Appva AB">
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
using Appva.Mcss.Admin.Application.Security.Identity;
using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class OverviewInventoryHandler : RequestHandler<OverviewInventory, OverviewInventoryModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventories;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewInventoryHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="taskService">The <see cref="ITaskService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        /// <param name="filtering">The <see cref="ITaxonFilterSessionHandler"/></param>
        public OverviewInventoryHandler(
            IIdentityService identityService,
            ITaskService taskService, 
            IInventoryService inventories,
            ISettingsService settings,
            ITaxonFilterSessionHandler filtering)
        {
            this.identityService = identityService;
            this.taskService = taskService;
            this.inventories = inventories;
            this.settings = settings;
            this.filtering = filtering;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override OverviewInventoryModel Handle(OverviewInventory message)
        {
            var inventoryCalculationSpanInDays = this.settings.Find<int>(ApplicationSettings.InventoryCalculationSpanInDays);
            var taxon = this.filtering.GetCurrentFilter();
            var today = DateTime.Now.Date;
            var lastStockCalculationDate = today.AddDays(-inventoryCalculationSpanInDays);

            var delayedStockCounts = this.inventories.ListRecountsBefore(lastStockCalculationDate, taxon.Id);
            var commingStockCounts = this.inventories.ListRecountsBefore(lastStockCalculationDate.AddDays(7), taxon.Id);
            /*RecountOverviewItemViewModel dto = null;
            Inventory inventory = null;
            Patient patient = null;
            Schedule schedule = null;
            ScheduleSettings scheduleSettings = null;
            var account = this.persistence.Get<Account>(this.identityService.PrincipalId);
            var scheduleList = TaskService.GetRoleScheduleSettingsList(account);

            var query = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                .JoinAlias(x => x.Schedule, () => schedule)
                    .Where(() => schedule.IsActive)
                    .JoinAlias(x => schedule.ScheduleSettings, () => scheduleSettings)
                        .WhereRestrictionOn(() => scheduleSettings.Id).IsIn(scheduleList.Select(x => x.Id).ToArray())
                .JoinAlias(x => x.Inventory, () => inventory)
                    .Where(() => inventory.LastRecount < lastStockCalculationDate)
                    .And(() => inventory.IsActive)
                .OrderBy(() => inventory.LastRecount).Asc
                .JoinAlias(x => x.Patient, () => patient)
                    .Where(() => patient.IsActive && !patient.Deceased)
                    .JoinQueryOver<Taxon>(() => patient.Taxon)
                        .WhereRestrictionOn(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere)
                .SelectList(list => list
                    .Select(() => patient.FullName).WithAlias(() => dto.Patient)
                    .Select(() => patient.Id).WithAlias(() => dto.PatientId)
                    .Select(() => inventory.LastRecount).WithAlias(() => dto.LastRecount)
                    .Select(() => inventory.Id).WithAlias(() => dto.InventoryId)
                    .Select(() => inventory.Description).WithAlias(() => dto.Name)
                );
            var allStockCounts = query.TransformUsing(Transformers.AliasToBean<RecountOverviewItemViewModel>())
                .List<RecountOverviewItemViewModel>();*/
            return new OverviewInventoryModel
            {
                DelayedStockCounts = delayedStockCounts,
                CommingStockCounts = commingStockCounts,
                StockControlIntervalInDays = inventoryCalculationSpanInDays
            };
        }

        #endregion
    }
}