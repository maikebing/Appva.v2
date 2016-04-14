// <copyright file="UpdateInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateInventoryHandler : RequestHandler<UpdateInventory, UpdateInventoryModel>
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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInventoryHandler"/> class.
        /// </summary>
        /// <param name="inventoryService">The <see cref="IInventoryService"/></param>
        /// <param name="settingService">The <see cref="ISettingsService"/></param>
        public UpdateInventoryHandler(IInventoryService inventoryService, ISettingsService settingService)
        {
            this.inventoryService = inventoryService;
            this.settingService   = settingService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateInventoryModel Handle(UpdateInventory message)
        {
            var inventory = this.inventoryService.Find(message.Inventory);
            return new UpdateInventoryModel
            {
                Id          = message.Id,
                Inventory   = inventory.Id,
                Amounts     = inventory.Unit,
                Name        = inventory.Description,
                AmountsList = this.settingService.GetIventoryAmountLists().Select(x => new SelectListItem()
                {
                    Text  = x.Name,
                    Value = x.Name
                })
            };
        }

        #endregion
    }
}