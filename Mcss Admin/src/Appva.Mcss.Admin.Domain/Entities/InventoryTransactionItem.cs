// <copyright file="InventoryTransactionItem.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class InventoryTransactionItem : AggregateRoot<InventoryTransactionItem>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryTransactionItem"/> class.
        /// </summary>
        public InventoryTransactionItem()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="InventoryTransactionItem"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The inventory
        /// </summary>
        public virtual Inventory Inventory
        {
            get;
            set;
        }

        /// <summary>
        /// The Account which made the transaction
        /// </summary>
        public virtual Account Account
        {
            get;
            set;
        }

        /// <summary>
        /// The Sequence
        /// </summary>
        public virtual Sequence Sequence
        {
            get;
            set;
        }

        /// <summary>
        /// The Task
        /// </summary>
        public virtual Task Task
        {
            get;
            set;
        }

        /// <summary>
        /// The type of this transaction
        /// </summary>
        public virtual string Operation
        {
            get;
            set;
        }

        /// <summary>
        /// The value of the transaction
        /// </summary>
        public virtual double Value
        {
            get;
            set;
        }

        /// <summary>
        /// The level  of the inventory before the transaction
        /// </summary>
        public virtual double PreviousInventoryValue
        {
            get;
            set;
        }

        /// <summary>
        /// The level  of the inventory after the transaction
        /// </summary>
        public virtual double CurrentInventoryValue
        {
            get;
            set;
        }

        /// <summary>
        /// The reason or description of the transaction
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        #endregion
    }
}