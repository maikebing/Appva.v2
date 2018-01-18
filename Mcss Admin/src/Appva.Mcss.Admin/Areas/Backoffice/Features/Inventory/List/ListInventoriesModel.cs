// <copyright file="ListInventoriesModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListInventoriesModel
    {
        #region Properties

        /// <summary>
        /// The units.
        /// </summary>
        public IList<InventoryAmountListModel> InventoryUnits
        {
            get;
            set;
        }

        public IList<AdministrationValueModel> AdministrationUnits
        {
            get;
            set;
        }

        #endregion
    }
}