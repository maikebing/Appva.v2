// <copyright file="RolesResourcePermissionsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Features.RolesResourcePermissions
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("roles"), RoutePrefix("resource")]
    public sealed class RolesResourcePermissionsController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesResourcePermissionsController"/> class.
        /// </summary>
        public RolesResourcePermissionsController()
        {
        }

        #endregion

        #region Routes.

        #region Update Role Schedules.

        /// <summary>
        /// Update resource-permissions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update/{id:guid}")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Update(Identity<UpdateRoleResourcePermissions> request)
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update/{roleId:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Behörighet för mobil enhet har uppdaterats!")]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Update(UpdateRoleResourcePermissions request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}