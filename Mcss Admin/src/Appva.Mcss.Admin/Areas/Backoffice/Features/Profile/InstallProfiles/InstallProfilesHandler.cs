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
    /// Install profiles.
    /// </summary>
    public class InstallProfilesHandler : RequestHandler<InstallProfilesModel, TaxonIndex>
    {
        #region Fields.

        /// <summary>
        /// The taxonomyService. 
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallProfilesHandler"/> class.
        /// </summary>
        /// <param name="taxonomyService">The taxonomyService</param>
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

            foreach (var property in typeof(Taxons).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var taxon = (ITaxon)property.GetValue(null);
                taxon.IsActive = false;

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