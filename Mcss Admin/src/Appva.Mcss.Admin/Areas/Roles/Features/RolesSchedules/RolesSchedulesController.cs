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
    using Appva.Mcss.Admin.Areas.Roles.Features.RolesSchedules.Update;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize]
    [RouteArea("roles"), RoutePrefix("schedules")]
    public sealed class RolesSchedulesController : Controller
    {
        #region Routes.

        #region Update Role Schedules.

        [Route("update/{id:guid}")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Update(Identity<UpdateRoleSchedule> request)
        {
            return this.View();
        }

        [Route("update/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Roles")]
        [AlertSuccess("Roll-signeringslista har uppdaterats!")]
        public ActionResult Update(UpdateRoleSchedule request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}