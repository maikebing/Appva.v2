// <copyright file="DelegationOverviewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
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
    public sealed class DelegationOverviewModel
    {
        #region Properties.

        /// <summary>
        /// Accounts with delegations expiring in 50 days
        /// </summary>
        public IEnumerable<AccountModel> DelegationsExpiresWithin50Days 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Accounts with expired delegations
        /// </summary>
        public IEnumerable<AccountModel> DelegationsExpired 
        { 
            get; 
            set; 
        }

        #endregion
    }
}