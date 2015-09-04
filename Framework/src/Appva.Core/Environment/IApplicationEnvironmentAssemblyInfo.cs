// <copyright file="IApplicationEnvironmentAssemblyInfo.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Environment
{
    #region Imports.

    using System;

    #endregion

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
}