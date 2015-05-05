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
    using Appva.Mcss.Web.Mappers;
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
        /// The <see cref="IIdentityService"/>.
		/// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="HandleAllAlertHandler"/> class.
		/// </summary>
        /// <param name="settings">The <see cref="IIdentityService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="IAccountService"/> implementation</param>
        public HandleAllAlertHandler(IIdentityService identityService, ITaskService taskService, IAccountService accountService, IPatientService patientService)
		{
            this.identityService = identityService;
            this.taskService = taskService;
            this.accountService = accountService;
            this.patientService = patientService;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListAlert Handle(HandleAllAlert message)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var patient = this.patientService.Get(message.Id);
            this.taskService.HandleAnyAlert(account, patient);
            return new ListAlert
            {
                Id = message.Id,
                Year = message.Year,
                Month = message.Month
            };
        }

        #endregion
    }
}