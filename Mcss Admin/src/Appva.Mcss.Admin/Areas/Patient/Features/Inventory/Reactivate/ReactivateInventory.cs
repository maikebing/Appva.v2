// <copyright file="ReactivateInventory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ReactivateInventory : Identity<ListInventory>
    {
        #region Properties.

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