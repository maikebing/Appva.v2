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

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;

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
        /// The <see cref="IDeviceAlertService"/>
        /// </summary>
        private readonly IDeviceAlertService alertService;

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
        /// <param name="alertService">The <see cref="IDeviceAlertService"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public EditDeviceHandler(IDeviceService deviceService, IDeviceAlertService alertService, ITaxonomyService taxonomyService)
        {
            this.deviceService = deviceService;
            this.alertService = alertService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditDeviceModel Handle(Identity<EditDeviceModel> message)
        {
            var organizationList = new List<SelectListItem>();
            var device = this.deviceService.Find(message.Id);
            var organizations = this.taxonomyService.List(Application.Common.TaxonomicSchema.Organization);

            foreach (var organization in organizations)
            {
                organizationList.Add(new SelectListItem()
                {
                    Text = string.Format("{0}: {1}", organization.Type, organization.Name),
                    Value = organization.Id.ToString(),
                });
            }

            return new EditDeviceModel
            {
                Id = device.Id,
                TaxonId = device.Taxon == null ? Guid.Empty : device.Taxon.Id,
                Description = device.Description,
                AuthenticationMethod = device.AuthenticationMethod,
                Organizations = organizationList,
            };
        }

        #endregion
    }
}