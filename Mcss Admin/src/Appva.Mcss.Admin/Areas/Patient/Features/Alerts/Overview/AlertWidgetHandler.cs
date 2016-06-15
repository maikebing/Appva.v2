// <copyright file="AlertWidgetHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AlertWidgetHandler : RequestHandler<AlertWidget, AlertOverviewViewModel>
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
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="HandleAlertHandler"/> class.
		/// </summary>
        /// <param name="settings">The <see cref="IIdentityService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="IAccountService"/> implementation</param>
        public AlertWidgetHandler(IIdentityService identityService, IAccountService accountService, ITaxonFilterSessionHandler filtering, IPatientTransformer transformer, IPatientService patientService)
		{
            this.transformer     = transformer;
            this.identityService = identityService;
            this.accountService  = accountService;
            this.patientService  = patientService;
            this.filtering       = filtering;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override AlertOverviewViewModel Handle(AlertWidget message)
        {
            var taxon           = this.filtering.GetCurrentFilter();
            var delayedTasks    = this.patientService.FindDelayedPatientsBy(taxon);
            var incompleteTasks = this.patientService.FindDelayedPatientsBy(taxon, true);
            return new AlertOverviewViewModel
            {
                Patients       = message.Status.Equals("all") ? this.transformer.ToPatientList(delayedTasks) : this.transformer.ToPatientList(incompleteTasks),
                CountAll       = delayedTasks.Count,
                CountNotSigned = incompleteTasks.Count
            };
        }

        #endregion
    }
}