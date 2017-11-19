// <copyright file="TenaIdentifiConfiguration.cs" company="Appva AB">
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
    /// The TENA Identifi configuration
    /// </summary>
    internal static class TenaIdentifiConfiguration
    {
        /// <summary>
        /// Returns the TENA Identifi server url.
        /// </summary>
        internal static string ServerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("TenaIdentifi.ServerUrl");
            }
        }
    }
}