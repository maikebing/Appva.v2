// <copyright file="ListInventory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListInventory : Identity<ListInventoryModel>
    {
        /// <summary>
        /// Optional inventory ID.
        /// </summary>
        public Guid? InventoryId
        {
            get;
            set;
        }

        /// <summary>
        /// Optional year.
        /// </summary>
        public int? Year
        {
            get;
            set;
        }

        /// <summary>
        /// Optional month.
        /// </summary>
        public int? Month
        {
            get;
            set;
        }

        /// <summary>
        /// Optional start date.
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Optional end date.
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Optional page, defaults to 1.
        /// </summary>
        public int? Page
        {
            get;
            set;
        }
    }
}