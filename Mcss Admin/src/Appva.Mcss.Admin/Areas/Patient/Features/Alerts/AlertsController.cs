// <copyright file="AlertsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Alerts
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Appva.Mcss.Admin.Application.Common;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// <c>/patient/b9018260-c432-4aea-914c-a45300a72c71/alert/...</c>
    /// </summary>
    [Authorize]
    [RouteArea("patient"), RoutePrefix("{id:guid}/alert")]
    public sealed class AlertsController : Controller
    {
        #region Routes.

        #region List Alerts.

        /// <summary>
        /// Returns a list of alerts that are either handled or to
        /// be handled.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="year">Optional year filter</param>
        /// <param name="month">Optional month filter</param>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        ////[PermissionsAttribute(Permissions.Alert.Read.Value)]
        public ActionResult List(ListAlert request)
        {
            return this.View();
        }

        #endregion

        #region Handle Alert.

        /// <summary>
        /// Sets a single alert status from unhandled to handled.
        /// </summary>
        /// <param name="id">The task id</param>
        /// <param name="startDate">Optional start date for redirect</param>
        /// <param name="endDate">Optional end date for redirect</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("handle-alert/{taskId:guid}")]
        [HttpGet, /*Validate, ValidateAntiForgeryToken,*/ Dispatch("List", "Alerts")]
        ////[PermissionsAttribute(Permissions.Alert.Handle.Value)]
        public ActionResult Handle(HandleAlert request)
        {
            return this.View();
        }

        #endregion

        #region Handle All Alerts.

        /// <summary>
        /// Sets all alerts statuses from unhandled to handled. 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="startDate">Optional start date for redirect</param>
        /// <param name="endDate">Optional end date for redirect</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("handle-all-alerts")]
        [HttpGet, /*Validate, ValidateAntiForgeryToken,*/ Dispatch("List", "Alerts")]
        ////[PermissionsAttribute(Permissions.Alert.HandleAll.Value)]
        public ActionResult HandleAll(HandleAllAlert request)
        {
            return this.View();
        }

        #endregion

        #region Overview Widget.

        /// <summary>
        /// Partial view used on the overview to show all patient with alerts.
        /// </summary>
        /// <param name="status">Either "notsigned" or empty</param>
        /// <returns><see cref="PartialViewResult"/></returns>
        [Route("~/patient/alert/overview/{status=notsigned}")]
        [HttpGet, Dispatch]
        public PartialViewResult Overview(AlertWidget request)
        {
            return this.PartialView();
        }

        #endregion

        #endregion
    }
}