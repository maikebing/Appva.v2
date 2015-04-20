// <copyright file="InactivateSequenceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Patient.Features.Sequence.Inactivate;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InactivateSequenceHandler : NotificationHandler<InactivateSequence>
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
        /// Initializes a new instance of the <see cref="InactivateSequenceHandler"/> class.
        /// </summary>
        public InactivateSequenceHandler(IPersistenceContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region NotificationHandler<InactivateSequence> Overrides.

        /// <inheritdoc />
        public override void Handle(InactivateSequence message)
        {
            var entity = this.context.Get<Sequence>(message.SequenceId);
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.Now;
            this.context.Update(entity);
        }

        #endregion
    }
}