// <copyright file="ApplicationEnvironmentExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Extensions
{
    #region Imports.

    using Appva.Core.Environment;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ApplicationEnvironmentExtensions
    {
        public static bool NoCertificateProduction(this ApplicationEnvironment environment)
        {
            if (ApplicationEnvironment.Is.Demo || ApplicationEnvironment.Is.Development || ApplicationEnvironment.Is.Production || ApplicationEnvironment.Is.Staging)
            {
                return false;
            }

            var config = ConfigurationManager.GetSection("mcss") as McssConfig;

            return config.Environment.Equals("NoCertificateProduction");
            
        }
    }

    /// <summary>
    /// Dummy class for MCSS config
    /// </summary>
    public class McssConfig
    {
        /// <summary>
        /// The environment
        /// </summary>
        public string Environment
        {
            get;
            set;
        }
    }
}