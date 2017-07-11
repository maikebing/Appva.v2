// <copyright file="UpdateRolesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System.Web.Mvc;
    using Appva.Mcss.Web;
    using Appva.Mcss.Admin.Application.Common;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRolesHandler : RequestHandler<UpdateRoles, UpdateRolesForm>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService service;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesHandler"/> class.
        /// </summary>
        /// <param name="service"></param>
        public UpdateRolesHandler(IAccountService service, ITaxonomyService taxonomies)
        {
            this.service    = service;
            this.taxonomies = taxonomies;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateRolesForm Handle(UpdateRoles message)
        {
            var user    = this.service.CurrentPrincipal();
            var userLocation    = user.Locations.Count > 0 ?
                this.taxonomies.Find(user.Locations.First().Taxon.Id, TaxonomicSchema.Organization) :
                this.taxonomies.Roots(TaxonomicSchema.Organization).First();
            var account = this.service.Find(message.Id);
            var roles   = user.GetRoleAccess();
            var selected = account.Locations.Count > 0 ?
               this.taxonomies.Find(account.Locations.First().Taxon.Id, TaxonomicSchema.Organization) :
               this.taxonomies.Roots(TaxonomicSchema.Organization).First();
            return new UpdateRolesForm
            {
                Roles         = roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                SelectedRoles = account.Roles.Select(x => x.Id.ToString()).ToArray(),
                Taxons        = TaxonomyHelper.CreateItems(user, selected, this.taxonomies.List(TaxonomicSchema.Organization)),
                Taxon         = selected.Id,
                CanUpdateTaxonPermission = userLocation.IsParentOf(selected)

            };
        }

        #endregion
    }
}