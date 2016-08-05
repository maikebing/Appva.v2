// <copyright file="GetSynchronizedAccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using Appva.Ldap.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class GetSynchronizedAccountModel
    {
        
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSynchronizedAccountModel"/> class.
        /// </summary>
        public GetSynchronizedAccountModel()
        {
        }

        #endregion

        /// <summary>
        /// The MCSS account
        /// </summary>
        public AccountViewModel Account
        {
            get;
            set;
        }

        /// <summary>
        /// The MCSS account
        /// </summary>
        public Account LocalAccount
        {
            get;
            set;
        }

        /// <summary>
        /// The ldap user
        /// </summary>
        public User LdapUser
        {
            get;
            set;
        }

        /// <summary>
        /// If synchronization is available for this tenant
        /// </summary>
        public bool SynchronizationAvailable
        {
            get;
            set;
        }

        /// <summary>
        /// If the account is in sync
        /// </summary>
        public bool AccountSynchronized
        {
            get;
            set;
        }

        /// <summary>
        /// The number of errors
        /// </summary>
        public int SynchronizationErrorCount
        {
            get;
            set;
        }
    }
}