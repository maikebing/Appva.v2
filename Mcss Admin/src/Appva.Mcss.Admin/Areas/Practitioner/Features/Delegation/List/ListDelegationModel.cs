// <copyright file="ListDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListDelegationModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDelegationModel"/> class.
        /// </summary>
        public ListDelegationModel()
        {
        }

        #endregion

        public Guid AccountId { get; set; }

        public Web.ViewModels.AccountViewModel Account { get; set; }

        public Dictionary<string, IList<Domain.Entities.KnowledgeTest>> KnowledgeTestMap { get; set; }

        public Dictionary<string, IList<Domain.Entities.Delegation>> DelegationMap { get; set; }

        /// <summary>
        /// Whether or not the user account is hidden, e.g. has only
        /// roels which are not shown in any common lists.
        /// </summary>
        public bool IsAccountVisibilityHidden
        {
            get;
            set;
        }
    }
}