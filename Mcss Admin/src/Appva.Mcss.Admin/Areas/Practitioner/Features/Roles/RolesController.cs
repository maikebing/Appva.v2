// <copyright file="RolesController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Roles
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RolesController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        public RolesController()
        {
        }

        #endregion

        #region Routes.

        //// TODO: Add routes here!

        #endregion
    }

    /*
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Business;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc.Html.DataAnnotations;
    using Appva.Core.Extensions;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Web.ViewModels;
    using NHibernate.Mapping;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class RoleController : AuthorizationController
    {
        [AutoWired]
        public RoleService RoleService
        {
            get;
            set;
        }

        #region Routes.

        [HttpGet]
        public ActionResult Index()
        {
            var query = this.Session.QueryOver<Role>()
                .OrderBy(x => x.Weight).Asc.ThenBy(x => x.Name).Asc;
            if (! User.IsInRole(RoleUtils.AppvaAccount))
            {
                query.Where(x => x.IsVisible == true);
            }
            var roles = query.List();
            return this.View(roles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var permissions = this.Session.QueryOver<Permission>().List();
            return this.View(new RoleFormViewModel
            {
                Permissions = permissions.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = false
                }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var permissions = this.Session.QueryOver<Permission>()
                    .AndRestrictionOn(x => x.Id)
                    .IsIn(model.SelectedPermissions.Select(x => new Guid(x)).ToArray())
                    .List();
                this.Session.Save(new Role
                {
                    Name = model.Name,
                    Description = model.Description,
                    MachineName = null,
                    IsVisible = true,
                    IsDeletable = true,
                    Permissions = permissions
                });
                return this.RedirectToAction("Index");
            }
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var permissions = this.Session.QueryOver<Permission>().List()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = true }).ToList();
            var role = this.Session.Get<Role>(id);
            var selected = role.Permissions.Select(x => x.Id.ToString()).ToArray();
            return this.View(new RoleFormViewModel
                {
                    Name = role.Name,
                    Description = role.Description,
                    Permissions = permissions,
                    SelectedPermissions = selected
                });
        }

        [HttpPost]
        public ActionResult Edit(Guid id, RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var permissions = this.Session.QueryOver<Permission>()
                    .AndRestrictionOn(x => x.Id)
                    .IsIn(model.SelectedPermissions.Select(x => new Guid(x)).ToArray())
                    .List();
                var role = this.Session.Get<Role>(id);
                role.Name = model.Name;
                role.Description = model.Description;
                role.Permissions = permissions;
                this.Session.Update(role);
                return this.RedirectToAction("Index");
            }
            return this.View(model);
        }

        [HttpGet]
        public ActionResult EditRoleSchedule(Guid id)
        {
            var permissions = this.Session.QueryOver<ScheduleSettings>().Where(x => x.Active == true).List()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = true
                }).ToList();
            var role = this.Session.Get<Role>(id);
            var selected = role.ScheduleSettings.Select(x => x.Id.ToString()).ToArray();
            return this.View(new RoleScheduleFormViewModel
            {
                Permissions = permissions,
                SelectedPermissions = selected
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditRoleSchedule(Guid id, RoleScheduleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var permissions = this.Session.QueryOver<ScheduleSettings>()
                    .AndRestrictionOn(x => x.Id)
                    .IsIn(model.SelectedPermissions.Select(x => new Guid(x)).ToArray())
                    .List();
                var role = this.Session.Get<Role>(id);
                role.ScheduleSettings = permissions;
                this.Session.Update(role);
                return this.RedirectToAction("Index");
            }
            return this.View(model);
        }

        #endregion
    }*/
}