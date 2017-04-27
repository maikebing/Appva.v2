// <copyright file="EditDeviceHandler.cs" company="Appva AB">
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

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EditDeviceHandler : RequestHandler<Identity<EditDeviceModel>, EditDeviceModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IDeviceService deviceService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDeviceHandler"/> class.
        /// </summary>
        /// <param name="deviceService">The <see cref="IDeviceService"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public EditDeviceHandler(IDeviceService deviceService, ITaxonomyService taxonomyService)
        {
            this.deviceService = deviceService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditDeviceModel Handle(Identity<EditDeviceModel> message)
        {
            var deviceModel = new EditDeviceModel();
            var organizationList = new List<SelectListItem>();
            var device = this.deviceService.Find(message.Id);
            var organizations = this.taxonomyService.List(Application.Common.TaxonomicSchema.Organization);
            deviceModel.TaxonId = device.Taxon != null ? device.Taxon.Id : Guid.Empty;

            foreach (var organization in organizations)
            {
                organizationList.Add(new SelectListItem()
                {
                    Text = organization.Name,
                    Value = organization.Id.ToString(),
                });
            }
            
            deviceModel.Id = device.Id;
            if (device.Taxon != null)
            {
                deviceModel.TaxonId = device.Taxon.Id;
            }
            deviceModel.Description = device.Description;
            deviceModel.Organizations = organizationList;
            return deviceModel;
        }

        #endregion
    }
}