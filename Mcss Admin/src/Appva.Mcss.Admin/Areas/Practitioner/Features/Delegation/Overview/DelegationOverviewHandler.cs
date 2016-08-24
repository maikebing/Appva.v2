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
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationOverviewHandler"/> class.
        /// </summary>
        public DelegationOverviewHandler(IPersistenceContext persistence, ITaxonFilterSessionHandler filtering)
        {
            this.persistence = persistence;
            this.filtering   = filtering;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override DelegationOverviewModel Handle(Parameterless<DelegationOverviewModel> message)
        {
            var taxon = this.filtering.GetCurrentFilter();
            var fiftyDaysFromNow = DateTime.Today.AddDays(50);
            var delegations = this.persistence.QueryOver<Delegation>()
                .Where(x => x.IsActive == true && x.Pending == false)
                .And(x => x.EndDate <= fiftyDaysFromNow)
                .OrderBy(o => o.EndDate).Asc
                    .JoinQueryOver<Account>(x => x.Account)
                        .Where(x => x.IsActive == true)
                    .JoinQueryOver<Taxon>(x => x.Taxon)
                        .WhereRestrictionOn(x => x.Path).IsLike(taxon.Id.ToString(), MatchMode.Anywhere)
                .List();
            var delegationsExpired = from x in delegations
                                     where x.EndDate.Subtract(DateTimeUtilities.Now()).Days < 0
                                     group x by x.Account into gr
                                     select new DelegationExpired
                                     {
                                         Id = gr.Key.Id,
                                         FullName = gr.Key.FullName,
                                         DaysLeft = gr.Min(x => x.EndDate.Subtract(DateTimeUtilities.Now()).Days)
                                     };
            var delegationsExpiresWithin50Days = from x in delegations
                                                 where x.EndDate.Subtract(DateTimeUtilities.Now()).Days >= 0
                                                 group x by x.Account into gr
                                                 select new DelegationExpired
                                                 {
                                                     Id = gr.Key.Id,
                                                     FullName = gr.Key.FullName,
                                                     DaysLeft = gr.Min(x => x.EndDate.Subtract(DateTimeUtilities.Now()).Days)
                                                 };
            return new DelegationOverviewModel
            {
                DelegationsExpired = delegationsExpired,
                DelegationsExpiresWithin50Days = delegationsExpiresWithin50Days
            };
        }

        #endregion
    }
}