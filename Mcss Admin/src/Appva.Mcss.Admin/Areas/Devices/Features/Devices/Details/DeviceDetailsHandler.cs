﻿// <copyright file="DeviceDetailsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using Application.Services;
    #endregion

    public class DeviceDetailsHandler : RequestHandler<Identity<DeviceViewModel>, DeviceViewModel>
    {
        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IDeviceService deviceService;

        public DeviceDetailsHandler(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        public override DeviceViewModel Handle(Identity<DeviceViewModel> message)
        {
            var device = this.deviceService.Find(message.Id);

            return new DeviceViewModel
            {
                Id = device.Id,
                OS = device.OS,
                OSVersion = device.OSVersion,
                AppBundle = device.AppBundle,
                CreatedAt = device.CreatedAt,
                Hardware = device.Hardware,
                UDID = device.UDID,
                Version = device.Version,
                LastPingedDate = device.LastPingedDate,
                Description = device.Description,
                AppVersion = device.AppVersion,
                IsActive = device.IsActive,
            };


        }
    }
}