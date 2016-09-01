// <copyright file="DelegationOverviewHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Core.Utilities;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DelegationOverviewHandler : RequestHandler<DelegationOverview, DelegationOverviewModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationOverviewHandler"/> class.
        /// </summary>
        public DelegationOverviewHandler(
            ITaxonFilterSessionHandler filtering, 
            IAccountService accountService,
            ISettingsService settings,
            IIdentityService identity)
        {
            this.filtering      = filtering;
            this.accountService = accountService;
            this.settings       = settings;
            this.identity       = identity;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override DelegationOverviewModel Handle(DelegationOverview message)
        {
            var accounts = this.accountService.ListByExpiringDelegation(
                this.filtering.GetCurrentFilter(), 
                DateTime.Today.AddDays(50),
                message.FilterByIssuer.GetValueOrDefault(this.settings.Find<bool>(ApplicationSettings.DefaultFilterDelegationOverviewByIssuer)) ? this.identity.PrincipalId : (Guid?)null);

            return new DelegationOverviewModel
            {
                FilteredByIssuer = message.FilterByIssuer.GetValueOrDefault(this.settings.Find<bool>(ApplicationSettings.DefaultFilterDelegationOverviewByIssuer)),
                DelegationsExpired = accounts.Where(x => x.DelegationDaysLeft < 0).OrderByDescending(x => x.DelegationDaysLeft).ToList(),
                DelegationsExpiresWithin50Days = accounts.Where(x => x.DelegationDaysLeft >= 0).OrderBy(x => x.DelegationDaysLeft).ToList()
            };
        }

        #endregion
    }
}