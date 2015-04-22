// <copyright file="RolesController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Roles
{
    #region Imports.

    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Appva.Core.Resources;
using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
using Appva.Mcss.Admin.Areas.Roles.Roles.List;
using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Mcss.Admin.Models;
using Appva.Mcss.Web.ViewModels;
using Appva.Mvc.Filters;
using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize]
    [RouteArea("roles"), RoutePrefix("")]
    public sealed class RolesController : Controller
    {
        #region Routes.

        #region List Roles.

        //[Permissions(PermissionTypes.ReadRole)]
        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<IList<Role>>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Create Role.

        //[Permissions(PermissionTypes.CreateRole)]
        [Route("create")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<CreateRole>))]
        public ActionResult Create()
        {
            return this.View();
        }

        //[Permissions(PermissionTypes.CreateRole)]
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Ny roll har skapats!")]
        public ActionResult Create(CreateRole request)
        {
            return this.View();
        }

        #endregion

        #region Update Role.

        //[Permissions(PermissionTypes.UpdateRole)]
        [Route("update/{id:guid}")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Update(Identity<UpdateRole> request)
        {
            return this.View();
        }

        //[Permissions(PermissionTypes.UpdateRole)]
        [Route("update/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Roll har redigerats!")]
        public ActionResult Update(UpdateRole request)
        {
            return this.View();
        }

        #endregion

        #region Delete Role.

        //[Permissions(PermissionTypes.UpdateRole)]
        [Route("delete/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Roll har tagits bort!")]
        public ActionResult Delete(DeleteRole request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}