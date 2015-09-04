// <copyright file="IApplicationOperationalEnvironment.cs" company="Appva AB">
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
}