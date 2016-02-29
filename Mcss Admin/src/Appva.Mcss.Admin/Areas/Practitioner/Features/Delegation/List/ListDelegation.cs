// <copyright file="ListDelegation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
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
    public sealed class ListDelegation : IRequest<ListDelegationModel>
    {
        #region Properties.

        /// <summary>
        /// The id of the current account
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        #endregion
    }
}