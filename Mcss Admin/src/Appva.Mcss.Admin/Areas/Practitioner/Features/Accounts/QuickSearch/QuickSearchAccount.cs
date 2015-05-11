// <copyright file="ListAccountCommand.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts.QuickSearch
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Domain.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class QuickSearchAccount : IRequest<IEnumerable<object>>
    {
        /// <summary>
        /// The search query.
        /// </summary>
        public string Term
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