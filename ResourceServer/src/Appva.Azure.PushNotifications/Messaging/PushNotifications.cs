// <copyright file="PushNotifications.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Azure.PushNotifications.Messaging
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Configuration;
    using Microsoft.ServiceBus.Notifications;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class PushNotifications
    {
        #region Static fields

        /// <summary>
        /// An instance of <see cref="PushNotifications"/>
        /// </summary>
        public static PushNotifications Instance = new PushNotifications();

        private static NotificationHubClient hub { get; set; }

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotifications"/> class.
        /// </summary>
        public PushNotifications()
        {
            ConnectionStringSettings connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["azureNotificationHub"];
            hub = NotificationHubClient.CreateClientFromConnectionString(connectionString.ConnectionString, connectionString.ProviderName);
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Register a new device in the notification hub and returns the Registration-id in the hub
        /// </summary>
        /// <param name="pushId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static string RegisterDevice(string pushId, IList<string> tags)
        {
            RegistrationDescription reg;
            try {
                var task = hub.CreateAppleNativeRegistrationAsync(pushId, tags);
                reg = task.Result;
            }
            catch (ArgumentException)
            {
                return null;   
            }

            return reg.RegistrationId;
        }

        /// <summary>
        /// Updates an already registered iOS-device
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="pushId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static bool UpdateDevice(string regId, string pushId, IList<string> tags = null)
        {
            AppleRegistrationDescription device = hub.GetRegistrationAsync<AppleRegistrationDescription>(regId).Result;

            if (pushId != null && pushId != "")
            {
                device.DeviceToken = pushId;
            }

            try
            {
                var reg = hub.CreateOrUpdateRegistrationAsync(device).Result;
            } 
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}