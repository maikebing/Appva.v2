// <copyright file="CreateDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateDelegationHandler : RequestHandler<CreateDelegation, CreateDelegationModel>
    {
        #region

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patients;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegations;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDelegationHandler"/> class.
        /// </summary>
        public CreateDelegationHandler(ITaxonFilterSessionHandler filtering, IAccountService accounts, IPatientService patients, IDelegationService delegations, ITaxonomyService taxonomies)
        {
            this.filtering = filtering;
            this.accounts = accounts;
            this.patients = patients;
            this.delegations = delegations;
            this.taxonomies = taxonomies;
        }

        #endregion

        #region RequestHandler Members.

        /// <inheritdoc />
        public override CreateDelegationModel Handle(CreateDelegation message)
        {
            var filterT = this.filtering.GetCurrentFilter();
            var account = this.accounts.Find(message.Id);
            var patients = this.patients.FindByTaxon(filterT.Id);
            var taxons = this.delegations.ListDelegationTaxons().OrderByDescending<ITaxon, bool>(x => x.IsRoot).ToList();
            var patientTaxons = this.delegations.List(byAccount: account.Id, isActive: true, isGlobal: true);
            var existingDelegations = new HashSet<Guid>(patientTaxons.Select(x => x.Taxon.Id));
            var delegationTypes = new List<SelectListItem>();
            var map = new Dictionary<ITaxon, IList<ITaxon>>();
            foreach (var taxon in taxons)
            {
                if (taxon.IsRoot)
                {
                    delegationTypes.Add(new SelectListItem
                    {
                        Text = taxon.Name,
                        Value = taxon.Id.ToString()
                    });
                    map.Add(taxon, new List<ITaxon>());
                }
                else
                {
                    var parent = this.taxonomies.Find(taxon.ParentId.GetValueOrDefault(), TaxonomicSchema.Delegation);
                    if (parent != null && map.ContainsKey(parent))
                    {
                        map[parent].Add(taxon);
                    }
                }
            }
            map = map.ToDictionary(x => x.Key, x => x.Value);

            return new CreateDelegationModel
            {
                Id = message.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365),
                DelegationTemplate = map,
                DelegationsTaken = existingDelegations,
                DelegationTypes = delegationTypes,
                PatientItems = patients.Select(p => new SelectListItem
                {
                    Text = p.FullName,
                    Value = p.Id.ToString()
                }).ToList(),
                Taxons = TaxonomyHelper.SelectList(this.taxonomies.List(TaxonomicSchema.Organization))
            };
        }

        #endregion
    }
}