// <copyright file="UpdateRoleSchedulePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Roles.List
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleSchedulePublisher : RequestHandler<UpdateRoleSchedule, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleSchedulePublisher"/> class.
        /// </summary>
        public UpdateRoleSchedulePublisher(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<UpdateRoleSchedule, bool> Overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateRoleSchedule message)
        {
            var allSchedules = message.Schedules.Concat(message.Categories);
            var settings = this.persistence.QueryOver<ScheduleSettings>()
                .AndRestrictionOn(x => x.Id)
                .IsIn(allSchedules.Where(x => x.IsSelected).Select(x => new Guid(x.Id)).ToArray())
                .List();
            var role = this.persistence.Get<Role>(message.Id);
            role.ScheduleSettings = settings;
            this.persistence.Update(role);
            return true;
        }

        #endregion
    }
}