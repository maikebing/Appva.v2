// <copyright file="RenewDelegationsHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RenewDelegationsHandler : RequestHandler<RenewDelegations, RenewDelegationsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RenewDelegationsHandler"/> class.
        /// </summary>
        public RenewDelegationsHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandlerOverrides

        /// <inheritdoc />
        public override RenewDelegationsModel Handle(RenewDelegations message)
        {
            var category = this.taxonomyService.Find(message.DelegationCategoryId, TaxonomicSchema.Delegation);
            while (! category.IsRoot){
                category = category.Parent;
            }
            return new RenewDelegationsModel
            {
                StartDate               = DateTime.Now.Date,
                EndDate                 = DateTime.Now.AddYears(1).Date,
                AccountId               = message.Id,
                DelegationCategoryId    = category.Id,
                DelegationCategoryName  = category.Name
            };
        }

        #endregion
    }
}