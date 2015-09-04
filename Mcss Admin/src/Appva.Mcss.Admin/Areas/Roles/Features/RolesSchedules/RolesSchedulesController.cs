// <copyright file="RolesController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Roles
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Roles.Features.RolesSchedules.Update;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("roles"), RoutePrefix("schedules")]
    public sealed class RolesSchedulesController : Controller
    {
        #region Routes.

        #region Update Role Schedules.

        [Route("update/{id:guid}")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Update(Identity<UpdateRoleSchedule> request)
        {
            return this.View();
        }

        [Route("update/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Roll-signeringslista har uppdaterats!")]
        [PermissionsAttribute(Permissions.Role.CreateValue)]
        public ActionResult Update(UpdateRoleSchedule request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}