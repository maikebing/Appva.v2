// <copyright file="ListLogModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Log.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListLogModel
    {
        #region Properties.

        /// <summary>
        /// The entities
        /// </summary>
        public IList<LogModel> Logs
        {
            get;
            set;
        }

        /// <summary>
        /// The page
        /// </summary>
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// The pagesize
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// The total count
        /// </summary>
        public int TotalCount 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The tenant-name
        /// </summary>
        public string TenantName
        {
            get;
            set;
        }

        #endregion
    }
}