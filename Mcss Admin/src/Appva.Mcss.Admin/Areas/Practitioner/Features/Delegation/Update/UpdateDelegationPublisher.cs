// <copyright file="UpdateDelegationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateDelegationPublisher : RequestHandler<UpdateDelegationModel, ListDelegation>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDelegationPublisher"/> class.
        /// </summary>
        public UpdateDelegationPublisher(
            IPersistenceContext persistence,
            ITaxonomyService taxonomyService,
            IAccountService accountService,
            IAuditService auditing,
            IIdentityService identity)
        {
            this.persistence     = persistence;
            this.taxonomyService = taxonomyService;
            this.accountService  = accountService;
            this.auditing        = auditing;
            this.identity        = identity;
        }

        #endregion

        #region RequestHandler Overrides.

        /// </inheritdoc />
        public override ListDelegation Handle(UpdateDelegationModel message)
        {
            var currentUser = this.accountService.Load(this.identity.PrincipalId);
            var delegation = this.persistence.Get<Delegation>(message.Id);
            var oldPatients = delegation.Patients;
            var rootId = this.taxonomyService.Roots(TaxonomicSchema.Organization).First().Id;
            this.persistence.Save(new ChangeSet
            {
                EntityId = delegation.Id,
                Entity = typeof(Delegation).ToString(),
                Revision = delegation.Version,
                ModifiedBy = currentUser,
                Changes = new List<Change> {
                    new Change {
                        Property = "StartDate",
                        OldState = delegation.StartDate.ToShortDateString(),
                        NewState = message.StartDate.ToShortDateString(),
                        TypeOf = typeof(DateTime).ToString()
                    },
                    new Change {
                        Property = "EndDate",
                        OldState = delegation.EndDate.ToShortDateString(),
                        NewState = message.EndDate.ToShortDateString(),
                        TypeOf = typeof(DateTime).ToString()
                    },
                    new Change {
                        Property = "Patients",
                        OldState = string.Join(",", oldPatients),
                        NewState = string.Join(",", delegation.Patients),
                        TypeOf = typeof(Array).ToString()
                    },
                    new Change {
                        Property = "OrganisationTaxon",
                        OldState = delegation.Taxon.ToString(),
                        NewState = (message.ValidForSpecificPatients ||  message.OrganizationTaxon.IsNull() ? this.taxonomyService.Load(rootId) : this.taxonomyService.Load(new Guid(message.OrganizationTaxon))).ToString(),
                        TypeOf = typeof(Taxon).ToString()
                    },
                    new Change {
                        Property = "CreatedBy",
                        OldState = delegation.CreatedBy.ToString(),
                        NewState = currentUser.ToString(),
                        TypeOf = typeof(Account).ToString()
                    }
                }
            });
            if (message.ValidForSpecificPatients)
            {
                foreach (string guid in message.Patients)
                {
                    Guid patientId;
                    if (Guid.TryParse(guid, out patientId))
                    {
                        Patient patient = this.persistence.Get<Patient>(patientId);
                        if (!delegation.Patients.Contains(patient))
                        {
                            delegation.Add(patient);
                        }
                    }
                }
                delegation.OrganisationTaxon = this.taxonomyService.Load(rootId);
            }
            else
            {
                delegation.Patients = new List<Patient>();
                delegation.OrganisationTaxon = message.OrganizationTaxon.IsNotNull() ? this.taxonomyService.Load(new Guid(message.OrganizationTaxon)) : this.taxonomyService.Load(rootId);
            }
            delegation.StartDate = message.StartDate;
            delegation.EndDate = message.EndDate;
            delegation.UpdatedAt = DateTime.Now;
            delegation.CreatedBy = currentUser;
            delegation.IsGlobal = (message.Patients.IsNotNull() && message.Patients.Count() > 0) ? false : true;
            this.persistence.Update(delegation);
            this.auditing.Update(
                "ändrade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för patient/patienter {4} för användare {5} (REF: {6}).",
                delegation.Name,
                delegation.StartDate,
                delegation.EndDate,
                delegation.Id,
                delegation.IsGlobal ? "alla" : string.Join(",", delegation.Patients.ToArray().Select(x => x.FullName)),
                delegation.Account.FullName,
                delegation.Account.Id);

            return new ListDelegation { Id = delegation.Account.Id };
        }

        #endregion
    }
}