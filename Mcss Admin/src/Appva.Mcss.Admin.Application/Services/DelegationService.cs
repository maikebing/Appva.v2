// <copyright file="DelegationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Core.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Auditing;

    #endregion

    /// <summary>
    /// The <see cref="Delegation"/> service.
    /// </summary>
    public interface IDelegationService : IService
    {
        /// <summary>
        /// Lists all Delegation taxons
        /// </summary>
        /// <returns>List of <see cref="Taxon"/></returns>
        IList<ITaxon> ListDelegationTaxons(Guid? byRoot = null, bool includeRoots = true);

        /// <summary>
        /// Lists delegations
        /// </summary>
        /// <param name="byAccount"></param>
        /// <param name="isPending"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IList<Delegation> List(Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null);

        /// <summary>
        /// Saves a delegation to database
        /// </summary>
        /// <param name="delegation"></param>
        void Save(Delegation delegation);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationService : IDelegationService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        /// <summary>
        /// The <see cref="IDelegationRepository"/>.
        /// </summary>
        private readonly IDelegationRepository repository;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationService"/> class.
        /// </summary>
        public DelegationService(ITaxonomyService taxonomies, IDelegationRepository repository, IAuditService auditing)
        {
            this.taxonomies = taxonomies;
            this.repository = repository;
            this.auditing = auditing;
        }

        #endregion

        #region IDelegationService members.

        /// <inheritdoc />
        public IList<ITaxon> ListDelegationTaxons(Guid? byRoot = null, bool includeRoots = true)
        {
            var taxons = this.taxonomies.List(TaxonomicSchema.Delegation);
            
            if(!includeRoots) 
            {
                taxons = taxons.Where(x => !x.IsRoot).ToList();
            }
            if(byRoot.HasValue && byRoot.Value.IsNotEmpty())
            {
                taxons = taxons.Where(x => x.ParentId.Value == byRoot.Value).ToList();
            }

            taxons = taxons.OrderBy<ITaxon, int>(t => t.Sort).ThenBy<ITaxon, string>(t => t.Name).ToList();

            return taxons;
        }

        /// <inheritdoc />
        public IList<Delegation> List(Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null)
        {
            return this.repository.List(byAccount, createdBy, byCategory, isPending, isGlobal, isActive);
        }

        public void Save(Delegation delegation)
        {
            
            this.repository.Save(delegation);

            this.auditing.Create(
                    "lade till delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för patient/patienter {4} för användare {5} (REF: {6}).",
                    delegation.Name,
                    delegation.StartDate,
                    delegation.EndDate,
                    delegation.Id,
                    delegation.IsGlobal ? "alla" : string.Join(",", delegation.Patients.ToArray().Select(x => x.FullName)),
                    delegation.Account.FullName,
                    delegation.Account.Id);
        }

        #endregion
    }
}