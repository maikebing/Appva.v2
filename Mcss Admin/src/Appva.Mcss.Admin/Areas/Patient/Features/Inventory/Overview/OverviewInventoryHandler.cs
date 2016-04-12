// <copyright file="OverviewInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class OverviewInventoryHandler : RequestHandler<OverviewInventory, OverviewInventoryModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingService;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewInventoryHandler"/> class.
        /// </summary>
        /// <param name="inventoryService">The <see cref="IInventoryService"/></param>
        /// <param name="settingService">The <see cref="ISettingsService"/></param>
        /// <param name="filtering">The <see cref="ITaxonFilterSessionHandler"/></param>
        public OverviewInventoryHandler(
            IInventoryService inventoryService,
            ISettingsService settingService,
            ITaxonFilterSessionHandler filtering)
        {
            this.inventoryService = inventoryService;
            this.settingService   = settingService;
            this.filtering        = filtering;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override OverviewInventoryModel Handle(OverviewInventory message)
        {
            var inventoryCalculationSpanInDays = this.settingService.Find<int>(ApplicationSettings.InventoryCalculationSpanInDays);
            var taxon = this.filtering.GetCurrentFilter();
            var today = DateTime.Now.Date;
            var lastStockCalculationDate = today.AddDays(-inventoryCalculationSpanInDays);
            var delayedStockCounts = this.inventoryService.ListRecountsBefore(lastStockCalculationDate, null, taxon.Id);
            var commingStockCounts = this.inventoryService.ListRecountsBefore(lastStockCalculationDate.AddDays(7), lastStockCalculationDate, taxon.Id);
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