// <copyright file="CreatePatientPublisher.cs" company="Appva AB">
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
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreatePatientPublisher : RequestHandler<CreatePatient, bool>
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
        /// Initializes a new instance of the <see cref="CreatePatientPublisher"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="ITaxonomyService"/></param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public CreatePatientPublisher(IPatientService patientService, ITaxonomyService taxonomyService, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override bool Handle(CreatePatient message)
        {
            var taxon = this.taxonomyService.Get(message.Taxon.ToGuid());
            if (taxon.IsNull())
            {
                throw new ArgumentNullException("Taxon is null");
            }
            IList<Taxon> assessments = null;
            if (this.settingsService.HasSeniorAlert())
            {
                var selectedIds = message.Assessments.Where(x => x.IsSelected).Select(x => new Guid(x.Id)).ToArray();
                if (selectedIds.Length > 0)
                {
                    assessments = this.taxonomyService.ListIn(selectedIds);
                }
            }
            Patient patient = null;
            return this.patientService.Create(message.FirstName, message.LastName, message.PersonalIdentityNumber, message.Tag, taxon, assessments, message.IsPersonOfPublicInterestOrVip, message.IsPersonWithHightenedSensitivity, out patient);
        }

        #endregion
    }
}