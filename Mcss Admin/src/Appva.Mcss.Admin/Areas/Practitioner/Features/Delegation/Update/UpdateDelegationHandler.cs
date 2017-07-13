// <copyright file="UpdateDelegationHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateDelegationHandler : RequestHandler<Identity<UpdateDelegationModel>, UpdateDelegationModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        #endregion
        
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDelegationHandler"/> class.
        /// </summary>
        public UpdateDelegationHandler(
            IAccountService accountService,
            ITaxonomyService taxonomyService,
            IPatientService patientService,
            IPersistenceContext persistence, 
            ITaxonFilterSessionHandler filtering)
        {
            this.accountService  = accountService;
            this.taxonomyService = taxonomyService;
            this.patientService  = patientService;
            this.persistence     = persistence;
            this.filtering       = filtering;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override UpdateDelegationModel Handle(Identity<UpdateDelegationModel> message)
        {
            var user       = this.accountService.CurrentPrincipal();
            var filter     = this.filtering.GetCurrentFilter();
            var delegation = this.persistence.Get<Delegation>(message.Id);
            var patients   = this.patientService.FindByTaxon(filter.Id, false)
                .Select(x => new SelectListItem
                {
                    Text = string.Format("{0} {1}", x.FirstName, x.LastName),
                    Value = x.Id.ToString()
                }).ToList();
            var selected = delegation.OrganisationTaxon == null ? filter : this.taxonomyService.Find(delegation.OrganisationTaxon.Id, TaxonomicSchema.Organization);
            return new UpdateDelegationModel
            {
                Id                       = delegation.Id,
                StartDate                = delegation.StartDate,
                EndDate                  = delegation.EndDate,
                ConnectedPatients        = delegation.Patients.ToList(),
                PatientItems             = patients,
                OrganizationTaxons       = TaxonomyHelper.CreateItems(user, selected, this.taxonomyService.List(TaxonomicSchema.Organization)),
                OrganizationTaxon        = selected.Id.ToString(),
                ValidForSpecificPatients = delegation.Patients.Count() != 0,
                AccountId                = delegation.Account.Id
            };
        }

        #endregion
    }
}