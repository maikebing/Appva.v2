// <copyright file="Dosage.cs" company="Appva AB">
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
    public sealed class Dosage
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Dosage"/> class.
        /// </summary>
        public Dosage()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The day in a period this dosage is guilty
        /// </summary>
        [JsonProperty("dagIPeriod")]
        public int DayInPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// The amount of the drug which should be given
        /// </summary>
        [JsonProperty("intagsmangd")]
        public double? Amount
        {
            get;
            set;
        }

        /// <summary>
        /// The time which the dosage should be given
        /// </summary>
        [JsonProperty("klockslag")]
        public int Time
        {
            get;
            set;
        }

        #endregion
    }
}