// <copyright file="InventoryAmountListModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    using Newtonsoft.Json;
    #region Imports.

    using System;
using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InventoryAmountListModel
    {
        #region Properties

        /// <summary>
        /// The inventory id
        /// </summary>
        //[JsonProperty("id")]
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the list.
        /// </summary>
        //[JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The unit.
        /// </summary>
        //[JsonProperty("unit")]
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// List of amounts in current list.
        /// </summary>
        //[JsonProperty("amounts")]
        public IList<double> Amounts
        {
            get;
            set;
        }

        #endregion
    }
}