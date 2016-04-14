// <copyright file="ListInventoryModel.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListInventoryModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListInventoryModel"/> class.
        /// </summary>
        public ListInventoryModel()
        {
            this.OperationTranslationDictionary = new Dictionary<string, string> 
            {
                { "withdrawal", "Uttag"           },
                { "add",        "Insättning"      },
                { "recount",    "Kontrollräkning" },
                { "readd",      "Återförd mängd"  }
            };
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The patient
        /// </summary>
        public PatientViewModel Patient 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The active inventories
        /// </summary>
        public IDictionary<Guid, string> ActiveInventories 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The inactive inventories
        /// </summary>
        public IDictionary<Guid, string> InactiveInventories
        {
            get;
            set;
        }

        /// <summary>
        /// The current inventory
        /// </summary>
        public Inventory Inventory
        {
            get;
            set;
        }

        /// <summary>
        /// Filtered year
        /// </summary>
        public int? Year 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Filtered month
        /// </summary>
        public int? Month 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Filterable years
        /// </summary>
        public IEnumerable<SelectListItem> Years 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Filterable months
        /// </summary>
        public IEnumerable<SelectListItem> Months 
        {
            get;
            set; 
        }

        /// <summary>
        /// The end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Total count of transactions.
        /// </summary>
        public int TotalTransactionCount { get; set; }

        /// <summary>
        /// Current pagesize of transactions.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Current page of transactions.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The dictionary with translations for operations.
        /// </summary>
        public IDictionary<string, string> OperationTranslationDictionary
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// The transactions.
        /// </summary>
        public IList<InventoryTransactionItem> Transactions { get; set; }
    }
}