// <copyright file="DosageScaleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author></author>

namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DosageScaleModel
    {
        /// <summary>
        /// The <see cref="Guid"/>.
        /// </summary>
        public Guid Id {
            get;
            internal set;
        }

        /// <summary>
        /// The dosage name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The unit.
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// A collection of dosage values.
        /// </summary>
        public string Values
        {
            get;
            set;
        }
    }
}
