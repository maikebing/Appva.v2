// <copyright file="PushNotification.cs" company="Appva AB">
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
    using System.Linq;
    using System.Threading.Tasks;
    using Logging;
    using Microsoft.ServiceBus.Notifications;

    #endregion

    /// <summary>
    /// Azure IOS push notification <see cref="IPushNotification"/> implementation.
    /// </summary>
    public sealed class PushNotification : IPushNotification
    {
        #region Varables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="PushNotification"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<PushNotification>();

        /// <summary>
        /// The Azure <see cref="NotificationHubClient"/>.
        /// </summary>
        private readonly NotificationHubClient hub;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotification"/> class.
        /// </summary>
        public PushNotification()
        {
            var hubConfiguration = ConfigurationManager.ConnectionStrings["azureNotificationHub"];
            this.hub = NotificationHubClient.CreateClientFromConnectionString(
                hubConfiguration.ConnectionString, 
                hubConfiguration.ProviderName);
        }

        #endregion

        #region IPushNotification Members.

        /// <inheritdoc />
        public string RegisterDevice(string pushId, IList<string> tags)
        {
            try
            {
                Log.Debug(DebugFormats.RegistrationExecute);
                var registration = this.hub.CreateAppleNativeRegistrationAsync(pushId, tags).Result;
                Log.DebugFormat(DebugFormats.RegistrationResponse, pushId, registration.RegistrationId, registration.Tags);
                return registration.RegistrationId;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(DebugFormats.RegistrationException, pushId), ex);
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<string> RegisterDeviceAsync(string pushId, IList<string> tags)
        {
            try
            {
                Log.Debug(DebugFormats.RegistrationExecute);
                var registration = await this.hub.CreateAppleNativeRegistrationAsync(pushId, tags);
                Log.DebugFormat(DebugFormats.RegistrationResponse, pushId, registration.RegistrationId, registration.Tags);
                return registration.RegistrationId;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(DebugFormats.RegistrationException, pushId), ex);
                return null;
            }
        }

        /// <inheritdoc />
        public bool UpdateDevice(string registrationId, string pushId, IList<string> tags = null)
        {
            try
            {
                Log.Debug(DebugFormats.UpdateExecute);
                var device = this.hub.GetRegistrationAsync<AppleRegistrationDescription>(registrationId).Result;
                if (! string.IsNullOrEmpty(pushId))
                {
                    device.DeviceToken = pushId;
                }
                var updatedDevice = this.hub.CreateOrUpdateRegistrationAsync(device).Result;
                Log.DebugFormat(DebugFormats.UpdateRegistrationResponse, pushId, updatedDevice.RegistrationId, updatedDevice.Tags);
                return true;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(DebugFormats.UpdateException, registrationId, pushId), ex);
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> UpdateDeviceAsync(string registrationId, string pushId, IList<string> tags = null)
        {
            try
            {
                Log.Debug(DebugFormats.UpdateExecute);
                var device = await this.hub.GetRegistrationAsync<AppleRegistrationDescription>(registrationId);
                if (! string.IsNullOrEmpty(pushId))
                {
                    device.DeviceToken = pushId;
                }
                var updatedDevice = await this.hub.CreateOrUpdateRegistrationAsync(device);
                Log.DebugFormat(DebugFormats.UpdateRegistrationResponse, pushId, updatedDevice.RegistrationId, updatedDevice.Tags);
                return true;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(DebugFormats.UpdateException, registrationId, pushId), ex);
                return false;
            }
        }

        /// <inheritdoc />
        public bool SendPush(List<string> devices, string payload)
        {
            try
            {
                Log.Debug(DebugFormats.PushExecute);
                var tags = devices.Select(x => string.Format("deviceId:{0}", x)).ToList();
                var outcome = this.hub.SendAppleNativeNotificationAsync(payload, tags).Result;
                Log.DebugFormat(DebugFormats.PushOutcomeResponse, outcome.Success, outcome.Failure, outcome.State, devices);
                return outcome.Success > 0;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(DebugFormats.PushException, devices, payload), ex);
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> SendPushAsync(List<string> devices, string payload)
        {
            try
            {
                Log.Debug(DebugFormats.PushExecute);
                var tags = devices.Select(x => string.Format("deviceId:{0}", x)).ToList();
                var outcome = await this.hub.SendAppleNativeNotificationAsync(payload, tags);
                Log.DebugFormat(DebugFormats.PushOutcomeResponse, outcome.Success, outcome.Failure, outcome.State, devices);
                return outcome.Success > 0;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(DebugFormats.PushException, devices, payload), ex);
                return false;
            }
        }

        #endregion
    }
}