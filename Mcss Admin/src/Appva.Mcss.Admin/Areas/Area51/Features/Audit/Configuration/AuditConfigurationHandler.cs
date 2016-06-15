// <copyright file="AuditConfigurationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Audit
{
	#region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;

	#endregion

	/// <summary>
	/// TODO: Add a descriptive summary to increase readability.
	/// </summary>
    public class AuditConfigurationHandler : RequestHandler<Parameterless<AuditConfiguration>, AuditConfiguration>
	{
		#region Variables.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="AuditConfigurationHandler"/> class.
		/// </summary>
        /// <param name="context">The <see cref="ITaxonomyService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public AuditConfigurationHandler(ITaxonomyService taxonomyService, ISettingsService settingsService)
		{
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
		}

		#endregion

		#region RequestHandler Overrides.

		/// <inheritdoc />
        public override AuditConfiguration Handle(Parameterless<AuditConfiguration> message)
		{
            var configuration       = this.settingsService.AuditLoggingConfiguration();
            var twoLevels           = new List<ITaxon>();
            var taxons              = this.taxonomyService.List(TaxonomicSchema.Organization);
            foreach (var root in this.taxonomyService.Roots(TaxonomicSchema.Organization))
            {
                foreach (var child in this.taxonomyService.ListByParent(root.Id))
                {
                    foreach (var childofachild in this.taxonomyService.ListByParent(child.Id))
                    {
                        twoLevels.Add(childofachild);
                    }
                    twoLevels.Add(child);
                }
                twoLevels.Add(root);
            }
            var organizationalUnits = taxons.Where(x =>
                (
                    x.Type.IsNotEmpty() &&
                    (
                        x.Type.ToNullSafeLower() == "enhet"        ||
                        x.Type.ToNullSafeLower() == "driftställe"  ||
                        x.Type.ToNullSafeLower() == "organisation"
                    )
                ) || twoLevels.Contains(x))
                .OrderBy(x => x.Sort)
                    .Select(x => new OrganizationalUnit { Id = x.Id, Label = x.Name, IsSelected = configuration.Units != null && configuration.Units.Contains(x.Id), HelpText = x.Description })
                .ToList();
            return new AuditConfiguration { OrganizationalUnits = organizationalUnits };
		}

		#endregion
    }
}