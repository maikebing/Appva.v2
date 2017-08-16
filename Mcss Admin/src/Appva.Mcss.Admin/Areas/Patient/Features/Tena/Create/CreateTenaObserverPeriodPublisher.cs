// <copyright file="CreateTenaObserverPeriodPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>


namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Areas.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateTenaObserverPeriodPublisher : RequestHandler<CreateTenaObserverPeriodModel, ListTena>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTenaObserverPeriodPublisher"/> class.
        /// </summary>
        public CreateTenaObserverPeriodPublisher(IPatientService patientService, ITenaService tenaService, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
            this.settingsService = settingsService;
        }

        #endregion


        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListTena Handle(CreateTenaObserverPeriodModel message)
        {
            var patient = this.patientService.Get(message.Id);
            var listTena = new ListTena { Id = patient.Id };
            if(this.tenaService.CreateTenaObservervationPeriod(patient, message.StartDate, message.EndDate) == false)
            {
                listTena.Message = "Gick inte att skapa ny period. Överlappande datum.";
            }
            return listTena;
        }

        #endregion
    }
}