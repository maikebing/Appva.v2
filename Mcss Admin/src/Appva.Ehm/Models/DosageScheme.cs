// <copyright file="DosageScheme.cs" company="Appva AB">
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
    public sealed class DosageScheme
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DosageScheme"/> class.
        /// </summary>
        public DosageScheme()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The day in a period this dosage is guilty
        /// </summary>
        [JsonProperty("intagstillfalle")]
        public IList<Dosage> Dosages
        {
            get;
            set;
        }

        /// <summary>
        /// The amount of the drug which should be given
        /// </summary>
        [JsonProperty("periodLangd")]
        public int PeriodLength
        {
            get;
            set;
        }

        #endregion
    }
}