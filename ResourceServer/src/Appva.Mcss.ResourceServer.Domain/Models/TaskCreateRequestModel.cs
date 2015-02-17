// <copyright file="TaskCreateRequestModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [JsonObject]
    public class TaskCreateRequestModel
    {
        /// <summary>
        /// The id of the <code>Task</code>.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid? Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The id of the <code>Sequence</code>.
        /// </summary>
        [JsonProperty(PropertyName = "sequence_id", Required = Required.Always)]
        public Guid SequenceId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The id of the <code>Taxon</code>.
        /// </summary>
        [JsonProperty(PropertyName = "status_id")]
        public Guid? StatusId { get; set; }

        /// <summary>
        /// The datetime when the <code>Task</code> is scheduled.
        /// </summary>
        [JsonProperty(PropertyName = "date_time_scheduled", Required = Required.Always)]
        public DateTime DateTimeScheduled { get; set; }

        /// <summary>
        /// Whether or notthe task is needs based.
        /// </summary>
        [JsonProperty(PropertyName = "is_needs_based", Required = Required.Always)]
        public bool IsNeedsBased 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// FIXME: Oauth remove
        /// The id of the <code>Account</code>.
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public Guid AccountId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: InventoryIds.
        /// </summary>
        [JsonProperty(PropertyName = "inventory_ids")]
        public List<Guid> InventoryIds 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If someone was contacted because of a deviation
        /// </summary>
        [JsonProperty(PropertyName = "contacted_id")]
        public Guid ContactedId
        {
            get;
            set;
        }
    }
}