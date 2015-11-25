﻿// <copyright file="InactivateInventoryHandler.cs" company="Appva AB">
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
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InactivateInventoryHandler : RequestHandler<InactivateInventory, ListInventory>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventories;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListInventoryHandler"/> class.
        /// </summary>
        public InactivateInventoryHandler(IInventoryService inventories)
        {
            this.inventories = inventories;
        }

        #endregion
    
        #region RequestHandler Overrides.  

        /// <inheritdoc />
        public override ListInventory Handle(InactivateInventory message)
        {
            var inventory = this.inventories.Find(message.Inventory);
            this.inventories.Inactivate(inventory);
 	        return new ListInventory
            {
                Id = message.Id,
                InventoryId = message.Inventory
            };
        }

        #endregion
    }
}