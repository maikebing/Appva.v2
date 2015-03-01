// <copyright file="ApplicationConfiguration.cs" company="Appva AB">
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
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ApplicationConfiguration : IApplicationConfiguration
    {
        /// <inheridoc />
        public string ServiceName
        {
            get;
            set;
        }

        /// <inheridoc />
        public string ServiceDisplayName
        {
            get;
            set;
        }

        /// <inheridoc />
        public string ServiceDescription
        {
            get;
            set;
        }

        /// <inheridoc />
        public bool IsProduction
        {
            get;
            set;
        }

        /// <inheridoc />
        public string NotificationSender
        {
            get;
            set;
        }

        /// <inheridoc />
        public IEnumerable<string> NotificationRecipients
        {
            get;
            set;
        }

        /// <inheridoc />
        public bool SendNotificationOnStart
        {
            get;
            set;
        }

        /// <inheridoc />
        public bool SendNotificationOnStop
        {
            get;
            set;
        }

        /// <inheridoc />
        public bool SendNotificationOnPause
        {
            get;
            set;
        }

        /// <inheridoc />
        public bool SendNotificationOnResume
        {
            get;
            set;
        }

        /// <inheridoc />
        public bool SendNotificationOnShutdown
        {
            get;
            set;
        }

        /// <inheridoc />
        public bool SendNotificationOnException
        {
            get;
            set;
        }

    }
}