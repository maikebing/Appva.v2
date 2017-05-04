// <copyright file="ListAccountHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Caching.Providers;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListAccountHandler : RequestHandler<ListAccount, ListAccountModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegations;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAccountHandler"/> class.
        /// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="filtering">The <see cref="ITaxonFilterSessionHandler"/>.</param>
        /// <param name="delegations">The <see cref="IDelegationService"/>.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/>.</param>
        public ListAccountHandler(
            IAccountService accountService,
            ITaxonFilterSessionHandler filtering,
            IDelegationService delegations,
            ITaxonomyService taxonomyService)
        {
            this.accountService    = accountService;
            this.filtering         = filtering;
            this.delegations       = delegations;
            this.taxonomyService   = taxonomyService;
        }

        #endregion

        #region IRequestHandler overrides.

        /// <inheritdoc />
        public override ListAccountModel Handle(ListAccount message)
        {
            var user      = this.accountService.CurrentPrincipal();
            var taxonPath = user.Locations.Count > 0 ? user.Locations.First().Taxon.Path : this.taxonomyService.Roots(TaxonomicSchema.Organization).First().Path;
            var roles     = user.GetRoleAccess();
            var accounts  = this.accountService.Search(
                new SearchAccountModel
                {
                    IsFilterByIsActiveEnabled       = message.isActive.GetValueOrDefault(true),
                    IsFilterByIsPausedEnabled       = message.isPaused.GetValueOrDefault(false),
                    IsFilterByIsSynchronizedEnabled = message.isSynchronized,
                    IsFilterByCreatedByEnabled      = message.filterByCreatedBy.GetValueOrDefault(false),
                    DelegationFilterId              = message.DelegationFilterId,
                    RoleFilterId                    = message.RoleFilterId,
                    OrganisationFilterTaxonPath     = this.filtering.GetCurrentFilter().Path,
                    CurrentUserId                   = user.Id,
                    SearchQuery                     = message.q,
                    CurrentUserLocationPath         = taxonPath //// TODO: Grab from Principal object.
                },
                message.page.GetValueOrDefault(1));
            return new ListAccountModel
            {
                Accounts                        = accounts,
                Roles                           = roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                Delegations                     = this.delegations.ListDelegationTaxons(includeRoots: false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList<SelectListItem>(),
                RoleFilterId                    = message.RoleFilterId,
                DelegationFilterId              = message.DelegationFilterId,
                IsFilterByCreatedByEnabled      = message.filterByCreatedBy,
                IsFilterByIsActiveEnabled       = message.isActive.GetValueOrDefault(true),
                IsFilterByIsPausedEnabled       = message.isPaused.GetValueOrDefault(false),
                IsFilterByIsSynchronizedEnabled = message.isSynchronized
            };
        }

        #endregion
    }
}