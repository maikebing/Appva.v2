// <copyright file="DelegationOverviewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationOverviewModel
    {
        #region Properties.

        /// <summary>
        /// Accounts with delegations expiring in 50 days
        /// </summary>
        public IEnumerable<DelegationExpired> DelegationsExpiresWithin50Days 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Accounts with expired delegations
        /// </summary>
        public IEnumerable<DelegationExpired> DelegationsExpired 
        { 
            get; 
            set; 
        }

        #endregion
    }

    public class DelegationExpired
    {
        /// <summary>
        /// The account-id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The account fulname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Days left before expire
        /// </summary>
        public int DaysLeft { get; set; }
    }
}