// <copyright file="UpdateInventoryPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers 
{    
    #region Imports.

    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateInventoryPublisher : RequestHandler<UpdateInventoryModel, ListInventory>
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
        /// Initializes a new instance of the <see cref="UpdateInventoryPublisher"/> class.
        /// </summary>
        /// <param name="inventoryService">The <see cref="IInventoryService"/></param>
        /// <param name="settingService">The <see cref="ISettingsService"/></param>
        public UpdateInventoryPublisher(IInventoryService inventoryService, ISettingsService settingService)
        {
            this.inventoryService = inventoryService;
            this.settingService   = settingService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListInventory Handle(UpdateInventoryModel message)
        {
            var amounts = message.Amounts.IsNotEmpty() ? this.settingService.GetIventoryAmountLists().Where(x => x.Name == message.Amounts).FirstOrDefault() : new InventoryAmountListModel();
            this.inventoryService.Update(message.Inventory, message.Name, amounts.Name, amounts.Amounts);           
            return new ListInventory 
            {
                Id          = message.Id,
                InventoryId = message.Inventory
            };
        }

        #endregion
    }
}