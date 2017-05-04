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
    #region Imports.

    using Application.Services;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DeviceDetailsHandler : RequestHandler<Identity<DeviceDetailsModel>, DeviceDetailsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceService deviceService;

        /// <summary>
        /// The <see cref="IDeviceAlertService"/>
        /// </summary>
        private readonly IDeviceAlertService alertService;

        /// <summary> 
        /// Initializes a new instance of the <see cref="DeviceDetailsHandler"/> class. 
        /// </summary> 
        /// <param name="deviceService">The <see cref="IDeviceService"/> implementation.</param> 
        /// <param name="alertService">The <see cref="IDeviceAlertService"/> implementation.</param>
        public DeviceDetailsHandler(IDeviceService deviceService, IDeviceAlertService alertService)
        {
            this.alertService = alertService;
            this.deviceService = deviceService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override DeviceDetailsModel Handle(Identity<DeviceDetailsModel> message)
        {
            var device = this.deviceService.Find(message.Id);
            var alert = this.alertService.Find(message.Id);

            return new DeviceDetailsModel
            {
                Device = device,
                HasAlert = alert == null ? false : true
            };
        }

        #endregion
    }
}