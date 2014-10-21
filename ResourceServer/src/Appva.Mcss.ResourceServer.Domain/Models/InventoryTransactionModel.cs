// <copyright file="InventoryTransactionModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Inventory model
    /// </summary>
    /// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
    [JsonObject]
    public class InventoryTransactionModel
    {
        /// <summary>
        /// The type of the transaction. Eg withdrawl, add, count
        /// </summary>
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public string Type 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The Amount of the transaction
        /// </summary>
        [JsonProperty(PropertyName = "amount", Required = Required.Always)]
        public double Amount 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The id of the inventory the current transaction belongs to
        /// </summary>
        [JsonProperty(PropertyName = "inventory_id", Required = Required.Always)]
        public Guid InventoryId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Eventual reason for transaction
        /// </summary>
        [JsonProperty(PropertyName = "reason")]
        public string Reason 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The id of the inventory the current transaction belongs to
        /// </summary>
        [JsonProperty(PropertyName = "sequence_id")]
        public Guid SequenceId 
        { 
            get; 
            set; 
        }
    }
}