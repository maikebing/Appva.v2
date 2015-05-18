// <copyright file="Application.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System;
    using System.Reflection;
    using System.Web.Configuration;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// FIXME: Rename to ApplicationEnvironment and add IApplicationEnvironment, ApplicationHostContext.
    /// http://www.codeproject.com/Tips/355467/Getting-publishing-information-for-a-ClickOnce-dep
    /// </summary>
    public static class Application
    {
        #region Private Fields.

        /// <summary>
        /// Cached environment variable.
        /// </summary>
        private static readonly ApplicationConfiguration Configuration;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes static members of the <see cref="Application" /> class.
        /// </summary>
        static Application()
        {
            Configuration = (ApplicationConfiguration) WebConfigurationManager.GetSection("mcss");
        }

        #endregion

        #region Public Static Properties.

        /// <summary>
        /// Returns the full name of the application.
        /// </summary>
        public static string FullName
        {
            get
            {
                return AppDomain.CurrentDomain.ApplicationIdentity.FullName;
            }
        }

        /// <summary>
        /// Returns the version for the application.
        /// </summary>
        public static string Version
        {
            get
            {
                return Assembly.GetAssembly(typeof(Application)).GetName().Version.ToString();
            }
        }
        /// <summary>
        /// Returns the NetBIOS name of this local computer.
        /// </summary>
        public static string MachineName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }

        /// <summary>
        /// Returns the operational environment.
        /// </summary>
        public static OperationalEnvironment OperationalEnvironment
        {
            get
            {
                return Configuration.Environment;
            }
        }

        /// <summary>
        /// Returns the operational environment as user friendly text.
        /// </summary>
        public static string Environment
        {
            get
            {
                return Configuration.Environment.AsUserFriendlyText();
            }
        }

        /// <summary>
        /// Returns whether or not the application is running in a production
        /// environment.
        /// </summary>
        public static bool IsInProduction
        {
            get
            {
                return Configuration.Environment.Equals(OperationalEnvironment.Production);
            }
        }

        /// <summary>
        /// Returns whether or not the application is running in a demo
        /// environment.
        /// </summary>
        public static bool IsInDemo
        {
            get
            {
                return Configuration.Environment.Equals(OperationalEnvironment.Demo);
            }
        }

        /// <summary>
        /// Returns whether or not the application is running in a staging
        /// environment.
        /// </summary>
        public static bool IsInStaging
        {
            get
            {
                return Configuration.Environment.Equals(OperationalEnvironment.Staging);
            }
        }

        /// <summary>
        /// Returns whether or not the application is running in a development
        /// environment.
        /// </summary>
        public static bool IsInDevelopment
        {
            get
            {
                return Configuration.Environment.Equals(OperationalEnvironment.Development);
            }
        }

        #endregion
    }
}