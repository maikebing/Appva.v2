// <copyright file="UpdatePatientPublisher.cs" company="Appva AB">
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
    internal sealed class UpdatePatientPublisher : RequestHandler<UpdatePatient, Identity<Guid>>
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
        /// Initializes a new instance of the <see cref="UpdatePatientPublisher"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="ITaxonomyService"/></param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public UpdatePatientPublisher(IPatientService patientService, ITaxonomyService taxonomyService, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override Identity<Guid> Handle(UpdatePatient message)
        {
            var address = this.taxonomyService.Get(message.Taxon.ToGuid());
            var selectedIds = message.SeniorAlerts.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
            IList<Taxon> assessments = null;
            if (selectedIds.Length > 0)
            {
                this.taxonomyService.ListIn(selectedIds);
            }
            Patient patient = null;
            if (! this.patientService.Update(message.Id, message.FirstName, message.LastName, message.PersonalIdentityNumber, message.Tag, message.IsDeceased, address, assessments, out patient))
            {
                throw new Exception("Unable to update patient ID " + message.Id);
            }
            return new Identity<Guid>
            {
                Id = patient.Id
            };
        }

        #endregion
    }
}