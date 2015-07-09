// <copyright file="RolesController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Roles
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("roles"), RoutePrefix("")]
    public sealed class RolesController : Controller
    {
        #region Routes.

        #region List Roles.

        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<IList<Role>>))]
        [PermissionsAttribute(Permissions.Role.ReadValue)]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Create Role.

        [Route("create")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<CreateRole>))]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Create()
        {
            return this.View();
        }

        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Ny roll har skapats!")]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Create(CreateRole request)
        {
            return this.View();
        }

        #endregion

        #region Update Role.

        [Route("update/{id:guid}")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Role.UpdateValue)]
        public ActionResult Update(Identity<UpdateRole> request)
        {
            return this.View();
        }

        [Route("update/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Roll har redigerats!")]
        [PermissionsAttribute(Permissions.Role.UpdateValue)]
        public ActionResult Update(UpdateRole request)
        {
            return this.View();
        }

        #endregion

        #region Delete Role.

        [Route("delete/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Roll har tagits bort!")]
        [PermissionsAttribute(Permissions.Role.DeleteValue)]
        public ActionResult Delete(DeleteRole request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}