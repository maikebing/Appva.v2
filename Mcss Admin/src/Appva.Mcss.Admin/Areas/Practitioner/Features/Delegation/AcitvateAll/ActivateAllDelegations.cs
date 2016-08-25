// <copyright file="ActivateAllDelegations.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ActivateAllDelegations : IRequest<ListDelegation>
    {
        #region Properties.

        /// <summary>
        /// The account id
        /// </summary>
        public Guid AccountId
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation category
        /// </summary>
        public Guid DelegationCategoryId
        {
            get;
            set;
        }

        #endregion
    }
}