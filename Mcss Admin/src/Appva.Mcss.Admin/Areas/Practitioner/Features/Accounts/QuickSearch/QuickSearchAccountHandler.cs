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
        /// <param name="accounts">The <see cref="IAccountService"/></param>
        public QuickSearchAccountHandler(
            IRuntimeMemoryCache cache,
            ISettingsService settings,
            IIdentityService identities,
            IAccountService accounts,
            ITaxonFilterSessionHandler filtering)
        {
            this.cache = cache;
            this.settings = settings;
            this.identities = identities;
            this.accounts = accounts;
            this.filtering = filtering;
        }

        #endregion

        #region IRequestHandler overrides.

        /// <inheritdoc />
        public override IEnumerable<object> Handle(QuickSearchAccount message)
        {
            ////this.settings.Find<bool>(SettingKey.AutogeneratePasswordForMobileDevice, false);
            this.settings.Find<bool>(ApplicationSettings.IsAccessControlInPreviewMode, false);
            this.settings.Find<bool>(ApplicationSettings.IsAccessControlInstalled, false);

            var accounts = this.accounts.Search(
                new SearchAccountModel
                {
                    IsFilterByIsActiveEnabled = message.IsFilterByIsActiveEnabled,
                    IsFilterByIsPausedEnabled = message.IsFilterByIsPausedEnabled,
                    IsFilterByCreatedByEnabled = message.IsFilterByCreatedByEnabled,
                    DelegationFilterId = message.DelegationFilterId,
                    RoleFilterId = message.RoleFilterId,
                    OrganisationFilterId = filtering.GetCurrentFilter().Id, // FIXME: Global filter
                    CurrentUserId = this.identities.PrincipalId,
                    SearchQuery = message.Term
                },
                pageSize: 30);

            return accounts.Entities.Select(x => x.FullName).ToList();
        }

        #endregion
    }
}