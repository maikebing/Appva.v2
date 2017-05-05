// <copyright file="DeviceDetailsPublisher.cs" company="Appva AB">
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

    using System.Linq;
    using Application.Services;
    using Appva.Cqrs;
    using Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeviceDetailsPublisher : RequestHandler<DeviceDetailsModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceService deviceService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IDeviceAlertService"/>
        /// </summary>
        private readonly IDeviceAlertService alertService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceDetailsPublisher"/> class.
        /// </summary>
        /// <param name="deviceService">The <see cref="IDeviceService"/> implementation.</param>
        /// <param name="alertService">The <see cref="IDeviceAlertService"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public DeviceDetailsPublisher(IDeviceService deviceService, IDeviceAlertService alertService, ITaxonomyService taxonomyService)
        {
            this.deviceService = deviceService;
            this.alertService = alertService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        /// <inheritdoc />
        public override bool Handle(DeviceDetailsModel message)
        {
            var device = this.deviceService.Find(message.Id);
            var alert = this.alertService.Find(message.Id);
            var selected = message.DeviceLevelTaxons.Where(x => x.IsSelected).Select(x => x.Id).ToArray();

            if (selected.Length == 0 && message.HasAlert)
            {
                return false;
            }

            if (alert != null && message.HasAlert)
            {
                alert.Taxons = this.alertService.ListAllIn(selected);
                alert.EscalationLevel = this.alertService.GetEscalationLevel(message.EscalationLevelId);
                this.alertService.Update(alert);
            }
            else if (alert == null && message.HasAlert)
            {
                this.alertService.Save(new DeviceAlert
                {
                    Taxons = this.alertService.ListAllIn(selected),
                    EscalationLevel = this.alertService.GetEscalationLevel(message.EscalationLevelId),
                    Device = device,
                });
            }
            else if (alert != null && message.HasAlert == false)
            {
                this.alertService.Delete(alert);
            }

            return true;
        }
    }
}