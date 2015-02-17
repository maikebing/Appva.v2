// <copyright file="InventoryModel.cs" company="Appva AB">
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
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Inventory model
    /// </summary>
    [JsonObject]
    public class InventoryModel
    {
        /// <summary>
        /// The ID of the <code>Inventory</code>.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The value to apply.
        /// </summary>
        [Required, JsonProperty(PropertyName = "value", Required = Required.AllowNull)]
        public double Value 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Reasons to make withdrawl
        /// </summary>
        [JsonProperty(PropertyName = "reasons")]
        public IList<string> Reasons
        {
            get;
            set;
        }

        /// <summary>
        /// The amounts available to withdrawl
        /// </summary>
        public IList<double> Amounts 
        { 
            get; 
            set; 
        }
    }
}