// <copyright file="IApplicationEnvironment.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Environment
{
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
}