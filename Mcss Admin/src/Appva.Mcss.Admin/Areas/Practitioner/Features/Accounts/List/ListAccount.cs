// <copyright file="ListAccountCommand.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListAccount : IRequest<ListAccountModel>
    {
        /// <summary>
        /// The search query.
        /// </summary>
        public string q
        {
            get;
            set;
        }

        /// <summary>
        /// The current paging number.
        /// </summary>
        public int? page
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
        public bool filterByCreatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// <c>Account.IsActive</c> to filter by. Defaults True
        /// </summary>
        public bool isActive
        {
            get;
            set;
        }

        /// <summary>
        /// <c>Account.IsPaused</c> to filter by. Defaults False
        /// </summary>
        public bool isPaused
        {
            get;
            set;
        }
    }
}