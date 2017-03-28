// <copyright file="ListDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListDelegationHandler : RequestHandler<ListDelegation, ListDelegationModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegations;

        /// <summary>
        /// The <see cref="IPatientService"/>
        /// </summary>
        private readonly IPatientService patients;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        /// <summary>
        /// The <see cref="IAccountTransformer"/>.
        /// </summary>
        private readonly IAccountTransformer transformer;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDelegationHandler"/> class.
        /// </summary>
        public ListDelegationHandler(
            IIdentityService identity,
            IAccountService accounts, 
            IDelegationService delegations,
            IPatientService patients,
            ITaxonomyService taxonomies,
            IAccountTransformer transformer,
            IAuditService auditing,
            IPersistenceContext persistence)
        {
            this.identity = identity;
            this.accounts = accounts;
            this.delegations = delegations;
            this.patients = patients;
            this.taxonomies = taxonomies;
            this.auditing = auditing;
            this.persistence = persistence;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListDelegationModel Handle(ListDelegation message)
        {
            var user          = this.accounts.CurrentPrincipal();
            var userLocation  = this.identity.Principal.LocationPath();
            var account       = this.accounts.Find(message.Id);
            var isInvisible   = account.Roles.Where(x => x.IsVisible).Count() == 0 && ! this.identity.IsInRole("_AA");
            var delegations   = this.delegations.List(userLocation, byAccount: message.Id, isActive: true);
            var taxons        = this.taxonomies.List(TaxonomicSchema.Organization);    
            var delegationMap = new Dictionary<string, IList<DelegationViewModel>>();
            var available     = user.GetRoleDelegationAccess();
            foreach (var delegation in delegations)
            {
                var taxon = this.taxonomies.Find(delegation.Taxon.Parent.Id, TaxonomicSchema.Delegation);
                if (taxon == null)
                {
                    continue;
                }
                if (! available.Where(x => x.Id == delegation.Taxon.Parent.Id).Any())
                {
                    continue;
                }
                var organizationalTaxon      = this.taxonomies.Find(delegation.OrganisationTaxon.Id, TaxonomicSchema.Organization);
                var isEditableForCurrentUser = organizationalTaxon.Path.StartsWith(userLocation);
                if (delegationMap.ContainsKey(taxon.Name))
                {
                    delegationMap[taxon.Name].Add(DelegationTransformer.ToDelegationViewModel(delegation, taxons, isEditableForCurrentUser));
                }
                else
                {
                    delegationMap.Add(taxon.Name, new List<DelegationViewModel> { DelegationTransformer.ToDelegationViewModel(delegation, taxons, isEditableForCurrentUser) });
                }
            }
            var knowledgeTestMap = new Dictionary<string, IList<KnowledgeTest>>();
            var knowledgeTests = this.persistence.QueryOver<KnowledgeTest>()
                    .Where(x => x.IsActive)
                      .And(x => x.Account.Id == message.Id)
                .List();
            foreach (var knowledgeTest in knowledgeTests)
            {
                if (this.taxonomies.Find(knowledgeTest.DelegationTaxon.Id, TaxonomicSchema.Delegation) != null)
                {
                    var name = this.taxonomies.Find(knowledgeTest.DelegationTaxon.Id, TaxonomicSchema.Delegation).Name;
                    if (knowledgeTestMap.ContainsKey(name))
                    {
                        knowledgeTestMap[name].Add(knowledgeTest);
                    }
                    else
                    {
                        knowledgeTestMap.Add(name, new List<KnowledgeTest>() { knowledgeTest });
                    }
                }
            }
            this.auditing.Read("läste delegeringar för användare {0}", account.FullName);
            return new ListDelegationModel
            {
                AccountId                 = account.Id,
                Account                   = this.transformer.ToAccount(account),
                DelegationMap             = delegationMap.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value),
                KnowledgeTestMap          = knowledgeTestMap,
                IsAccountVisibilityHidden = isInvisible
            };
        }

        #endregion
    }
}