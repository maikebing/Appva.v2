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
    /// <example>
    /// The preferred usage is to use the push notification client as a singleton.
    /// <code language="cs" title="Not Preferred Example">
    ///     var notifications = PushNotification.CreateNew();
    ///     notifications.RegisterDevice("foo", new [] { "bar", "baz" });
    /// </code>
    /// <code language="cs" title="Preferred Example with IoC">
    ///     var builder = new ContainerBuilder();
    ///     builder.RegisterType{PushNotification}().As{IPushNotification}().SingleInstance();
    /// </code>
    /// </example>
    public sealed class PushNotification : IPushNotification
    {
        #region Varables.

        /// <summary>
        /// The Azure notification hub connection string key.
        /// </summary>
        private const string ConnectionStringKey = "Azure.Notifications.Hub";

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
            : this(ConfigurationManager.ConnectionStrings[ConnectionStringKey])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotification"/> class.
        /// </summary>
        /// <param name="hubConfiguration">The connection settings</param>
        private PushNotification(ConnectionStringSettings hubConfiguration)
            : this(hubConfiguration.ConnectionString, hubConfiguration.ProviderName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotification"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="providerName">The provider name</param>
        private PushNotification(string connectionString, string providerName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            Log.Debug(Debug.Messages.ConstructorInitialization);
            this.hub = NotificationHubClient.CreateClientFromConnectionString(
                connectionString,
                providerName);
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new push notification client.
        /// </summary>
        /// <returns>A new <see cref="IPushNotification"/> instance</returns>
        public static IPushNotification CreateNew()
        {
            return new PushNotification();
        }

        /// <summary>
        /// Creates a new push notification client.
        /// </summary>
        /// <param name="connectionString">The azure connection string</param>
        /// <param name="providerName">The azure provider name</param>
        /// <returns>A new <see cref="IPushNotification"/> instance</returns>
        public static IPushNotification CreateNew(string connectionString, string providerName)
        {
            return new PushNotification(connectionString, providerName);
        }

        #endregion

        #region IPushNotification Members.

        /// <inheritdoc />
        public string RegisterDevice(string deviceToken, IList<string> tags)
        {
            return this.RegisterDeviceAsync(deviceToken, tags).Result;
        }

        /// <inheritdoc />
        public bool UpdateDevice(string registrationId, string deviceToken, IList<string> tags = null)
        {
            return this.UpdateDeviceAsync(registrationId, deviceToken, tags).Result;
        }

        /// <inheritdoc />
        public bool SendPush(IList<string> devices, string payload)
        {
            return this.SendPushAsync(devices, payload).Result;
        }

        /// <inheritdoc />
        public async Task<string> RegisterDeviceAsync(string deviceToken, IList<string> tags)
        {
            try
            {
                Log.DebugFormat(Debug.Messages.RegistrationExecuting, deviceToken);
                var registration = await this.hub.CreateAppleNativeRegistrationAsync(deviceToken, tags);
                if (Log.IsDebugEnabled())
                {
                    Log.DebugFormat(Debug.Messages.RegistrationResponse, registration.Serialize());
                }
                return registration.RegistrationId;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(Debug.Messages.RegistrationException, deviceToken), ex);
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<bool> UpdateDeviceAsync(string registrationId, string deviceToken, IList<string> tags = null)
        {
            try
            {
                Log.DebugFormat(Debug.Messages.UpdateExecute, registrationId, deviceToken);
                var registration = await this.hub.GetRegistrationAsync<AppleRegistrationDescription>(registrationId);
                if (!string.IsNullOrEmpty(deviceToken))
                {
                    registration.DeviceToken = deviceToken;
                }
                var response = await this.hub.CreateOrUpdateRegistrationAsync(registration);
                if (Log.IsDebugEnabled())
                {
                    Log.DebugFormat(Debug.Messages.UpdateResponse, response.Serialize());
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.DebugException(string.Format(Debug.Messages.UpdateException, registrationId, deviceToken), ex);
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> SendPushAsync(IList<string> devices, string payload)
        {
            var tags = devices.Select(x => string.Format("deviceId:{0}", x)).ToList();
            try
            {
                if (Log.IsDebugEnabled())
                {
                    Log.DebugFormat(Debug.Messages.PushExecuting, string.Join(",", tags));
                }
                var response = await this.hub.SendAppleNativeNotificationAsync(payload, tags);
                Log.DebugFormat(Debug.Messages.PushResponse, response.Success, response.Failure, response.State);
                return response.Success > 0;
            }
            catch (Exception ex)
            {
                if (Log.IsDebugEnabled())
                {
                    Log.DebugException(string.Format(Debug.Messages.PushException, string.Join(",", tags), payload), ex);
                }
                return false;
            }
        }

        #endregion
    }
}
