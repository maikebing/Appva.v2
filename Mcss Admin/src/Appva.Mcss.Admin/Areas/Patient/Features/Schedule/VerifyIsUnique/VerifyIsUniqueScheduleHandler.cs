// <copyright file="VerifyIsUniqueScheduleHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Transform;
    using Appva.Core.Extensions;
    using Appva.Mcss.Web.Controllers;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class VerifyIsUniqueScheduleHandler : RequestHandler<VerifyIsUniqueSchedule, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyIsUniqueScheduleHandler"/> class.
        /// </summary>
        public VerifyIsUniqueScheduleHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(VerifyIsUniqueSchedule message)
        {
            var count = this.persistence.QueryOver<Schedule>()
                    .Where(x => x.Patient.Id == message.Id)
                    .And(x => x.ScheduleSettings.Id == message.ScheduleSetting)
                    .And(x => x.IsActive == true)
                    .RowCount();
            return count == 0;
        }

        #endregion
    }
}