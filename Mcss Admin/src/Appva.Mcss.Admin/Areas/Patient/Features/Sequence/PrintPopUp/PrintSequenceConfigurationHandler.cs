// <copyright file="PrintSequenceSettingsHandler.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintSequenceSettingsHandler : RequestHandler<PrintSequenceSettings, PrintSequenceSettingsForm>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintSequenceSettingsHandler"/> class.
        /// </summary>
        public PrintSequenceSettingsHandler(IPersistenceContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override PrintSequenceSettingsForm Handle(PrintSequenceSettings message)
        {
            return new PrintSequenceSettingsForm
            {
                PatientId = message.PatientId,
                ScheduleId = message.ScheduleId,
                StartDate = DateTime.Now.FirstOfMonth(),
                EndDate = DateTime.Now.LastOfMonth()
            };
        }

        #endregion
    }
}