// <copyright file="HandleAllAlertHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class HandleAllAlertHandler : RequestHandler<HandleAllAlert, ListAlert>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="HandleAllAlertHandler"/> class.
		/// </summary>
        /// <param name="taskService">The <see cref="ITaskService"/></param>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        public HandleAllAlertHandler(ITaskService taskService, IPatientService patientService)
		{
            this.taskService = taskService;
            this.patientService = patientService;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListAlert Handle(HandleAllAlert message)
        {
            var patient = this.patientService.Get(message.Id);
            this.taskService.HandleAlertsForPatient(patient);
            return new ListAlert
            {
                Id    = message.Id,
                Year  = message.Year,
                Month = message.Month
            };
        }

        #endregion
    }
}