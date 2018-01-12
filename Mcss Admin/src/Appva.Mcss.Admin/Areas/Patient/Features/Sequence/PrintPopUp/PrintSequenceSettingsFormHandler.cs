// <copyright file="PrintSequenceSettingsFormHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintSequenceSettingsFormHandler : RequestHandler<PrintSequenceSettingsForm, PrintSequence>
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
        /// Initializes a new instance of the <see cref="PrintSequenceSettingsFormHandler"/> class.
        /// </summary>
        public PrintSequenceSettingsFormHandler(IPersistenceContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override PrintSequence Handle(PrintSequenceSettingsForm message)
        {
            return new PrintSequence
            {
                PatientId         = message.PatientId,
                ScheduleId        = message.ScheduleId,
                StartDate         = message.StartDate,
                EndDate           = message.EndDate,
                OnNeedBasis       = message.OnNeedBasis,
                StandardSequences = message.StandardSequneces
            };
        }

        #endregion
    }
}