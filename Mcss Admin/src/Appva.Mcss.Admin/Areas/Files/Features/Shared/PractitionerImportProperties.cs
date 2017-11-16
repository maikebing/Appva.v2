// <copyright file="PractitionerImportProperties.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerImportProperties
    {
        #region Properties.

        /// <summary>
        /// Read practitioner data from the specified row.
        /// </summary>
        public int? SelectedFirstRow
        {
            get;
            set;
        }

        /// <summary>
        /// Read practitioner data to the specified row.
        /// </summary>
        public int? SelectedLastRow
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if the file has importable practitioners.
        /// </summary>
        public bool IsImportable
        {
            get;
            set;
        }

        #endregion
    }
}