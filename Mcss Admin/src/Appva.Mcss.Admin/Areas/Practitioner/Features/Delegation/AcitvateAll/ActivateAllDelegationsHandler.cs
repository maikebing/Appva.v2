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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ActivateAllDelegationsHandler : RequestHandler<ActivateAllDelegations, ListDelegation>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateAllDelegationsHandler"/> class.
        /// </summary>
        public ActivateAllDelegationsHandler(IDelegationService delegationService, ITaxonomyService taxonomyService)
        {
            this.delegationService = delegationService;
            this.taxonomyService   = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListDelegation Handle(ActivateAllDelegations message)
        {
            var category = this.taxonomyService.Find(message.DelegationCategoryId, TaxonomicSchema.Delegation);
            while (!category.IsRoot)
            {
                category = category.Parent;
            }
            var delegations = this.delegationService.List(
                byAccount:  message.AccountId,
                byCategory: category.Id,
                isActive:   true,
                isPending:  true
                );

            foreach (var delegation in delegations)
            {
                this.delegationService.Activate(delegation);
            }

            return new ListDelegation { Id = message.AccountId };
        }

        #endregion
    }
}