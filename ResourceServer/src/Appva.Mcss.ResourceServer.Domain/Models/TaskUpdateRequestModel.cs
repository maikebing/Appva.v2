// <copyright file="TaskUpdateRequestModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
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
    public class TaskUpdateRequestModel
    {
        /// <summary>
        /// The id of the <code>Taxon</code>.
        /// </summary>
        [JsonProperty(PropertyName = "status_id")]
        public string StatusId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The id of the <code>Account</code>.
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public Guid AccountId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Active
        /// </summary>
        [JsonProperty(PropertyName = "active")]
        public bool Active 
        { 
            get; 
            set; 
        }
    }
}