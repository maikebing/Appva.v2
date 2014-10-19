// <copyright file="CompletedDetailsModel.cs" company="Appva AB">
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
    /// TODO: Summary.
    /// </summary>
    [JsonObject]
    public class CompletedDetailsModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CompletedDetailsModel"/> class.
        /// </summary>
        public CompletedDetailsModel()
        {
            this.Accounts = new List<string>();
        }

        #endregion

        /// <summary>
        /// TODO: Accounts.
        /// </summary>
        [JsonProperty(PropertyName = "accounts")]
        public IList<string> Accounts 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Time.
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public DateTime Time 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Status.
        /// </summary>
        [JsonProperty(PropertyName="status")]
        public StatusItemModel Status 
        { 
            get; 
            set; 
        }
    }
}
