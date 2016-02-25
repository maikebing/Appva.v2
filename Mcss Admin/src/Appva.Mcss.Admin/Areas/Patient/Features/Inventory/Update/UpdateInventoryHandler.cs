// <copyright file="UpdateInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateInventoryHandler : RequestHandler<UpdateInventory, UpdateInventoryModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IInventoryService"/>
        /// </summary>
        private readonly IInventoryService inventories;

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInventoryHandler"/> class.
        /// </summary>
        public UpdateInventoryHandler(IInventoryService inventories, ISettingsService settings)
        {
            this.inventories = inventories;
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateInventoryModel Handle(UpdateInventory message)
        {
            var inventory = this.inventories.Find(message.Inventory);

            return new UpdateInventoryModel
            {
                Id = message.Id,
                Inventory = inventory.Id ,
                Amounts = inventory.Amounts != null ? string.Join(";", inventory.Amounts): string.Empty,
                Name = inventory.Description,
                Unit = inventory.Unit,
                AmountsList = this.settings.GetIventoryAmountLists().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = string.Join(";", x.Amounts)
                })
            };
        }

        #endregion
    }
}