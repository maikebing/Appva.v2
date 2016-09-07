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

        /// <summary>
        /// The <see cref="IPatientService"/>
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegationService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDelegationPublisher"/> class.
        /// </summary>
        public UpdateDelegationPublisher(
            ITaxonomyService taxonomyService,
            IAccountService accountService,
            IAuditService auditing,
            IIdentityService identity,
            IPatientService patientService,
            IDelegationService delegationService)
        {
            this.taxonomyService   = taxonomyService;
            this.accountService    = accountService;
            this.auditing          = auditing;
            this.identity          = identity;
            this.patientService    = patientService;
            this.delegationService = delegationService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// </inheritdoc />
        public override ListDelegation Handle(UpdateDelegationModel message)
        {
            var currentUser = this.accountService.Load(this.identity.PrincipalId);
            var rootId = this.taxonomyService.Roots(TaxonomicSchema.Organization).First().Id;

            var updateDelegation = new Application.Models.DelegationUpdateModel
            {
                StartDate = message.StartDate,
                EndDate = message.EndDate,
                CreatedBy = currentUser,
                IsGlobal = message.ValidForSpecificPatients == false || message.Patients.Length == 0
            };
            if (message.ValidForSpecificPatients)
            {
                updateDelegation.Patients = new List<Patient>();
                foreach (string guid in message.Patients)
                {
                    Guid patientId;
                    if (Guid.TryParse(guid, out patientId))
                    {
                        Patient patient = this.patientService.Get(patientId);
                        if (!updateDelegation.Patients.Contains(patient))
                        {
                            updateDelegation.Patients.Add(patient);
                        }
                    }
                }
                updateDelegation.OrganisationTaxon = this.taxonomyService.Load(rootId);
            }
            else
            {
                updateDelegation.Patients = new List<Patient>();
                updateDelegation.OrganisationTaxon = message.OrganizationTaxon.IsNotNull() ? this.taxonomyService.Load(new Guid(message.OrganizationTaxon)) : this.taxonomyService.Load(rootId);
            }

            this.delegationService.Update(message.Id, updateDelegation);

            this.auditing.Update(
                "ändrade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för patient/patienter {4} för användare {5}.",
                message.Id,
                updateDelegation.StartDate.GetValueOrDefault(),
                updateDelegation.EndDate.GetValueOrDefault(),
                message.AccountId,
                updateDelegation.IsGlobal.GetValueOrDefault() ? "alla" : string.Join(",", updateDelegation.Patients.ToArray().Select(x => x.FullName)),
                message.Id);

            return new ListDelegation { Id = message.AccountId };
        }

        #endregion
    }
}