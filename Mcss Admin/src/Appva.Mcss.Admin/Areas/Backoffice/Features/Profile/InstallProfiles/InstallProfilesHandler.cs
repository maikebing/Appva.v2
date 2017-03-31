// <copyright file="InstallProfilesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using System.Reflection;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Area51.Models;

    #endregion

    /// <summary>
    /// Installs all default profiles
    /// </summary>
    public class InstallProfilesHandler : RequestHandler<InstallProfilesModel, TaxonIndex>
    {
        #region Fields.

        /// <summary>
        /// The Service for Taxonomy 
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallProfilesHandler"/> class.
        /// <param name="taxonomyService">the Itaxonomyservice</param>
        /// </summary>
        public InstallProfilesHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override TaxonIndex Handle(InstallProfilesModel message)
        {
             var installedItems = this.taxonomyService.ListByFilter(TaxonomicSchema.RiskAssessment, null);

            foreach (var prop in typeof(Taxons).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var getTaxon = (ITaxon)prop.GetValue(null);
                var taxon = new TaxonItem(getTaxon.Id, getTaxon.Name, getTaxon.Description, getTaxon.Path, getTaxon.Type, 0, null, false) as ITaxon;

                if (installedItems.Where(x => x.Type == taxon.Type).Any())
                {
                    continue;
                }

                this.taxonomyService.Save(taxon, TaxonomicSchema.RiskAssessment);
            }

            return new TaxonIndex();
        }

        #endregion
    }
}