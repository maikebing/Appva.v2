// <copyright file="InactivateInventoryHandler.cs" company="Appva AB">
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
    internal sealed class InactivateInventoryHandler : RequestHandler<InactivateInventory, ListInventory>
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateInventoryHandler"/> class.
        /// </summary>
        /// <param name="inventoryService">The <see cref="IInventoryService"/></param>
        public InactivateInventoryHandler(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        #endregion
    
        #region RequestHandler Overrides.  

        /// <inheritdoc />
        public override ListInventory Handle(InactivateInventory message)
        {
            var inventory = this.inventoryService.Find(message.Inventory);
            this.inventoryService.Inactivate(inventory);
            return new ListInventory
            {
                Id          = message.Id,
                InventoryId = message.Inventory
            };
        }

        #endregion
    }
}