﻿// <copyright file="OverviewOrderHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.Controllers;
using NHibernate.Criterion;
using NHibernate.Transform;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class OverviewOrderHandler : RequestHandler<OverviewOrder, OrderOverviewViewModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewOrderHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        /// <param name="filtering">The <see cref="ITaxonFilterSessionHandler"/></param>
        public OverviewOrderHandler(
            IIdentityService identityService, 
            IPersistenceContext persistence,
            ITaxonFilterSessionHandler filtering)
        {
            this.identityService = identityService;
            this.persistence = persistence;
            this.filtering = filtering;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override OrderOverviewViewModel Handle(OverviewOrder message)
        {
            var account = this.persistence.Get<Account>(this.identityService.PrincipalId);
            var filterTaxon = this.filtering.GetCurrentFilter();
            var orders = this.persistence.QueryOver<Article>()
                .Where(x => x.IsActive)
                    .And(x => x.Refill == true)
                        .Fetch(x => x.RefillOrderedBy).Eager;

            orders.JoinQueryOver<Patient>(x => x.Patient)
                .Where(x => x.IsActive)
                    .And(x => x.Deceased == false)
                        .JoinQueryOver<Taxon>(x => x.Taxon)
                            .Where(Restrictions.On<Taxon>(x => x.Path)
                                .IsLike(filterTaxon.Id.ToString(), MatchMode.Anywhere));

            return new OrderOverviewViewModel
            {
                Orders = orders.OrderBy(x => x.RefillOrderDate).Asc.List()
            };
        }

        #endregion
    }
}