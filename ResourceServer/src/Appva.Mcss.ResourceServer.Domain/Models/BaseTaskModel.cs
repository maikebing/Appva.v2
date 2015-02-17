// <copyright file="BaseTaskModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
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
    public class BaseTaskModel
    {
        /// <summary>
        /// The id of the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The category of the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public string Category 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The types of the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public List<string> Type 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The name of the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The datetime when the <code>Task</code> is scheduled
        /// </summary>
        [JsonProperty(PropertyName = "date_time_scheduled")]
        public string DateTimeScheduled 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Start and end datetimes for the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "date_time_interval")]
        public List<string> DateTimeInterval 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Statuses of the <code>Task</code>
        /// </summary>
        [JsonProperty(PropertyName = "statuses")]
        public List<string> Statuses 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Returns a <code>CompletedDetailsModel</code> if task is completed
        /// </summary>
        [JsonProperty(PropertyName = "completed")]
        public CompletedDetailsModel Completed 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The last time this task was completed, used in Need Based  tasks
        /// </summary>
        [JsonProperty(PropertyName = "last_completion")]
        public DateTime? LastCompletion 
        { 
            get; 
            set; 
        }
    }
}