// <copyright file="ListLdapModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LdapSyncModel : IRequest<LdapSync>
    {
        #region Properties.

        /// <summary>
        /// Accounts not found in Ldap
        /// </summary>
        public List<Account> NotInLdap
        {
            get;
            set;
        }

        /// <summary>
        /// Accounts ready for sync
        /// </summary>
        public List<Tickable> ReadyToSync
        {
            get;
            set;
        }

        /// <summary>
        /// Accounts with differences that needs to be checked
        /// </summary>
        public List<Account> NotReadyForSync
        {
            get;
            set;
        }

        /// <summary>
        /// All synchronized accounts
        /// </summary>
        public List<Account> Synchronized
        {
            get;
            set;
        }

        #endregion

    }
}