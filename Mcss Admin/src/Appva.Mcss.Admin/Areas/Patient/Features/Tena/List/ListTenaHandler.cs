// <copyright file="ListTenaHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListTenaHandler : RequestHandler<ListTena, ListTenaModel>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private IPatientTransformer patientTransformer;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private ISettingsService settingsService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTenaHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="patientTransformer">The <see cref="IPatientTransformer"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public ListTenaHandler(IPatientService patientService, IPatientTransformer patientTransformer, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.patientTransformer = patientTransformer;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListTenaModel Handle(ListTena message)
        {
            return new ListTenaModel
            {
                PatientViewModel = this.patientTransformer.ToPatient(this.patientService.Get(message.Id)),
                Patient = this.patientService.Get(message.Id),
                IsInstalled = this.settingsService.Find(ApplicationSettings.TenaSettings).IsInstalled,
                Message = message.Message
            };
        }

        #endregion
    }
}