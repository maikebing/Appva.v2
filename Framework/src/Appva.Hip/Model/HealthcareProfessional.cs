// <copyright file="HealthcareProfessional.cs" company="Appva AB">
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
    public sealed class HealthcareProfessional
    {
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "hsaId")]
        public string HsaId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "role")]
        public string Role
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "careUnitHsaId")]
        public string CareUnitHsaId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "orgUnit")]
        public OrgUnit OrgUnit
        {
            get;
            set;
        }
    }
}