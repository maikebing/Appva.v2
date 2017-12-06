// <copyright file="ListOrdinationsResponse.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm.Models
{
    #region Imports.

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [JsonObject]
    public class ListOrdinationsResponse
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the ordinations.
        /// </summary>
        /// <value>
        /// The ordinations.
        /// </value>
        [JsonProperty("Ordinationer")]
        public IList<Ordination> Ordinations
        {
            get;
            set;
        }

        #endregion
    }
}
