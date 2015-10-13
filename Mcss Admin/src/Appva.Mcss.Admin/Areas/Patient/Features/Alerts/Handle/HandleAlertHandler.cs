// <copyright file="HandleAlertHandler.cs" company="Appva AB">
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
    internal sealed class HandleAlertHandler : RequestHandler<HandleAlert, ListAlert>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="HandleAlertHandler"/> class.
		/// </summary>
        /// <param name="taskService">The <see cref="ITaskService"/></param>
        public HandleAlertHandler(ITaskService taskService)
		{
            this.taskService = taskService;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListAlert Handle(HandleAlert message)
        {
            this.taskService.HandleAlert(message.TaskId);
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