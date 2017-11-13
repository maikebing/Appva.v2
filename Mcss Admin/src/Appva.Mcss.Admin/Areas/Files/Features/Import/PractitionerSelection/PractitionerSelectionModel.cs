// <copyright file="PractitionerSelectionModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Data;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerSelectionModel
    {
        #region Properties.

        /// <summary>
        /// The excel data.
        /// </summary>
        public DataTable Data
        {
            get;
            set;
        }

        /// <summary>
        /// Validate cells at the specified row.
        /// </summary>
        public int ValidateAtRow
        {
            get;
            set;
        }

        /// <summary>
        /// Reads data from the specified row.
        /// </summary>
        public int ReadFromRow
        {
            get;
            set;
        }

        /// <summary>
        /// The last row.
        /// </summary>
        public int LastRow
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies how many rows to skip before reading the first practitioner row.
        /// </summary>
        public int SkipRows
        {
            get;
            set;
        }

        /// <summary>
        /// The number of rows that will be previewed.
        /// </summary>
        public int PreviewRows
        {
            get;
            set;
        }

        #endregion
    }
}