// <copyright file="UpdateInventoryPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers {
    
    #region Imports.

    using Appva.Cqrs;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateInventoryPublisher : RequestHandler<UpdateInventoryModel, ListInventory>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventories;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInventoryPublisher"/> class.
        /// </summary>
        public UpdateInventoryPublisher(IInventoryService inventories, ISettingsService settings)
        {
            this.inventories = inventories;
            this.settings = settings;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override ListInventory Handle(UpdateInventoryModel message)
        {
            var amounts = message.Amounts.IsNotEmpty() ? this.settings.GetIventoryAmountLists().Where(x => x.Name == message.Amounts).FirstOrDefault() : new InventoryAmountListModel();
            this.inventories.Update(message.Inventory, message.Name, amounts.Name, amounts.Amounts);           

            return new ListInventory 
            {
                Id = message.Id,
                InventoryId = message.Inventory
            };
        }

        #endregion
    }
}