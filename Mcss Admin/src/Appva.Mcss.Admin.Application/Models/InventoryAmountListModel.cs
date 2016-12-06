// <copyright file="InventoryAmountListModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InventoryAmountListModel
    {
        #region Properties

        /// <summary>
        /// The inventory id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the list.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// List of amounts in current list.
        /// </summary>
        public IList<double> Amounts
        {
            get;
            set;
        }

        #endregion
    }
}