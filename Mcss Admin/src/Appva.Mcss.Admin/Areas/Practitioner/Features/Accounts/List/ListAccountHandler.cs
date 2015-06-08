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
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Caching.Providers;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
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
        /// The <see cref="ICacheService"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identities;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roles;

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAccountHandler"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="identities">The <see cref="IIdentityService"/></param>
        /// <param name="taxonomies">The <see cref="ITaxonomyService"/></param>
        /// <param name="roles">The <see cref="IRoleService"/></param>
        /// <param name="accounts">The <see cref="IAccountService"/></param>
        public ListAccountHandler(
            IRuntimeMemoryCache cache,
            ISettingsService settings,
            IIdentityService identities,
            ITaxonomyService taxonomies,
            IRoleService roles,
            IAccountService accounts,
            ITaxonFilterSessionHandler filtering)
        {
            this.cache = cache;
            this.settings = settings;
            this.identities = identities;
            this.taxonomies = taxonomies;
            this.roles = roles;
            this.accounts = accounts;
            this.filtering = filtering;
        }

        #endregion

        #region IRequestHandler overrides.

        /// <inheritdoc />
        public override ListAccountModel Handle(ListAccount message)
        {
            var accounts = this.accounts.Search(
                new SearchAccountModel
                {
                    IsFilterByIsActiveEnabled = message.isActive.GetValueOrDefault(true),
                    IsFilterByIsPausedEnabled = message.isPaused.GetValueOrDefault(false),
                    IsFilterByCreatedByEnabled = message.filterByCreatedBy.GetValueOrDefault(false),
                    DelegationFilterId = message.DelegationFilterId,
                    RoleFilterId = message.RoleFilterId,
                    OrganisationFilterId = this.filtering.GetCurrentFilter().Id,
                    CurrentUserId = this.identities.PrincipalId,
                    SearchQuery = message.q
                },
                page: message.page.GetValueOrDefault(1));

            return new ListAccountModel
            {
                Accounts = accounts,
                Roles = this.roles.ListVisible()
                    .Select(
                        x => new SelectListItem()
                        {
                            Value = x.Id.ToString(),
                            Text = x.Name
                        })
                    .ToList()
                    ,
                Delegations = this.taxonomies.List(TaxonomicSchema.Delegation)
                    .Where(x => !x.IsRoot)
                    .Select(
                        x => new SelectListItem()
                        {
                            Value = x.Id.ToString(),
                            Text = x.Name
                        })
                    .ToList<SelectListItem>(),
                RoleFilterId = message.RoleFilterId,
                DelegationFilterId = message.DelegationFilterId,
                IsFilterByCreatedByEnabled = message.filterByCreatedBy,
                IsFilterByIsActiveEnabled = message.isActive.GetValueOrDefault(true),
                IsFilterByIsPausedEnabled = message.isPaused.GetValueOrDefault(false)
            };
        }

        #endregion
    }
}