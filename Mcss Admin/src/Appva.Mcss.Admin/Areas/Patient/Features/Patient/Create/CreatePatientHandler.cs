// <copyright file="CreatePatientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreatePatientHandler : RequestHandler<Parameterless<CreatePatient>, CreatePatient>
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
        /// Initializes a new instance of the <see cref="CreatePatientHandler"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/> implementation</param>
        public CreatePatientHandler(ITaxonomyService taxonomyService, ISettingsService settingsService)
        {
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override CreatePatient Handle(Parameterless<CreatePatient> message)
        {
            var assessable = this.settingsService.HasSeniorAlert();
            var assessments = assessable ? this.taxonomyService.List(TaxonomicSchema.RiskAssessment)
                .Select(x => new Assessment
                {
                    Id = x.Id,
                    Label = x.Name,
                    Description = x.Description,
                    ImagePath = x.Type
                }).ToList() : null;
            var organizationalUnits = this.taxonomyService.List(TaxonomicSchema.Organization);
            return new CreatePatient
            {
                Taxons = TaxonomyHelper.SelectList(organizationalUnits),
                Assessments = assessments,
                HasAlternativeIdentifier = this.settingsService.HasPatientTag(),

                //// TODO: Must use setting
                IsUsingGeneratedUid = true
            };
        }

        #endregion
    }
}