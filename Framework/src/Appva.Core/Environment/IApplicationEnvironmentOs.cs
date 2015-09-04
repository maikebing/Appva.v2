// <copyright file="IApplicationEnvironmentOs.cs" company="Appva AB">
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
}