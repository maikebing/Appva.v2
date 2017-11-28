// <copyright file="ListAccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Repository;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListAccountModel
    {
        /// <summary>
        /// TODO: Remove and when add locations to principal
        /// Temp current user to be able to check location
        /// </summary>
        public Account CurrentUser
        {
            get;
            set;
        }
        /// <summary>
        /// The roles filters.
        /// </summary>
        public IList<SelectListItem> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation filters.
        /// </summary>
        public IList<SelectListItem> Delegations
        {
            get;
            set;
        }

        /// <summary>
        /// The Accounts
        /// </summary>
        public IPaged<AccountModel> Accounts
        {
            get;
            set;
        }
       
        /// <summary>
        /// Optional <c>Delegation</c> id to filter by.
        /// </summary>
        public Guid? DelegationFilterId
        {
            get;
            set;
        }

        /// <summary>
        /// Optional <c>Role</c> id to filter by.
        /// </summary>
        public Guid? RoleFilterId
        {
            get;
            set;
        }

        /// <summary>
        /// Optional <c>Account.CreatedBy</c> to filter by.
        /// </summary>
        public bool? IsFilterByCreatedByEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Optional <c>Account.IsActive</c> to filter by.
        /// </summary>
        public bool? IsFilterByIsActiveEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Optional <c>Account.IsPaused</c> to filter by.
        /// </summary>
        public bool? IsFilterByIsPausedEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Optional <c>Account.IsSynchronized</c> to filter by.
        /// </summary>
        public bool? IsFilterByIsSynchronizedEnabled
        {
            get;
            set;
        }
    }
}