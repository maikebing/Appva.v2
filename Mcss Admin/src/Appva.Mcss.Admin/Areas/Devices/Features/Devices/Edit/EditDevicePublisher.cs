// <copyright file="EditDevicePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.Edit
{
    #region Imports.

    using System;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EditDevicePublisher : RequestHandler<EditDeviceModel, bool>
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
        /// Initializes a new instance of the <see cref="EditDevicePublisher"/> class.
        /// </summary>
        /// <param name="deviceService">The <see cref="IDeviceService"/> implementation.</param>
        /// <param name="alertService">The <see cref="IDeviceAlertService"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public EditDevicePublisher(IDeviceService deviceService, IDeviceAlertService alertService, ITaxonomyService taxonomyService)
        {
            this.deviceService = deviceService;
            this.alertService = alertService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(EditDeviceModel message)
        {
            var device = this.deviceService.Find(message.Id);

            if (device == null)
            {
                return false;
            }

            var taxon = message.TaxonId == Guid.Empty ? null : this.taxonomyService.Get(message.TaxonId);

            device.Description = message.Description;
            device.Taxon = taxon;
            device.AuthenticationMethod = message.AuthenticationMethod;
            this.deviceService.Update(device);

            return true;
        }

        #endregion
    }
}