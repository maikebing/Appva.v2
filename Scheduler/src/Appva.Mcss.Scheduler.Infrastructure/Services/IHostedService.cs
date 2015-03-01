// <copyright file="IHostedService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Infrastructure.Services
{
    #region Imports.

    using Topshelf;

    #endregion

    /// <summary>
    /// Interface for hosted service.
    /// </summary>
    public interface IHostedService : ServiceControl
    {
        /// <summary>
        /// Starts the hosted service instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the hosted service instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Pauses the hosted service.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the hosted service.
        /// </summary>
        void Continue();

        /// <summary>
        /// Shut downs the the hosted service.
        /// </summary>
        void Shutdown();
    }
}