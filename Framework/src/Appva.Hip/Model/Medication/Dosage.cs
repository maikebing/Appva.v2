// <copyright file="Dosage.cs" company="Appva AB">
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
    public sealed class Dosage
    {
        [JsonProperty(PropertyName = "lengthOfTreatment")]
        public Interval LengthOfTreatment
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "isMaximumTreatmentTime")]
        public bool? IsMaximumTreatmentTime
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "dosageInstruction")]
        public string DosageInstruction
        { 
            get;
            set;
        }

        [JsonProperty(PropertyName = "unitDose")]
        public string UnitDose
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "shortNotation")]
        public string ShortNotation
        {
            get;
            set;
        }
    }
}