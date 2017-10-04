// <copyright file="User.cs" company="Appva AB">
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
    public class User
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the prescriber code.
        /// </summary>
        /// <value>
        /// The prescriber code.
        /// </value>
        [JsonProperty("forskrivarkod")]
        public string PrescriberCode
        {
            get;
            set;
        }

        [JsonProperty("legitimationskod")]
        public string Legitimation
        {
            get;
            set;
        }

        #endregion
    }
}