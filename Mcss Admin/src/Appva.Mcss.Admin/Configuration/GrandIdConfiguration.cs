// <copyright file="GrandIdConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Configuration;

    #endregion

    /// <summary>
    /// The grand id configuration.
    /// </summary>
    /// <remarks>
    /// See external file GrandId.Tests.Integration.config.
    /// </remarks>
    internal static class GrandIdConfiguration
    {
        /// <summary>
        /// Returns the grand id server url.
        /// </summary>
        public static string ServerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("GrandId.ServerUrl");
            }
        }

        /// <summary>
        /// Returns the grand id api key.
        /// </summary>
        public static string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("GrandId.ApiKey");
            }
        }

        /// <summary>
        /// Returns the grand id authentication service key.
        /// </summary>
        public static string AuthenticationServiceKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("GrandId.AuthenticationServiceKey");
            }
        }

        /// <summary>
        /// Returns the grand id mobile api key.
        /// </summary>
        public static string MobileApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("GrandId.Mobile.ApiKey");
            }
        }

        /// <summary>
        /// Returns the grand id mobile authentication service key.
        /// </summary>
        public static string MobileAuthenticationServiceKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("GrandId.Mobile.AuthenticationServiceKey");
            }
        }
    }
}