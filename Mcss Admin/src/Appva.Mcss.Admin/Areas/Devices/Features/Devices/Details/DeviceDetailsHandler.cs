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
    using Application.Models;
    #region Imports.

    using Application.Services;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using Domain.Entities;
    using Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

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
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor
        /// <summary> 
        /// Initializes a new instance of the <see cref="DeviceDetailsHandler"/> class. 
        /// </summary> 
        /// <param name="deviceService">The <see cref="IDeviceService"/> implementation.</param> 
        /// <param name="alertService">The <see cref="IDeviceAlertService"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public DeviceDetailsHandler(IDeviceService deviceService, IDeviceAlertService alertService, ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
            this.alertService = alertService;
            this.deviceService = deviceService;
        }
        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override DeviceDetailsModel Handle(Identity<DeviceDetailsModel> message)
        {
            var organizationList = new List<SelectListItem>();
            var organizations = this.taxonomyService.List(Application.Common.TaxonomicSchema.Organization);
            var device = this.deviceService.Find(message.Id);
            var deviceAlert = this.alertService.Find(message.Id);

            foreach (var organization in organizations)
            {
                organizationList.Add(new SelectListItem()
                {
                    Text = string.Format("{0}: {1}", organization.Type, organization.Name),
                    Value = organization.Id.ToString(),
                });
            }

            return new DeviceDetailsModel
            {
                Device = device,
                HasAlert = deviceAlert == null ? false : true,
                EscalationLevels = this.alertService.GetEscalationLevels(),
                EscalationLevelId = this.alertService.Find(device.Id) == null ? Guid.Empty : this.alertService.Find(device.Id).EscalationLevel.Id,
                DeviceLevelTaxons = this.Merge(organizations, deviceAlert == null ? null : deviceAlert.Taxons)
            };
        }
        #endregion

        #region Methods.

        /// <inheritdoc />
        private IList<Tickable> Merge(IList<ITaxon> items, IList<Taxon> selected)
        {
            var organizations = items.Select(x => new Tickable
            {
                Id = x.Id,
                Label = string.IsNullOrEmpty(x.Description) ? x.Name : string.Format("{0}: {1}", x.Description, x.Name)
            }).ToList();

            if (selected != null)
            {
                var selections = selected.Select(x => x.Id).ToList();

                foreach (var organization in organizations)
                {
                    if (selections.Contains(organization.Id))
                    {
                        organization.IsSelected = true;
                    }
                }
            }

            return organizations;
        }

        #endregion

    }
}