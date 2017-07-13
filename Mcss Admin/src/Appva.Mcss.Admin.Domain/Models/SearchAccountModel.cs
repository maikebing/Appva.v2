// <copyright file="SearchAccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SearchAccountModel
    {
        #region Properties.

        /// <summary>
        /// The current account id
        /// </summary>
        public Guid CurrentUserId
        {
            get;
            set;
        }

        /// <summary>
        /// The current user location (taxon) path.
        /// </summary>
        public string CurrentUserLocationPath
        {
            get;
            set;
        }

        /// <summary>
        /// The search-query
        /// </summary>
        public string SearchQuery
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by is active
        /// </summary>
        public bool IsFilterByIsActiveEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by is paused
        /// </summary>
        public bool IsFilterByIsPausedEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by delegation id
        /// </summary>
        public Guid? DelegationFilterId
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by role id
        /// </summary>
        public Guid? RoleFilterId
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by "created-by" current account
        /// </summary>
        public bool IsFilterByCreatedByEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Organization filter id
        /// </summary>
        public string OrganisationFilterTaxonPath 
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by synchronized account
        /// </summary>
        public bool? IsFilterByIsSynchronizedEnabled 
        { 
            get; 
            set;
        }

        #endregion
    }
}