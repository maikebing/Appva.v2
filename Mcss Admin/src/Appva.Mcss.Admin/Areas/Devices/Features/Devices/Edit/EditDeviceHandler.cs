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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;

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
        private readonly IPersistenceContext persistenceContext;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDeviceHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public EditDeviceHandler(IPersistenceContext persistenceContext, ITaxonomyService taxonomyService)
        {
            this.persistenceContext = persistenceContext;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditDeviceModel Handle(Identity<EditDeviceModel> message)
        {
            var deviceModel = new EditDeviceModel();
            var organizationList = new List<SelectListItem>();

            var query = this.persistenceContext.QueryOver<Device>()
                .Where(x => x.Id == message.Id)
                    .SingleOrDefault();

            var organizations = this.taxonomyService.List(Application.Common.TaxonomicSchema.Organization);

            foreach (var organization in organizations)
            {
                organizationList.Add(new SelectListItem()
                {
                    Text = organization.Name,
                    Value = organization.Id.ToString(),
                    Selected = (query.Taxon.Path == organization.Path ? true : false)
                });
            }

            deviceModel.Description = query.Description;
            deviceModel.Organizations = organizationList;
            return deviceModel;
        }

        #endregion
    }
}