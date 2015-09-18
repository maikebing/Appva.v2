// <copyright file="ReportSearch.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The entry type</typeparam>
    public sealed class ReportSearch<T>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportSearch{T}"/> class.
        /// </summary>
        public ReportSearch()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The entries per page size.
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// The current page number.
        /// </summary>
        public int PageNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The total entries count.
        /// </summary>
        public int TotalItemCount
        {
            get;
            set;
        }

        /// <summary>
        /// The list of entries.
        /// </summary>
        public IList<T> Items
        {
            get;
            set;
        }

        #endregion
    }
}