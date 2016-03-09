// <copyright file="ListInventoriesModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
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
    public sealed class ListInventoriesModel
    {
        #region Properties

        /// <summary>
        /// The units
        /// </summary>
        public IList<InventoryAmountListModel> Units
        {
            get;
            set;
        }

        #endregion
    }
}