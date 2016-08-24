// <copyright file="RecountInventoryItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RecountInventoryItem : Identity<InventoryTransactionItemViewModel>
    {
        /// <summary>
        /// The inventory ID.
        /// </summary>
        public Guid InventoryId
        {
            get;
            set;
        }

        /// <summary>
        /// The return URL.
        /// </summary>
        public string ReturnUrl
        {
            get;
            set;
        }
    }
}