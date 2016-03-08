// <copyright file="Inventory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Inventory : AggregateRoot<Inventory>
    {
        #region Fields.
        
        /// <summary>
        /// The amountlist as a stored string
        /// </summary>
        private string amounts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Inventory"/> class.
        /// </summary>
        public Inventory()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The current level of the inventory
        /// </summary>
        public virtual double CurrentLevel
        {
            get;
            set;
        }

        /// <summary>
        /// All transactions in the current inventory
        /// </summary>
        public virtual IList<InventoryTransactionItem> Transactions
        {
            get;
            set;
        }

        /// <summary>
        /// Last time the inventory was checked and recounted
        /// </summary>
        public virtual DateTime? LastRecount
        {
            get;
            set;
        }

        /// <summary>
        /// Description of the inventory 
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The unit for the inventory
        /// </summary>
        public virtual string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// The patient
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The amounts represented as a list
        /// </summary>
        public virtual IList<double> Amounts
        {
            get 
            {
                if (this.amounts == null)
                    return null;
                return JsonConvert.DeserializeObject<List<double>>(this.amounts);
            }
            set
            {
                if (value == null)
                {
                    this.amounts = null;
                }
                else
                {
                    this.amounts = JsonConvert.SerializeObject(value);
                }
            }
        }

        #endregion
    }
}