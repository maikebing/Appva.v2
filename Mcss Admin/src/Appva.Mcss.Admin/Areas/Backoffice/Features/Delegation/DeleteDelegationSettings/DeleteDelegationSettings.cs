// <copyright file="DeleteDelegationSettings.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
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
    public sealed class DeleteDelegationSettingsModel : IRequest<object>
    {
        #region Properties.

        /// <summary>
        /// If reason should be specified on delegation removal
        /// </summary>
        public bool SpecifyReasonIsActive 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The reasons
        /// </summary>
        public string Reasons 
        {
            get;
            set; 
        }

        #endregion
    }
}