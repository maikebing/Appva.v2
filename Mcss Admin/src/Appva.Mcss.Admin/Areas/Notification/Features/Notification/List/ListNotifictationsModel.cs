// <copyright file="ListNotifictationsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListNotifictationsModel
    {
        #region Propeties.

        /// <summary>
        /// The list of <see cref="Notification" />
        /// </summary>
        public IEnumerable<Notification> Notifications
        {
            get;
            set;
        }

        /// <summary>
        /// The current page
        /// </summary>
        public int Page
        { 
            get; 
            set;
        }

        /// <summary>
        /// The current page-size
        /// </summary>
        public int PageSize 
        { 
            get;
            set;
        }

        /// <summary>
        /// The total count of notifications
        /// </summary>
        public int Total 
        { 
            get; 
            set; 
        }

        #endregion
    }
}