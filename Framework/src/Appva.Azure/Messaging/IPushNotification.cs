// <copyright file="IPushNotification.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Azure
{
    #region Imports.

    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPushNotification
    {
        /// <summary>
        /// Register a new device in the notification hub and returns the 
        /// <c>Registration-id</c> in the hub.
        /// </summary>
        /// <param name="deviceToken">The device token</param>
        /// <param name="tags">List of tags</param>
        /// <returns>The Azure registration id</returns>
        string RegisterDevice(string deviceToken, IList<string> tags);

        /// <summary>
        /// Asynchronously register a new device in the notification hub and returns the 
        /// <c>Registration-id</c> in the hub.
        /// </summary>
        /// <param name="deviceToken">The device token</param>
        /// <param name="tags">List of tags</param>
        /// <returns>The Azure registration id</returns>
        Task<string> RegisterDeviceAsync(string deviceToken, IList<string> tags);

        /// <summary>
        /// Updates an already registered iOS-device.
        /// </summary>
        /// <param name="registrationId">The Azure registration id</param>
        /// <param name="deviceToken">The device token</param>
        /// <param name="tags">List of tags</param>
        /// <returns>The new Azure registration id</returns>
        string UpdateDevice(string registrationId, string deviceToken, IList<string> tags = null);

        /// <summary>
        /// Asynchronously updates an already registered iOS-device.
        /// </summary>
        /// <param name="registrationId">The Azure registration id</param>
        /// <param name="deviceToken">The device token</param>
        /// <param name="tags">List of tags</param>
        /// <returns>The new Azure registration id</returns>
        Task<string> UpdateDeviceAsync(string registrationId, string deviceToken, IList<string> tags = null);

        /// <summary>
        /// Sends notifications to given tags.
        /// </summary>
        /// <param name="devices">List of device id</param>
        /// <param name="payload">The push payload</param>
        /// <returns>The Azure tracking id</returns>
        string SendPush(IList<string> devices, string payload);

        /// <summary>
        /// Asynchronously sends notifications to given tags.
        /// </summary>
        /// <param name="devices">List of device id</param>
        /// <param name="payload">The push payload</param>
        /// <returns>The Azure tracking id</returns>
        Task<string> SendPushAsync(IList<string> devices, string payload);
    }
}
