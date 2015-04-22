// <copyright file="UpdateRoleScheduleHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Areas.Roles.Features.RolesSchedules.Update;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc.Html.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleScheduleHandler : RequestHandler<Identity<UpdateRoleSchedule>, UpdateRoleSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleScheduleHandler"/> class.
        /// </summary>
        public UpdateRoleScheduleHandler(IRoleService roleService, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<Id<UpdateRoleSchedule>, UpdateRoleSchedule> Overrides.

        /// <inheritdoc />
        public override UpdateRoleSchedule Handle(Identity<UpdateRoleSchedule> message)
        {
            var role = this.roleService.Find(message.Id);
            var schedules = this.persistence.QueryOver<ScheduleSettings>()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.ScheduleType).Asc.ThenBy(x => x.Name).Asc
                .List();
            return new UpdateRoleSchedule
            {
                Schedules = this.Merge(schedules, role.ScheduleSettings)
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        private IList<Tickable> Merge(IList<ScheduleSettings> items, IList<ScheduleSettings> selected)
        {
            var selections = selected.Select(x => x.Id).ToList();
            var permissions = items.Select(x => new Tickable
            {
                Id = x.Id,
                Label = x.Name
            }).ToList();
            foreach (var permission in permissions)
            {
                if (selections.Contains(permission.Id))
                {
                    permission.IsSelected = true;
                }
            }
            return permissions;
        }

        #endregion
    }
}