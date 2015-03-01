// <copyright file="IApplicationConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Infrastructure.Configuration
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Core.Configuration;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IApplicationConfiguration : IConfigurableResource
    {
        /// <summary>
        /// The service name.
        /// </summary>
        string ServiceName
        {
            get;
        }

        /// <summary>
        /// The friendly display name.
        /// </summary>
        string ServiceDisplayName
        {
            get;
        }

        /// <summary>
        /// The service description.
        /// </summary>
        string ServiceDescription
        {
            get;
        }

        /// <summary>
        /// Whether or not the service is running in production
        /// environment.
        /// </summary>
        bool IsProduction
        {
            get;
        }

        /// <summary>
        /// The sender for all notifications.
        /// </summary>
        string NotificationSender
        {
            get;
        }

        /// <summary>
        /// The recipients for all notifications.
        /// </summary>
        IEnumerable<string> NotificationRecipients
        {
            get;
        }

        /// <summary>
        /// Whether or not to send a notification
        /// on service start event.
        /// </summary>
        bool SendNotificationOnStart
        {
            get;
        }

        /// <summary>
        /// Whether or not to send a notification
        /// on service stop event.
        /// </summary>
        bool SendNotificationOnStop
        {
            get;
        }

        /// <summary>
        /// Whether or not to send a notification
        /// on service pause event.
        /// </summary>
        bool SendNotificationOnPause
        {
            get;
        }

        /// <summary>
        /// Whether or not to send a notification
        /// on service resume event.
        /// </summary>
        bool SendNotificationOnResume
        {
            get;
        }

        /// <summary>
        /// Whether or not to send a notification
        /// on service shutdown event.
        /// </summary>
        bool SendNotificationOnShutdown
        {
            get;
        }

        /// <summary>
        /// Whether or not to send a notification
        /// on service exception.
        /// </summary>
        bool SendNotificationOnException
        {
            get;
        }
    }
}