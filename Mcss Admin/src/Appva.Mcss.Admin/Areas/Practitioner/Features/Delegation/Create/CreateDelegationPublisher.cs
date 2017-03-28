// <copyright file="CreateDelegationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Core.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateDelegationPublisher : RequestHandler<CreateDelegationModel, ListDelegation>
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
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        /// <summary>
        /// The <see cref="IPatientService"/>
        /// </summary>
        private readonly IPatientService patients;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegations;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDelegationPublisher"/> class.
        /// </summary>
        public CreateDelegationPublisher(
            IIdentityService identity, 
            IAccountService accounts, 
            ITaxonomyService taxonomies, 
            IDelegationService delegations,
            IPatientService patientService)
        {
            this.identity = identity;
            this.accounts = accounts;
            this.taxonomies = taxonomies;
            this.delegations = delegations;
            this.patients = patientService;
        }

        #endregion

        #region RequestHandler overrides

        public override ListDelegation Handle(CreateDelegationModel message)
        {
            //// The account which creates the delegation
            var delegatingAccount = this.identity.PrincipalId;
            //// The accoutn which recives the delegation
            var delegatedAccount = this.accounts.Find(message.Id);
            var user = this.accounts.Find(delegatingAccount);
            var patients = new List<Patient>();
            if (message.ValidForSpecificPatients)
            {
                foreach (string guid in message.Patients)
                {
                    Guid patientId;
                    if (Guid.TryParse(guid, out patientId))
                    {
                        var patient = this.patients.Get(patientId);
                        if (! patients.Contains(patient))
                        {
                            patients.Add(patient);
                        }
                    }
                }
            }

            var orgTaxon = message.OrganizationTaxon.IsNotNull() ? 
                this.taxonomies.Find(new Guid(message.OrganizationTaxon), TaxonomicSchema.Organization) : 
                this.taxonomies.Roots(TaxonomicSchema.Organization).FirstOrDefault();
            //// IF the org taxon is null then the root taxon will be choosen...
            //// We should change it. 
            foreach (Guid delegation in message.Delegations)
            {
                var taxon = this.taxonomies.Find(delegation, TaxonomicSchema.Delegation);
                var del = new Delegation
                {
                    Account = delegatedAccount,
                    StartDate = message.StartDate,
                    EndDate = message.EndDate,
                    Name = taxon.Name,
                    Pending = true,
                    Patients = patients,
                    OrganisationTaxon = this.taxonomies.Load(orgTaxon.Id),
                    Taxon = this.taxonomies.Load(taxon.Id),
                    CreatedBy = this.accounts.Load(delegatingAccount),
                    IsGlobal = (message.Patients.IsNotNull() && message.Patients.Count() > 0) ? false : true
                };
                if (del.IsGlobal)
                {
                    del.OrganisationTaxon = user.Locations.First().Taxon; 
                }
                this.delegations.Save(del);
            }

            return new ListDelegation 
            { 
                Id = delegatedAccount.Id
            };
        }

        #endregion
    }
}