// <copyright file="RolesCategoriesPermissionsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Roles.Roles
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("roles"), RoutePrefix("categories")]
    public sealed class RolesCategoriesPermissionsController : Controller
    {
        #region Routes.

        #region Update Role Schedules.

        /// <summary>
        /// Update article category permissions.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update/{id:guid}")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Update(Identity<UpdateRolesCategoriesPermissions> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the post request to update article category permissions.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update/{roleId:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Behörighet för artikelkategorier har uppdaterats!")]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Update(UpdateRolesCategoriesPermissions request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}