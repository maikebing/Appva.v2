// <copyright file="SearchDeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SearchDeviceModel
    {
        #region Properties.

        public bool? IsActive
        {
            get;
            set;
        }

        public string SearchQuery
        {
            get;
            set;
        }

        /// <summary>
        /// What the devices should be ordered by
        /// </summary>
        public string OrderBy
        {
            get; set;
        }
        #endregion
    }
}