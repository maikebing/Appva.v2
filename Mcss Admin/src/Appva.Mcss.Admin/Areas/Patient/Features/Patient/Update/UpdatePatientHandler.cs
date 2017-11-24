// <copyright file="UpdatePatientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Web;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using System.Linq;
    using Appva.Mvc;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdatePatientHandler : RequestHandler<Identity<UpdatePatient>, UpdatePatient>
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
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

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
        /// Initializes a new instance of the <see cref="UpdatePatientHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="patientService">The <see cref="ITaxonomyService"/>.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public UpdatePatientHandler(IIdentityService identityService, IAccountService accountService, IPatientService patientService, ITaxonomyService taxonomyService, ISettingsService settingsService)
        {
            this.identityService = identityService;
            this.accountService  = accountService;
            this.patientService  = patientService;
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override UpdatePatient Handle(Identity<UpdatePatient> message)
        {
            var id          = this.identityService.PrincipalId;
            var user        = this.accountService.Find(id);
            var alternateId = this.settingsService.HasPatientTag();
            var assessable  = this.settingsService.HasSeniorAlert();
            var patient     = this.patientService.Get(message.Id);
            var assessments = assessable ? this.taxonomyService.List(TaxonomicSchema.RiskAssessment)
                .Select(x => new Assessment 
                    { 
                        Id          = x.Id.ToString(), 
                        Label       = x.Name, 
                        Description = x.Description, 
                        ImagePath   = x.Type 
                    }).ToList() : null;
            this.SetSelectedAssessments(assessable, patient, assessments);
            var taxons   = this.taxonomyService.List(TaxonomicSchema.Organization);
            var selected = this.taxonomyService.Find(patient.Taxon.Id, TaxonomicSchema.Organization);
            return new UpdatePatient
            {
                Id                               = patient.Id,
                FirstName                        = patient.FirstName,
                LastName                         = patient.LastName,
                Taxon                            = patient.Taxon.Id.ToString(),
                Taxons                           = TaxonomyHelper.CreateItems(user, selected, taxons),
                PersonalIdentityNumber           = patient.PersonalIdentityNumber,
                IsDeceased                       = patient.Deceased,
                Assessments                      = assessments,
                HasAlternativeIdentifier         = this.settingsService.HasPatientTag(),
                Tag                              = patient.Identifier,
                IsPersonOfPublicInterestOrVip    = patient.IsPersonOfPublicInterest,
                IsPersonWithHightenedSensitivity = patient.IsAllDemographicInformationSensitive
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Selects previous assessments if any.
        /// </summary>
        /// <param name="assessable"></param>
        /// <param name="patient"></param>
        /// <param name="assessments"></param>
        private void SetSelectedAssessments(bool assessable, Patient patient, IList<Assessment> assessments)
        {
            if (! assessable)
            {
                return;
            }
            if (assessments == null)
            {
                return;
            }
            var previousAssessments = patient.SeniorAlerts;
            var ids = previousAssessments.Select(x => x.Id).ToList();
            foreach (var assessment in assessments)
            {
                if (ids.Contains(new Guid(assessment.Id)))
                {
                    assessment.IsSelected = true;
                }
            }
        }

        #endregion
    }
}