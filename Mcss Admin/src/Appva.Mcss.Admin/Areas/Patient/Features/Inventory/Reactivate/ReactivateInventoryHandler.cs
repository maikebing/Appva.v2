// <copyright file="ReactivateInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ReactivateInventoryHandler : RequestHandler<ReactivateInventory, ListInventory>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactivateInventoryHandler"/> class.
        /// </summary>
        /// <param name="inventoryService">The <see cref="IInventoryService"/></param>
        public ReactivateInventoryHandler(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListInventory Handle(ReactivateInventory message)
        {
            var inventory = this.inventoryService.Find(message.Inventory);
            this.inventoryService.Reactivate(inventory);
            return new ListInventory
            {
                Id          = message.Id,
                InventoryId = message.Inventory
            };
        }

        #endregion
    }
}