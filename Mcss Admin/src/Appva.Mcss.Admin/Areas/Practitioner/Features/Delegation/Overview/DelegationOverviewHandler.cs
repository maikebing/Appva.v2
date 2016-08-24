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
using Appva.Mcss.Admin.Application.Services;
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
    internal sealed class DelegationOverviewHandler : RequestHandler<Parameterless<DelegationOverviewModel>, DelegationOverviewModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationOverviewHandler"/> class.
        /// </summary>
        public DelegationOverviewHandler(ITaxonFilterSessionHandler filtering, IAccountService accountService)
        {
            this.filtering      = filtering;
            this.accountService = accountService;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override DelegationOverviewModel Handle(Parameterless<DelegationOverviewModel> message)
        {
            var accounts = this.accountService.ListByExpiringDelegation(
                this.filtering.GetCurrentFilter(), 
                DateTime.Today.AddDays(50));

            return new DelegationOverviewModel
            {
                DelegationsExpired = accounts.Where(x => x.DelegationDaysLeft < 0).ToList(),
                DelegationsExpiresWithin50Days = accounts.Where(x => x.DelegationDaysLeft >= 0).ToList()
            };
        }

        #endregion
    }
}