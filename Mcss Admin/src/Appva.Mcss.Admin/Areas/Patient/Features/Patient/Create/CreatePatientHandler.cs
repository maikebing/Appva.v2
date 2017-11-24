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
    using Appva.Mcss.Admin.Application.Security.Identity;
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
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

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
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public CreatePatientHandler(IIdentityService identityService, IAccountService accountService, ITaxonomyService taxonomyService, ISettingsService settingsService)
        {
            this.identityService = identityService;
            this.accountService  = accountService;
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override CreatePatient Handle(Parameterless<CreatePatient> message)
        {
            var id          = this.identityService.PrincipalId;
            var user        = this.accountService.Find(id);
            var assessable  = this.settingsService.HasSeniorAlert();
            var assessments = assessable ? this.taxonomyService.List(TaxonomicSchema.RiskAssessment)
                .Select(x => new Assessment
                {
                    Id = x.Id.ToString(),
                    Label       = x.Name,
                    Description = x.Description,
                    ImagePath   = x.Type
                }).ToList() : null;
            var organizations = this.taxonomyService.List(TaxonomicSchema.Organization);
            return new CreatePatient
            {
                Taxons                   = TaxonomyHelper.CreateItems(user, null, organizations),
                Assessments              = assessments,
                HasAlternativeIdentifier = this.settingsService.HasPatientTag()
            };
        }

        #endregion
    }
}