// <copyright file="StatusTaxonIndexHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class TaxonIndexHandler : RequestHandler<TaxonIndex, TaxonIndexModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonIndexHandler"/> class.
        /// </summary>
        public TaxonIndexHandler(ITaxonomyService taxonomies)
        {
            this.taxonomies = taxonomies;
        }

        #endregion

        public override TaxonIndexModel Handle(TaxonIndex message)
        {
            return new TaxonIndexModel
            {
                InstalledItems = this.taxonomies.List(TaxonomicSchema.RiskAssessment)
            };
        }
    }
}