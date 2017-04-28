// <copyright file="DeviceDetailsHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion

    public class DeviceDetailsHandler : RequestHandler<Identity<DeviceViewModel>, DeviceViewModel>
    {
        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceService service;

        public DeviceDetailsHandler(IDeviceService service)
        {
            this.service = service;
        }

        public override DeviceViewModel Handle(Identity<DeviceViewModel> message)
        {
            var device = this.service.Find(message.Id);
            return new DeviceViewModel
            {
                Description = device.Description,
                CreatedAt = device.CreatedAt
            };
        }
    }
}