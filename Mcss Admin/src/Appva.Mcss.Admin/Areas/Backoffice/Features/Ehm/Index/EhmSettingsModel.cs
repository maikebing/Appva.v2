// <copyright file="EhmSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Mock;
    using Appva.Mcss.Admin.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EhmSettingsModel 
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the tenant settings.
        /// </summary>
        /// <value>
        /// The tenant settings.
        /// </value>
        public TenantAttributes TenantSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the mocked parameters.
        /// </summary>
        /// <value>
        /// The mocked parameters.
        /// </value>
        public EhmMockedParameters MockedParameters
        {
            get;
            set;
        }

        #endregion
    }
}