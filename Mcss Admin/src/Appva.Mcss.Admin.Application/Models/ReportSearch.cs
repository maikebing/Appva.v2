// <copyright file="ReportSearch.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ReportSearch<T>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportSearch"/> class.
        /// </summary>
        public ReportSearch()
        {
        }

        #endregion

        public int PageSize
        {
            get;
            set;
        }
        public int PageNumber
        {
            get;
            set;
        }
        public int TotalItemCount
        {
            get;
            set;
        }
        public IList<T> Items
        {
            get;
            set;
        }
    }
}