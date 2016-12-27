// <copyright file="ArchivePatient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArchivePatient : Identity<ListPatient>
    {
        #region Properties

        /// <summary>
        /// The search query.
        /// </summary>
        public string SearchQuery
        {
            get;
            set;
        }

        /// <summary>
        /// The current page in the set.
        /// </summary>
        public int? Page
        {
            get;
            set;
        }

        /// <summary>
        /// Optional is active query filter - defaults to true.
        /// </summary>
        public bool? IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Optional is deceased query filter - defaults to false
        /// </summary>
        public bool? IsDeceased
        {
            get;
            set;
        }

        #endregion
    }
}