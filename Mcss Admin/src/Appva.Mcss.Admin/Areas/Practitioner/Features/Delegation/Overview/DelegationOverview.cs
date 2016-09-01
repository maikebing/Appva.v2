// <copyright file="DelegationOverview.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationOverview : IRequest<DelegationOverviewModel>
    {
        #region Properties.

        /// <summary>
        /// If the overivew should be filtered by issuer
        /// </summary>
        public bool? FilterByIssuer
        {
            get;
            set;
        }

        #endregion
    }
}