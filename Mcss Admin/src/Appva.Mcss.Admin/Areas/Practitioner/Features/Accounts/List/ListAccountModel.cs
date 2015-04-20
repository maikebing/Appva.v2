// <copyright file="ListAccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts.List
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListAccountModel
    {
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

        public IList<Account> Accounts
        {
            get;
            set;
        }
        /*public SearchViewModel<AccountViewModel> Search
        {
            get;
            set;
        }*/

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
        public bool IsFilterByCreatedByEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Optional <c>Account.IsActive</c> to filter by.
        /// </summary>
        public bool IsFilterByIsActiveEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Optional <c>Account.IsPaused</c> to filter by.
        /// </summary>
        public bool IsFilterByIsPausedEnabled
        {
            get;
            set;
        }

        public IEnumerable<TestBoll> Tests
        {
            get;
            set;
        }
    }
}