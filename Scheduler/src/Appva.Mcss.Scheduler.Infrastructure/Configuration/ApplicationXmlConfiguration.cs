// <copyright file="ApplicationXmlConfiguration.cs" company="Appva AB">
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
    using System.Configuration;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ApplicationXmlConfiguration : ConfigurationSection, IApplicationConfiguration
    {
        #region Public static Functions.

        /// <summary>
        /// Reads the XML configuration.
        /// </summary>
        /// <returns>A new <see cref="IApplicationConfiguration"/> instance</returns>
        public static IApplicationConfiguration Build()
        {
            return (ApplicationXmlConfiguration) ConfigurationManager.GetSection("mcss-scheduler");
        }

        #endregion

        #region IApplicationConfiguration Members.

        /// <inheridoc />
        [ConfigurationProperty("service-name", IsRequired = true)]
        public string ServiceName
        {
            get
            {
                return this["service-name"] as string;
            }
            set
            {
                this["service-name"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("service-display-name", IsRequired = true)]
        public string ServiceDisplayName
        {
            get
            {
                return this["service-display-name"] as string;
            }
            set
            {
                this["service-display-name"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("service-description", IsRequired = true)]
        public string ServiceDescription
        {
            get
            {
                return this["service-description"] as string;
            }
            set
            {
                this["service-description"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("is-production", DefaultValue = false)]
        public bool IsProduction
        {
            get
            {
                return (bool) this["is-production"];
            }
            set
            {
                this["is-production"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("e-mail-sender", IsRequired = true)]
        public string NotificationSender
        {
            get
            {
                return this["e-mail-sender"] as string;
            }
            set
            {
                this["e-mail-sender"] = value;
            }
        }

        /// <inheridoc />
        public IEnumerable<string> NotificationRecipients
        {
            get;
            set;
        }

        [ConfigurationProperty("e-mail-recipients", IsRequired = true)]
        public string Recipients
        {
            get
            {
                return this["e-mail-recipients"] as string;
            }
            set
            {
                this["e-mail-recipients"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("send-notification-on-start", DefaultValue = false)]
        public bool SendNotificationOnStart
        {
            get
            {
                return (bool)this["send-notification-on-start"];
            }
            set
            {
                this["send-notification-on-start"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("send-notification-on-stop", DefaultValue = false)]
        public bool SendNotificationOnStop
        {
            get
            {
                return (bool)this["send-notification-on-stop"];
            }
            set
            {
                this["send-notification-on-stop"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("send-notification-on-pause", DefaultValue = false)]
        public bool SendNotificationOnPause
        {
            get
            {
                return (bool)this["send-notification-on-pause"];
            }
            set
            {
                this["send-notification-on-pause"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("send-notification-on-resume", DefaultValue = false)]
        public bool SendNotificationOnResume
        {
            get
            {
                return (bool)this["send-notification-on-resume"];
            }
            set
            {
                this["send-notification-on-resume"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("send-notification-on-shutdown", DefaultValue = false)]
        public bool SendNotificationOnShutdown
        {
            get
            {
                return (bool)this["send-notification-on-shutdown"];
            }
            set
            {
                this["send-notification-on-shutdown"] = value;
            }
        }

        /// <inheridoc />
        [ConfigurationProperty("send-notification-on-exception", DefaultValue = false)]
        public bool SendNotificationOnException
        {
            get
            {
                return (bool)this["send-notification-on-exception"];
            }
            set
            {
                this["send-notification-on-exception"] = value;
            }
        }

        #endregion
    }
}