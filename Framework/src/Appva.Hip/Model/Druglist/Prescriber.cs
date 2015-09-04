// <copyright file="Presciber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip.Model
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
    public sealed class Prescriber
    {
        [JsonProperty(PropertyName = "workplaceName")]
        public string WorkplaceName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "workplaceLocation")]
        public string WorkplaceLocation
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "postalAddress")]
        public string PostalAddress
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "postalNumber")]
        public string PostalNumber
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "specialities")]
        public IEnumerable<string> Specialities
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "occupation")]
        public string Occupation
        {
            get;
            set;
        }
    }
}