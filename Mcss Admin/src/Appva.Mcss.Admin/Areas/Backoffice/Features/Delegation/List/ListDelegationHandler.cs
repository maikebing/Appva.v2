﻿// <copyright file="ListDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.List
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Domain.Entities;
    using Persistence;

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
            Taxon org = null;
            
            var categories = this.taxonomyService.Roots(TaxonomicSchema.Delegation);
            var delegations = new Dictionary<ITaxon, IList<ITaxon>>();

            var activeDelegations = this.delegationService.List()
                       .Where(x => x.StartDate.CompareTo(DateTime.Now) < 0).ToList()
                       .Where(x => x.EndDate.CompareTo(DateTime.Now) > 0).ToList();

            var filteredDelegations = this.persistanceContext.QueryOver<Delegation>()
                .JoinAlias(x => x.OrganisationTaxon, () => org)
                .WhereRestrictionOn(() => org.Path).IsLike(this.filter.GetCurrentFilter().Path + "%")
                .List();

            foreach (var cat in categories)
            {
                delegations.Add(cat, this.taxonomyService.ListByParent(TaxonomicSchema.Delegation, cat));
            }

            return new ListDelegationModel
            {
                Delegations = delegations,
                ActiveDelegations = activeDelegations,
                FilteredDelegations = filteredDelegations
            };
        }

        #endregion
    }
}