// <copyright file="DeviceTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports

    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Models;

    #endregion

    /// <summary>
    /// Device transforming.
    /// </summary>
    public static class DeviceTransformer
    {
        /// <summary>
        /// Transforms a <see cref="Device"/> to a <see cref="DeviceModel"/>
        /// </summary>
        /// <param name="device">The <see cref="DeviceModel"/> to be transformed</param>
        /// <returns>The <see cref="Device"/></returns>
        public static Device ToDevice(DeviceModel device)
        {
            return new Device
            {
                Uuid = device.Uuid,
                PushUuid = device.RemoteMessagingId,
                Name = device.Name,
                Description = device.Description
            };
        }
    }
}