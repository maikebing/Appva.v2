// <copyright file="NoOpPushNotification.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Azure.Messaging
{
    #region Imports.

    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// No op push notification <see cref="IPushNotification"/> implementation.
    /// </summary>
    public sealed class NoOpPushNotification : IPushNotification
    {
        #region IPushNotification Members

        /// <inheritdoc />
        public string RegisterDevice(string pushId, IList<string> tags)
        {
            return null;
        }

        /// <inheritdoc />
        public string UpdateDevice(string registrationId, string pushId, IList<string> tags = null)
        {
            return null;
        }

        /// <inheritdoc />
        public string SendPush(IList<string> devices, string payload)
        {
            return null;
        }

        /// <inheritdoc />
        public Task<string> RegisterDeviceAsync(string pushId, IList<string> tags)
        {
            return Task.FromResult<string>(null);
        }

        /// <inheritdoc />
        public Task<string> UpdateDeviceAsync(string registrationId, string pushId, IList<string> tags = null)
        {
            return Task.FromResult<string>(null);
        }

        /// <inheritdoc />
        public Task<string> SendPushAsync(IList<string> devices, string payload)
        {
            return Task.FromResult<string>(null);
        }

        #endregion
    }
}
