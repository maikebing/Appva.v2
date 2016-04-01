// <copyright file="Class1.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class OverviewInventoryModel
    {
        #region Properties.

        /// <summary>
        /// The upcomming stock counts
        /// </summary>
        public IList<Inventory> CommingStockCounts 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Delayed stock counts
        /// </summary>
        public IList<Inventory> DelayedStockCounts 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The recount interval in days
        /// </summary>
        public int StockControlIntervalInDays
        {
            get;
            set;
        }

        #endregion
    }
}