// <copyright file="InstallRiskTaxonsHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InstallRiskTaxonsHandler : RequestHandler<InstallRiskTaxons, TaxonIndex>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallRiskTaxonsHandler"/> class.
        /// </summary>
        public InstallRiskTaxonsHandler(ITaxonomyService taxonomies)
        {
            this.taxonomies = taxonomies;
        }

        #endregion

        public override TaxonIndex Handle(InstallRiskTaxons message)
        {
            var installedItems = this.taxonomies.List(TaxonomicSchema.RiskAssessment);

            foreach (var prop in typeof(Taxons).GetFields(BindingFlags.Public | BindingFlags.Static)) 
            {
                var taxon = (ITaxon)prop.GetValue(null);
                if (installedItems.Where(x => x.Type == taxon.Type).SingleOrDefault() == null)
                {
                    this.taxonomies.Save(taxon, TaxonomicSchema.RiskAssessment);
                }
            }
            return new TaxonIndex();
        }
    }
}