// <copyright file="Inventory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class InventoryDefaults
    {
        #region Fields.

        /// <summary>
        /// The default amounts.
        /// </summary>
        public static readonly IReadOnlyCollection<double> AmountList = new List<double>
            { 
                 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 
                20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
                40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 59, 51, 52, 53, 54, 55, 56, 57, 58, 59, 
                60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 
                80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100
            };

        #endregion
        
        #region Static members

        public static List<InventoryAmountListModel> Units()
        {
            var zeropointfive = AmountList.ToList();
            zeropointfive.Insert(1, 0.5);
            return new List<InventoryAmountListModel>
                {
                    new InventoryAmountListModel
                    {
                        Id      = Guid.NewGuid(),
                        Name    = "dos",
                        Unit    = "st",
                        Amounts = zeropointfive
                    },
                    new InventoryAmountListModel
                    {
                        Id      = Guid.NewGuid(),
                        Name    = "ml",
                        Unit    = "ml",
                        Amounts = new List<double> { 0.2, 0.25, 0.5, 0.75, 1, 1.5, 2, 2.5, 3, 4, 5, 6, 7, 8, 9, 10 }
                    },
                    new InventoryAmountListModel
                    {
                        Id      = Guid.NewGuid(),
                        Name    = "plåster",
                        Unit    = "st",
                        Amounts = AmountList.ToList()
                    },
                    new InventoryAmountListModel
                    {
                        Id      = Guid.NewGuid(),
                        Name    = "tbl",
                        Unit    = "st",
                        Amounts = zeropointfive
                    }
                };
        }

        #endregion
    }
}