// <copyright file="ListDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.List
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Domain.Entities;
    using Persistence;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListDelegationHandler : RequestHandler<Parameterless<ListDelegationModel>, ListDelegationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistanceContext;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>
        /// </summary>
        private readonly ITaxonFilterSessionHandler filter;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDelegationHandler"/> class.
        /// <param name="taxonomyService"></param>
        /// <param name="delegationService"></param>
        /// <param name="persistanceContext"></param>
        /// <param name="filter"></param>
        /// </summary>
        public ListDelegationHandler(ITaxonomyService taxonomyService, IDelegationService delegationService, IPersistenceContext persistanceContext, ITaxonFilterSessionHandler filter)
        {
            this.taxonomyService = taxonomyService;
            this.delegationService = delegationService;
            this.persistanceContext = persistanceContext;
            this.filter = filter;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListDelegationModel Handle(Parameterless<ListDelegationModel> message)
        {
            Taxon organization = null;
            
            var categories = this.taxonomyService.Roots(TaxonomicSchema.Delegation);
            var delegations = new Dictionary<ITaxon, Dictionary<ListDelegation, ITaxon>>();

            foreach (var cat in categories)
            {
                var delegationList = this.taxonomyService.ListByParent(TaxonomicSchema.Delegation, cat);
                var delegationItems = new Dictionary<ListDelegation, ITaxon>();

                foreach (var item in delegationList)
                {
                    var delegationItem = new ListDelegation();
                    var query = this.persistanceContext.QueryOver<Delegation>()
                        .Where(x => x.Taxon.Id == item.Id)
                            .JoinAlias(x => x.OrganisationTaxon, () => organization)
                                .WhereRestrictionOn(() => organization.Path).IsLike(MatchMode.Start.ToMatchString(this.filter.GetCurrentFilter().Path));

                    delegationItem.TotalDelegationsCount = query.RowCount();
                    delegationItem.ActiveDelegationsCount = query
                        .Where(x => x.StartDate <= DateTime.Now)
                            .And(x => x.EndDate > DateTime.Now)
                                .RowCount();

                    delegationItems.Add(delegationItem, item);
                }

                delegations.Add(cat, delegationItems);
            }

            return new ListDelegationModel
            {
                Delegations = delegations
            };
        }

        #endregion
    }
}