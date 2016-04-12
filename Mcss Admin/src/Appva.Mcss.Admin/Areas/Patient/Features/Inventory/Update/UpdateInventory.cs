// <copyright file="UpdateInventory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateInventory : Identity<UpdateInventoryModel>
    {
        #region Properties

        /// <summary>
        /// The inventory id
        /// </summary>
        public Guid Inventory
        {
            get;
            set;
        }

        #endregion
    }
}