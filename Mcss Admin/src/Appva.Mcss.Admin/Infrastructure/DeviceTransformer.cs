// <copyright file="DeviceTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using Admin.Models;
    using Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IDeviceTransformer
    {
        /// <summary>
        /// Transform to device view model
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        IList<DeviceViewModel> ToDeviceList(IList<DeviceModel> devices);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeviceTransformer : IDeviceTransformer
    {
        /// <inheritdoc />
        public IList<DeviceViewModel> ToDeviceList(IList<DeviceModel> devices)
        {
            return devices.Select(d => new DeviceViewModel
            {
                Id = d.Id,
                CreatedAt = d.CreatedAt,
                Description = d.Description,
                AppBundle = d.AppBundle,
                AppVersion = d.AppVersion,
                OS = d.OS,
                OSVersion = d.OSVersion,
                Hardware = d.Hardware,
                IsActive = d.IsActive,
            }).ToList();
        }
    }
}