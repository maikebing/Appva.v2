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
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services.Settings;

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
        IList<Delegation> List(string taxonfilter, Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null);

        /// <summary>
        /// Saves a delegation to database
        /// </summary>
        /// <param name="delegation"></param>
        void Save(Delegation delegation);

        /// <summary>
        /// Inactivates the delegation
        /// </summary>
        /// <param name="delegation"></param>
        void Delete(Delegation delegation);

        /// <summary>
        /// Inactivates the delegation
        /// </summary>
        /// <param name="delegation"></param>
        void Delete(Delegation delegation, string reason);

        /// <summary>
        /// Find a delegation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Delegation Find(Guid id);

        /// <summary>
        /// Update a delegation
        /// </summary>
        /// <param name="delegation"></param>
        void Update(Guid delegationId, DelegationUpdateModel model);

        /// <summary>
        /// Activates a pending delegation
        /// </summary>
        /// <param name="delegation"></param>
        void Activate(Delegation delegation);
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
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationService"/> class.
        /// </summary>
        public DelegationService(
            ITaxonomyService taxonomies, 
            IDelegationRepository repository, 
            IAccountService accountService,
            IIdentityService identity,
            IAuditService auditing, 
            ISettingsService settings)
        {
            this.taxonomies     = taxonomies;
            this.repository     = repository;
            this.accountService = accountService;
            this.identity       = identity;
            this.auditing       = auditing;
            this.settings       = settings;
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
        public IList<Delegation> List(string taxonfilter, Guid? byAccount = null, Guid? createdBy = null, Guid? byCategory = null, bool? isPending = null, bool? isGlobal = null, bool? isActive = null)
        {
            return this.repository.List(taxonfilter, byAccount, createdBy, byCategory, isPending, isGlobal, isActive);
        }

        /// <inheritdoc />
        public void Save(Delegation delegation)
        {   
            this.repository.Save(delegation);

            this.auditing.Create(
                    "lade till delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för patient/patienter {4} för användare {5}.",
                    delegation.Name,
                    delegation.StartDate,
                    delegation.EndDate,
                    delegation.Id,
                    delegation.IsGlobal ? "alla" : string.Join(",", delegation.Patients.ToArray().Select(x => x.Id)),
                    delegation.Account.Id);
        }

        /// <inheritdoc />
        public void Delete(Delegation delegation)
        {
            this.auditing.Update(
               "inaktiverade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4}.",
               delegation.Name,
               delegation.StartDate,
               delegation.EndDate,
               delegation.Id,
               delegation.Account.Id);
            this.Update(delegation.Id, new DelegationUpdateModel { IsActive = false });
        }

        /// <inheritdoc />
        public void Delete(Delegation delegation, string reason)
        {
            this.auditing.Update(
               "inaktiverade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4} med anledning: {5}.",
               delegation.Name,
               delegation.StartDate,
               delegation.EndDate,
               delegation.Id,
               delegation.Account.Id,
               reason);
            this.Update(delegation.Id, new DelegationUpdateModel { IsActive = false, Reason = reason });
        }

        /// <inheritdoc />
        public Delegation Find(Guid id)
        {
            return this.repository.Find(id);
        }

        /// <inheritdoc />
        public void Update(Guid delegationId, DelegationUpdateModel model)
        {
            var changes = new List<Change>();
            var delegation = this.Find(delegationId);
            if (this.settings.Find<bool>(ApplicationSettings.RequireDelegationActivationAfterChange))
            {
                delegation.Pending = true;
            }
            if (model.CreatedBy.IsNotNull())
            {
                changes.Add(new Change
                {
                    Property = "CreatedBy",
                    OldState = delegation.CreatedBy.Id.ToString(),
                    NewState = model.CreatedBy.Id.ToString(),
                    TypeOf = typeof(Account).ToString()
                });
                delegation.CreatedBy = model.CreatedBy;
            }
            if (model.EndDate.HasValue)
            {
                changes.Add(new Change {
                        Property = "EndDate",
                        OldState = delegation.EndDate.ToShortDateString(),
                        NewState = model.EndDate.GetValueOrDefault().ToShortDateString(),
                        TypeOf = typeof(DateTime).ToString()
                    });
                delegation.EndDate = model.EndDate.GetValueOrDefault();
            }
            if (model.IsActive.HasValue)
            {
                changes.Add(new Change {
                        Property = "IsActive",
                        OldState = delegation.IsActive.ToString(),
                        NewState = model.IsActive.GetValueOrDefault().ToString(),
                        TypeOf = typeof(bool).ToString()
                    });
                delegation.IsActive = model.IsActive.GetValueOrDefault();
            }
            if(model.IsGlobal.HasValue)
            {
                changes.Add(new Change {
                        Property = "IsGlobal",
                        OldState = delegation.IsGlobal.ToString(),
                        NewState = model.IsGlobal.GetValueOrDefault().ToString(),
                        TypeOf = typeof(bool).ToString()
                    });
                delegation.IsGlobal = model.IsGlobal.GetValueOrDefault();
            }
            if (model.OrganisationTaxon.IsNotNull())
            {
                changes.Add(new Change {
                        Property = "OrganisationTaxon",
                        OldState = delegation.OrganisationTaxon.Id.ToString(),
                        NewState = model.OrganisationTaxon.Id.ToString(),
                        TypeOf = typeof(Taxon).ToString()
                    });
                delegation.OrganisationTaxon = model.OrganisationTaxon;
            }
            if (model.Patients.IsNotNull())
            {
                changes.Add(new Change {
                        Property = "Patients",
                        OldState = string.Join(",", delegation.Patients.Select(x => x.Id.ToString())),
                        NewState = string.Join(",", model.Patients.Select(x => x.Id.ToString())),
                        TypeOf = typeof(Array).ToString()
                    });
                delegation.Patients = model.Patients;
            }
            if (model.StartDate.HasValue)
            {
                changes.Add(new Change {
                        Property = "StartDate",
                        OldState = delegation.StartDate.ToShortDateString(),
                        NewState = model.StartDate.GetValueOrDefault().ToShortDateString(),
                        TypeOf = typeof(DateTime).ToString()
                    });
                delegation.StartDate = model.StartDate.GetValueOrDefault();
            }
            if (model.Reason.IsNotEmpty())
            {
                changes.Add(new Change
                {
                    Property = "Reason",
                    OldState = string.Empty,
                    NewState = model.Reason,
                    TypeOf = typeof(string).ToString()
                });
            }

            var changeSet = new ChangeSet
            {
                EntityId = delegation.Id,
                Entity = typeof(Delegation).ToString(),
                Revision = delegation.Version,
                ModifiedBy = this.accountService.Load(this.identity.PrincipalId),
                Changes = changes
            };
            this.repository.Update(delegation,changeSet);
        }

        /// <inheritdoc />
        public void Activate(Delegation delegation)
        {
            delegation.Pending = false;
            this.repository.Update(delegation);
            this.auditing.Update(
                "aktiverade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4} (REF: {5}).",
                delegation.Name,
                delegation.StartDate,
                delegation.EndDate,
                delegation.Id,
                delegation.Account.FullName,
                delegation.Account.Id);
        }

        #endregion
    }
}