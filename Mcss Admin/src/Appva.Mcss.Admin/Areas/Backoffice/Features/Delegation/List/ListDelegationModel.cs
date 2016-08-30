// <copyright file="ListDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        #endregion

        
    }
}