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
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using NHibernate.Type;
using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class QuickSearchAccountHandler : RequestHandler<QuickSearchAccount, IEnumerable<object>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identities;

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAccountHandler"/> class.
        /// </summary>
        /// <param name="identities">The <see cref="IIdentityService"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        public QuickSearchAccountHandler(
            IIdentityService identities,
            IAccountService accountService,
            ITaxonFilterSessionHandler filtering)
        {
            this.identities = identities;
            this.accountService = accountService;
            this.filtering  = filtering;
        }

        #endregion

        #region IRequestHandler overrides.

        /// <inheritdoc />
        public override IEnumerable<object> Handle(QuickSearchAccount message)
        {
            var user     = this.accountService.CurrentPrincipal();
            var accounts = this.accountService.Search(
                new SearchAccountModel
                {
                    IsFilterByIsActiveEnabled   = message.IsFilterByIsActiveEnabled,
                    IsFilterByIsPausedEnabled   = message.IsFilterByIsPausedEnabled,
                    IsFilterByCreatedByEnabled  = message.IsFilterByCreatedByEnabled,
                    DelegationFilterId          = message.DelegationFilterId,
                    RoleFilterId                = message.RoleFilterId,
                    OrganisationFilterTaxonPath = filtering.GetCurrentFilter().Path,
                    CurrentUserId               = this.identities.PrincipalId,
                    SearchQuery                 = message.Term,
                    CurrentUserLocationPath     = user.Locations.First().Taxon.Path //// TODO: Grab from Principal object.
                },
                pageSize: 30);
            return accounts.Items.Select(x => x.FullName).ToList();
        }

        #endregion
    }
}