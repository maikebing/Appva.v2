// <copyright file="InventoryAmountListModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    using Appva.Mcss.Admin.Application.Common;
    using Newtonsoft.Json;
    #region Imports.

    using System;
using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [JsonObject]
    public sealed class InventoryAmountListModel
    {
        #region Properties

        /// <summary>
        /// The inventory id
        /// </summary>
        [JsonProperty("Id")]
        public Guid Id
        {
            get;
            set;
        }


        /// <summary>
        /// The name of the scale/list.
        /// </summary>
        [JsonProperty("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The unit in use
        /// </summary>
        [JsonProperty("Unit")]
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// The Field of usage
        /// </summary>
        [JsonProperty("Field")]
        public string Field
        {
            get;
            set;
        }

        public InventoryDefaults.Feature Feature { get; set; }


        /// <summary>
        /// List of amounts in current list.
        /// </summary>
        [JsonProperty("Amounts")]
        public IList<double> Amounts
        {
            get;
            set;
        }

        #endregion
    }
}