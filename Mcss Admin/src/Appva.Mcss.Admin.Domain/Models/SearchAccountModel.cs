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
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountModel"/> class.
        /// </summary>
        public SearchAccountModel()
        {
        }

        #endregion

        public Guid CurrentUserId
        {
            get;
            set;
        }
        public string SearchQuery
        {
            get;
            set;
        }
        public bool IsFilterByIsActiveEnabled
        {
            get;
            set;
        }
        public bool IsFilterByIsPausedEnabled
        {
            get;
            set;
        }
        public Guid? DelegationFilterId
        {
            get;
            set;
        }
        public Guid? RoleFilterId
        {
            get;
            set;
        }
        public bool IsFilterByCreatedByEnabled
        {
            get;
            set;
        }

        public Guid? OrganisationFilterId 
        {
            get;
            set;
        }

        public int? DaysLeft
        { 
            get; 
            set;
        }
    }
}