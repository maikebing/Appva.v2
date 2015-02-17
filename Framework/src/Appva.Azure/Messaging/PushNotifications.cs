// <copyright file="PushNotifications.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Azure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Microsoft.ServiceBus.Notifications;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class PushNotifications
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotifications"/> class.
        /// </summary>
        public PushNotifications()
        {
            ConnectionStringSettings connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["azureNotificationHub"];
            Hub = NotificationHubClient.CreateClientFromConnectionString(connectionString.ConnectionString, connectionString.ProviderName);
        }

        #endregion

        #region Private Static Properties.

        /// <summary>
        /// The azure notificationhub
        /// </summary>
        private static NotificationHubClient Hub
        {
            get;
            set;
        }

        #endregion

        #region Public functions.

        /// <summary>
        /// Register a new device in the notification hub and returns the Registration-id in the hub
        /// </summary>
        /// <param name="pushId">The Pushid</param>
        /// <param name="tags">List of tags</param>
        /// <returns>The Azure registration id</returns>
        public static string RegisterDevice(string pushId, IList<string> tags)
        {
            RegistrationDescription reg;
            try 
            {
                var task = Hub.CreateAppleNativeRegistrationAsync(pushId, tags);
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
        /// <param name="regId">The Azure registration id</param>
        /// <param name="pushId">The Pushid</param>
        /// <param name="tags">List of tags</param>
        /// <returns>If success, true</returns>
        public static bool UpdateDevice(string regId, string pushId, IList<string> tags = null)
        {
            AppleRegistrationDescription device = Hub.GetRegistrationAsync<AppleRegistrationDescription>(regId).Result;

            if (pushId != null && pushId != string.Empty)
            {
                device.DeviceToken = pushId;
            }

            try
            {
                var reg = Hub.CreateOrUpdateRegistrationAsync(device).Result;
            } 
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sends notifications to given tags
        /// </summary>
        /// <param name="devices">List of device id</param>
        /// <param name="payload">The push payload</param>
        /// <returns>If success, true</returns>
        public static bool SendPush(List<string> devices, string payload)
        {
            List<string> tags = new List<string>();
            foreach (var d in devices)
            {
                tags.Add(string.Format("deviceId:{0}", d));
            }
            NotificationOutcome result = null;
            try
            {
                var send = Hub.SendAppleNativeNotificationAsync(payload, tags);
                result = send.Result;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}