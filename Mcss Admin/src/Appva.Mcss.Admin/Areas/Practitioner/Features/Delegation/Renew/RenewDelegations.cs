﻿// <copyright file="UpdateDelegation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RenewDelegations : Identity<RenewDelegationsModel>
    {
        #region Properties.

        /// <summary>
        /// The category id
        /// </summary>
        public Guid DelegationCategoryId
        {
            get;
            set;
        }

        #endregion
    }
}