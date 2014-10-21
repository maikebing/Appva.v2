// <copyright file="HydratedTaskModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Task model.
    /// </summary>
    [JsonObject]
    public class HydratedTaskModel : BaseTaskModel
    {
        /// <summary>
        /// The id of the sequence, needed to sign a task
        /// </summary>
        [JsonProperty(PropertyName = "sequence_id")]
        public Guid SequenceId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The description of the <code>Task</code> 
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The permissions the current user has to the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "permissions")]
        public List<string> Permissions 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Statuses available for the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "status_items")]
        public List<StatusItemModel> StatusItems 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Contact-dialogs for the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "contacts")]
        public Dictionary<string, ContactModel> Contacts 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If refill is available for the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "refill")]
        public RefillModel Refill 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The inventory for the <code>Task</code>. If null, no inventory
        /// </summary>
        [JsonProperty(PropertyName = "inventory")]
        public InventoryModel Inventory 
        { 
            get; 
            set; 
        }
    }
}