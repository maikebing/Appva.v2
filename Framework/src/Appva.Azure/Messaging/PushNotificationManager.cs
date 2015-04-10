// <copyright file="PushNotificationManager.cs" company="Appva AB">
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
    using System.Threading.Tasks;
    using Appva.Logging;
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
    ///     builder.RegisterType{PushNotificationManager}().As{IPushNotification}().SingleInstance();
    /// </code>
    /// </example>
    public sealed class PushNotificationManager : IPushNotification
    {
        #region Variables.

        /// <summary>
        /// The Azure notification hub connection string key.
        /// </summary>
        private const string ConnectionStringKey = "Azure.Notifications.Hub";

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="PushNotification"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<PushNotificationManager>();

        /// <summary>
        /// The Azure <see cref="NotificationHubClient"/>.
        /// </summary>
        private readonly NotificationHubClient hub;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationManager"/> class.
        /// </summary>
        public PushNotificationManager()
            : this(ConfigurationManager.ConnectionStrings[ConnectionStringKey])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationManager"/> class.
        /// </summary>
        /// <param name="connectionSettings">The <see cref="ConnectionStringSettings"/></param>
        public PushNotificationManager(ConnectionStringSettings connectionSettings)
            : this(connectionSettings.ConnectionString, connectionSettings.ProviderName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionstring</param>
        /// <param name="hubPath">The hub path</param>
        public PushNotificationManager(string connectionString, string hubPath)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrWhiteSpace(hubPath))
            {
                throw new ArgumentNullException("hubPath");
            }
            Log.Debug(Debug.Messages.ConstructorInitialization);
            this.hub = NotificationHubClient.CreateClientFromConnectionString(
                connectionString,
                hubPath);
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new push notification client.
        /// </summary>
        /// <returns>A new <see cref="IPushNotification"/> instance</returns>
        public static IPushNotification CreateNew()
        {
            return new PushNotificationManager();
        }

        /// <summary>
        /// Creates a new push notification client.
        /// </summary>
        /// <param name="connectionString">The azure connection string</param>
        /// <param name="providerName">The azure provider name</param>
        /// <returns>A new <see cref="IPushNotification"/> instance</returns>
        public static IPushNotification CreateNew(string connectionString, string providerName)
        {
            return new PushNotificationManager(connectionString, providerName);
        }

        #endregion

        #region IPushNotification Members.

        /// <inheritdoc />
        public async Task<string> RegisterDeviceAsync(string pushId, IList<string> tags)
        {
            RegistrationDescription task;
            try
            {
                task = await this.hub.CreateAppleNativeRegistrationAsync(pushId, tags);
            }
            catch (ArgumentException e)
            {
                Log.Error(string.Format("Failed to register device with pushId {0}. Exception: {1}", pushId, e));
                return null;
            }

            return task.RegistrationId;
        }

        /// <inheritdoc />
        public string RegisterDevice(string pushId, IList<string> tags)
        {
            return this.RegisterDeviceAsync(pushId, tags).Result; 
        }

        /// <inheritdoc />
        public async Task<string> UpdateDeviceAsync(string regId, string pushId, IList<string> tags = null)
        {
            AppleRegistrationDescription device = await this.hub.GetRegistrationAsync<AppleRegistrationDescription>(regId);

            if (pushId != null && pushId != string.Empty)
            {
                device.DeviceToken = pushId;
            }
            try
            {
                device = await this.hub.CreateOrUpdateRegistrationAsync(device);
            }
            catch (ArgumentException e)
            {
                Log.Error(string.Format("Failed to update device {0} with pushId {1}. Exception: {2}", regId, pushId, e));    
                return null;
            }

            return device.RegistrationId;
        }

        /// <inheritdoc />
        public string UpdateDevice(string regId, string pushId, IList<string> tags = null)
        {
            return this.UpdateDeviceAsync(regId, pushId, tags).Result;
        }

        /// <inheritdoc />
        public async Task<string> SendPushAsync(IList<string> devices, string payload)
        {
            List<string> tags = new List<string>();
            foreach (var d in devices)
            {
                tags.Add(string.Format("deviceId:{0}", d));
            }
            NotificationOutcome result = null;
            try
            {
                result = await this.hub.SendAppleNativeNotificationAsync(payload, tags);
            }
            catch (Exception e)
            {
                Log.Error(string.Format("Failed to send pushnotification ({0}) to tags {1}. Exception {2}", payload, string.Join(", ", tags), e));
                return null;
            }
            return result.TrackingId;
        }

        /// <inheritdoc />
        public string SendPush(IList<string> devices, string payload)
        {
            return this.SendPushAsync(devices, payload).Result;
        }

        #endregion
    }
}