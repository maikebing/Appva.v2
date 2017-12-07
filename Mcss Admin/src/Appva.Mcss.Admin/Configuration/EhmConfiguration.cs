// <copyright file="EhmConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Configuration;

    #endregion

    /// <summary>
    /// The Appva eHM-api configuration
    /// </summary>
    internal static class AppvaEhmConfiguration
    {
        /// <summary>
        /// The appva ehm-api server url
        /// </summary>
        internal static string ServerUrl
        {
            get 
            {
                return ConfigurationManager.AppSettings.Get("eHM.ServerUrl");
            }
        }

        /// <summary>
        /// Gets the shared prescriber code.
        /// </summary>
        internal static string SharedLegitimationCode
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("eHM.SharedLegitimationCode");
            }
        }
    }
}