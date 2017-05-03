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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System.Linq;

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

        private readonly IDeviceAlertService alertService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDevicePublisher"/> class.
        /// </summary>
        /// <param name="deviceService">The <see cref="IDeviceService"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public EditDevicePublisher(IDeviceService deviceService, ITaxonomyService taxonomyService, IDeviceAlertService alertService)
        {
            this.alertService = alertService;
            this.deviceService = deviceService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(EditDeviceModel message)
        {
            var device = this.deviceService.Find(message.Id);
            var alert = this.alertService.Find(message.Id);
            var selectedOrganizations = message.DeviceLevelTaxons.Where(x => x.IsSelected).Select(x => x.Id).ToArray();

            if (device == null)
            {
                return false;
            }

            var taxon = message.TaxonId == Guid.Empty ? null : this.taxonomyService.Get(message.TaxonId);
            device.Description = message.Description;
            device.Taxon = taxon;
            this.deviceService.Update(device);

            if (alert != null)
            {
                alert.Taxons = this.alertService.ListAllIn(selectedOrganizations);
                alert.EscalationLevel = this.alertService.GetEscalationLevel(message.EscalationLevelId);
                this.alertService.Update(alert);
            }

            return true;
        }

        #endregion
    }
}