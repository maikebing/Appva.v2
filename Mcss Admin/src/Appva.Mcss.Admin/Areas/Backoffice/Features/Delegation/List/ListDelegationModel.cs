// <copyright file="ListDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Models;
    using static Application.Common.Permissions;
    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListDelegationModel
    {
        #region Properties.

        /// <summary>
        /// The delegations
        /// </summary>
        public Dictionary<ITaxon, IList<ITaxon>> Delegations 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The currently active delegations
        /// </summary>
        public IList<Domain.Entities.Delegation> ActiveDelegations { get; set; }

        /// <summary>
        /// Taxon filtered delegations
        /// </summary>
        public IList<Domain.Entities.Delegation> FilteredDelegations { get; set; }
        #endregion
    }
}