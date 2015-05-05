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
    using System.Collections.Generic;
using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListInventory : IRequest<ListInventoryViewModel>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

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
    /// <param name="id">The patient id</param>
    /// <param name="inventoryId">The inventory id</param>
    /// <param name="year">Optional year</param>
    /// <param name="month">Optional month</param>
    /// <param name="startDate">Optional start date</param>
    /// <param name="endDate">Optional end date</param>
    /// <param name="page">Optional page - defaults to 1</param>
    //(Guid id, Guid? inventoryId, int? year, int? month, DateTime? startDate, DateTime? endDate, int page = 1
}