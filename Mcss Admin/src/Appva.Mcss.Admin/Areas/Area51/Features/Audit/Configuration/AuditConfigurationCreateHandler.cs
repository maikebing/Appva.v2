// <copyright file="AuditConfigurationCreateHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;

    #endregion

    /// <summary>
	/// TODO: Add a descriptive summary to increase readability.
	/// </summary>
    public class AuditConfigurationCreateHandler : RequestHandler<AuditConfiguration, AuditConfiguration>
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
        /// Initializes a new instance of the <see cref="AuditConfigurationCreateHandler"/> class.
		/// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public AuditConfigurationCreateHandler(ITaxonomyService taxonomyService, ISettingsService settingsService)
		{
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
		}

		#endregion

		#region RequestHandler Overrides.

		/// <inheritdoc />
        public override AuditConfiguration Handle(AuditConfiguration message)
		{
            var selected       = message.OrganizationalUnits.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
            IList<Taxon> units = new List<Taxon>();
            if (selected.Length > 0)
            {
                units = this.taxonomyService.ListIn(selected);
            }
            if (units.Count > 0)
            {
                this.settingsService.Upsert(ApplicationSettings.IsAuditCollectionActivated, true);
                this.settingsService.Upsert(
                    ApplicationSettings.AuditConfiguration,
                    AuditLoggingConfiguration.New(units.Select(x => x.Id).ToList()));
            }
            return new AuditConfiguration();
		}

		#endregion
    }
}