﻿// <copyright file="UpdatePatientHandler.cs" company="Appva AB">
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
    using Appva.Mvc.Html.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdatePatientHandler : RequestHandler<Identity<UpdatePatient>, UpdatePatient>
    {
        #region Variables.

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
        /// <param name="patientService">The <see cref="ITaxonomyService"/></param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public UpdatePatientHandler(IPatientService patientService, ITaxonomyService taxonomyService, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override UpdatePatient Handle(Identity<UpdatePatient> message)
        {
            var alternateId = this.settingsService.HasPatientTag();
            var assessable = this.settingsService.HasSeniorAlert();
            var patient = this.patientService.Get(message.Id);
            var assessments = assessable ? this.taxonomyService.List(TaxonomicSchema.RiskAssessment)
                .Select(x => new Assessment 
                    { 
                        Id = x.Id, 
                        Label = x.Name, 
                        Description = x.Description, 
                        ImagePath = x.Type 
                    }).ToList() : null;
            var taxons = this.taxonomyService.List(TaxonomicSchema.Organization);
            return new UpdatePatient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Taxon = patient.Taxon.Id.ToString(),
                Taxons = TaxonomyHelper.SelectList(patient.Taxon, taxons),
                PersonalIdentityNumber = patient.PersonalIdentityNumber,
                IsDeceased = patient.Deceased,
                SeniorAlerts = assessments,
                HasAlternativeIdentifier = this.settingsService.HasPatientTag(),
                Tag = patient.Identifier
            };
        }

        #endregion
    }
}