// <copyright file="ActivateAllDelegationsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ActivateAllDelegationsHandler : RequestHandler<ActivateAllDelegations, ListDelegation>
    {
        #region Varibles.

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateAllDelegationsHandler"/> class.
        /// </summary>
        public ActivateAllDelegationsHandler(IIdentityService identityService, IDelegationService delegationService, ITaxonomyService taxonomyService)
        {
            this.identityService   = identityService;
            this.delegationService = delegationService;
            this.taxonomyService   = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListDelegation Handle(ActivateAllDelegations message)
        {
            var category = this.taxonomyService.Find(message.DelegationCategoryId, TaxonomicSchema.Delegation);
            while (! category.IsRoot)
            {
                category = category.Parent;
            }
            var delegations = this.delegationService.List(
                this.identityService.Principal.LocationPath(),
                byAccount:  message.AccountId,
                byCategory: category.Id,
                isActive:   true,
                isPending:  true);
            foreach (var delegation in delegations)
            {
                if (delegation.OrganisationTaxon.Path.StartsWith(this.identityService.Principal.LocationPath()))
                {
                    this.delegationService.Activate(delegation);
                }
            }
            return new ListDelegation { Id = message.AccountId };
        }

        #endregion
    }
}