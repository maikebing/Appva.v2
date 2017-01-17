// <copyright file="ListDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.List
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListDelegationHandler : RequestHandler<Parameterless<ListDelegationModel>, ListDelegationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDelegationHandler"/> class.
        /// </summary>
        public ListDelegationHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListDelegationModel Handle(Parameterless<ListDelegationModel> message)
        {
            var categories = this.taxonomyService.Roots(TaxonomicSchema.Delegation);
            var delegations = new Dictionary<ITaxon, IList<ITaxon>>();
            foreach (var cat in categories)
            {
                delegations.Add(cat, this.taxonomyService.ListByParent(TaxonomicSchema.Delegation, cat));
            }

            return new ListDelegationModel
            {
                Delegations = delegations
            };
        }

        #endregion
    }
}