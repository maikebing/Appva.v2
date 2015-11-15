// <copyright file="ApplicationEnvironment.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Environment
{
    #region Imports.

    using System;
    using System.Reflection;
    using Appva.Core.Utilities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IApplicationEnvironment
    {
        /// <summary>
        /// Returns the operational environment.
        /// </summary>
        OperationalEnvironment Environment
        {
            get;
        }

        /// <summary>
        /// Returns the operational environment.
        /// </summary>
        IApplicationOperationalEnvironment Is
        {
            get;
        }

        /// <summary>
        /// Returns the assembly information.
        /// </summary>
        IApplicationEnvironmentAssemblyInfo Info
        {
            get;
        }

        /// <summary>
        /// Returns the operating system information.
        /// </summary>
        IApplicationEnvironmentOs OperatingSystem
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IApplicationOperationalEnvironment
    {
        /// <summary>
        /// Returns whether or not the application is running in a production
        /// environment.
        /// </summary>
        bool Production
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the application is running in a demo
        /// environment.
        /// </summary>
        bool Demo
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the application is running in a staging
        /// environment.
        /// </summary>
        bool Staging
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the application is running in a development
        /// environment.
        /// </summary>
        bool Development
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IApplicationEnvironmentAssemblyInfo
    {
        /// <summary>
        /// Returns the assembly title information.
        /// </summary>
        string Title
        {
            get;
        }

        /// <summary>
        /// Returns the assembly description information.
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// Returns the company name information.
        /// </summary>
        string Company
        {
            get;
        }

        /// <summary>
        /// Returns the product name information.
        /// </summary>
        string Product
        {
            get;
        }

        /// <summary>
        /// Returns the copyright information.
        /// </summary>
        string Copyright
        {
            get;
        }

        /// <summary>
        /// Returns the trademark information.
        /// </summary>
        string Trademark
        {
            get;
        }

        /// <summary>
        /// Returns  the build configuration, such as retail or debug, for the assembly.
        /// </summary>
        string Configuration
        {
            get;
        }

        /// <summary>
        /// Returns the version for the application.
        /// </summary>
        string Version
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IApplicationEnvironmentOs
    {
        /// <summary>
        /// Returns the NetBIOS name of this local computer.
        /// </summary>
        string MachineName
        {
            get;
        }

        /// <summary>
        /// Returns the <see cref="OperatingSystem"/> version that contains the current platform
        /// identifier and version number.
        /// </summary>
        string Version
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ApplicationEnvironment : 
        IApplicationEnvironment, 
        IApplicationOperationalEnvironment, 
        IApplicationEnvironmentAssemblyInfo,
        IApplicationEnvironmentOs
    {
        #region Variables.

        /// <summary>
        /// The internal instance.
        /// </summary>
        private static volatile ApplicationEnvironment instance;
        
        /// <summary>
        /// The synchronization lock.
        /// </summary>
        private static object syncLock = new object();

        /// <summary>
        /// The assembly.
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// The <see cref="OperationalEnvironment"/>.
        /// </summary>
        private readonly OperationalEnvironment environment;

        /// <summary>
        /// Specifies a description for an assembly.
        /// </summary>
        private readonly string title;

        /// <summary>
        /// Provides a text description for an assembly.
        /// </summary>
        private readonly string description;

        /// <summary>
        /// Defines a company name custom attribute for an assembly manifest.
        /// </summary>
        private readonly string company;

        /// <summary>
        /// Defines a product name custom attribute for an assembly manifest.
        /// </summary>
        private readonly string product;

        /// <summary>
        /// Defines a copyright custom attribute for an assembly manifest.
        /// </summary>
        private readonly string copyright;

        /// <summary>
        /// Defines a trademark custom attribute for an assembly manifest.
        /// </summary>
        private readonly string trademark;

        /// <summary>
        /// Specifies the build configuration, such as retail or debug, for an assembly.
        /// </summary>
        private readonly string configuration;

        /// <summary>
        /// Specifies the version of the assembly being attributed.
        /// </summary>
        private readonly string version;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationEnvironment"/> class.
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <param name="environment">
        /// The optional <see cref="OperationalEnvironment"/>, defaults to unknown
        /// </param>
        private ApplicationEnvironment(Assembly assembly, OperationalEnvironment environment = OperationalEnvironment.Unknown)
        {
            this.assembly      = assembly;
            this.environment   = environment;
            this.title         = AttributeUtilities.Find<AssemblyTitleAttribute>(this.assembly, x => x.Title);
            this.description   = AttributeUtilities.Find<AssemblyDescriptionAttribute>(this.assembly, x => x.Description);
            this.company       = AttributeUtilities.Find<AssemblyCompanyAttribute>(this.assembly, x => x.Company);
            this.product       = AttributeUtilities.Find<AssemblyProductAttribute>(this.assembly, x => x.Product);
            this.copyright     = AttributeUtilities.Find<AssemblyCopyrightAttribute>(this.assembly, x => x.Copyright);
            this.trademark     = AttributeUtilities.Find<AssemblyTrademarkAttribute>(this.assembly, x => x.Trademark);
            this.configuration = AttributeUtilities.Find<AssemblyConfigurationAttribute>(this.assembly, x => x.Configuration);
            this.version       = this.assembly.GetName().Version.ToString();
        }

        #endregion

        #region Public Static Members.

        /// <summary>
        /// Returns the short hand operational environment.
        /// </summary>
        public static IApplicationOperationalEnvironment Is
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Returns the assembly information.
        /// </summary>
        public static IApplicationEnvironmentAssemblyInfo Info
        {
            get 
            {
                return instance;
            }
        }

        /// <summary>
        /// Returns the short hand operating system information.
        /// </summary>
        public static IApplicationEnvironmentOs OperatingSystem
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Returns the short hand environment information.
        /// </summary>
        public static string Environment
        {
            get
            {
                return instance.environment.AsUserFriendlyText();
            }
        }

        #endregion

        #region IApplicationEnvironment Members.

        /// <inheritdoc />
        OperationalEnvironment IApplicationEnvironment.Environment
        {
            get
            {
                return this.environment;
            }
        }

        /// <inheritdoc />
        IApplicationOperationalEnvironment IApplicationEnvironment.Is
        {
            get
            {
                return this;
            }
        }

        /// <inheritdoc />
        IApplicationEnvironmentAssemblyInfo IApplicationEnvironment.Info
        {
            get
            {
                return this;
            }
        }

        /// <inheritdoc />
        IApplicationEnvironmentOs IApplicationEnvironment.OperatingSystem
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region IApplicationOperationalEnvironment Members.

        /// <inheritdoc />
        bool IApplicationOperationalEnvironment.Production
        {
            get
            {
                return this.environment.IsProduction();
            }
        }

        /// <inheritdoc />
        bool IApplicationOperationalEnvironment.Demo
        {
            get
            {
                return this.environment.IsDemo();
            }
        }

        /// <inheritdoc />
        bool IApplicationOperationalEnvironment.Staging
        {
            get
            {
                return this.environment.IsStaging();
            }
        }

        /// <inheritdoc />
        bool IApplicationOperationalEnvironment.Development
        {
            get
            {
                return this.environment.IsDevelopment();
            }
        }

        #endregion

        #region IApplicationEnvironmentAssemblyInfo Members.

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Title
        {
            get
            {
                return this.title;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Description
        {
            get
            {
                return this.description;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Company
        {
            get
            {
                return this.company;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Product
        {
            get
            {
                return this.product;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Copyright
        {
            get
            {
                return this.copyright;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Trademark
        {
            get
            {
                return this.trademark;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Configuration
        {
            get
            {
                return this.configuration;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentAssemblyInfo.Version
        {
            get
            {
                return this.version;
            }
        }

        #endregion

        #region IApplicationEnvironmentOs Members.

        /// <inheritdoc />
        string IApplicationEnvironmentOs.MachineName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }

        /// <inheritdoc />
        string IApplicationEnvironmentOs.Version
        {
            get
            {
                return System.Environment.OSVersion.VersionString;
            }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="IApplicationEnvironment"/> class.
        /// </summary>
        /// <typeparam name="T">The assembly type</typeparam>
        /// <param name="environment">
        /// The optional <see cref="OperationalEnvironment"/>, defaults to unknown
        /// </param>
        /// <returns>A <see cref="IApplicationEnvironment"/> instance</returns>
        public static IApplicationEnvironment For<T>(OperationalEnvironment environment = OperationalEnvironment.Unknown)
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationEnvironment(typeof(T).Assembly, environment);
                    }
                }
            }
            return instance;
        }

        #endregion
    }
}