// <copyright file="ListAccountCommand.cs" company="Appva AB">
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
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListAccountCommand : IRequest<ListAccountModel>
    {
        /// <summary>
        /// The search query.
        /// </summary>
        public string SearchQuery
        {
            get;
            set;
        }

        /// <summary>
        /// The current paging number.
        /// </summary>
        public int? CurrentPageNumber
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
    }
}