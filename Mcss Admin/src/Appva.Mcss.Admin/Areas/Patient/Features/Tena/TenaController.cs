// <copyright file="TenaController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Patient.Features.Tena
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Security;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/tena")]
    public sealed class TenaController : Controller
    {
        #region Routes.

        #region List Tena.

        /// <summary>
        /// Displays the observations list or the activation view.
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public ActionResult List(ListTena request)
        {
            return this.View();
        }

        #endregion

        #region Find.

        /// <summary>
        /// Search for a patient by the external id.
        /// </summary>
        /// <param name="request">The <see cref="FindTenaId"/>.</param>
        /// <returns>A <see cref="JsonResult"/>.</returns>
        [Route("find")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ActivateValue)]
        public DispatchJsonResult Find(FindTenaId request)
        {
            return this.JsonGet();
        }

        #endregion

        #region Activate.

        /// <summary>
        /// Activates TENA for a patient.
        /// </summary>
        /// <param name="request">The <see cref="ActivateTenaId"/>.</param>
        /// <returns>A <see cref="ListTena"/>.</returns>
        [Route("activate")]
        [HttpPost, Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Tena.ActivateValue)]
        public ActionResult Activate(ActivateTenaId request)
        {
            return this.View();
        }

        #endregion

        #region Check

        [Route("check")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public DispatchJsonResult Check(CheckDate request)
        {
            return this.JsonGet();
        }

        #endregion

        #region Create

        [Route("create")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public ActionResult Create(CreateTenaObserverPeriod request)
        {
            return this.View();
        }

        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken(), Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public ActionResult Create(CreateTenaObserverPeriodModel request)
        {
            return this.View();
        }

        #endregion

        #region View
        [Route("view")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public ActionResult View(ViewTenaMeasurements request)
        {
            return this.View();
        }


        #endregion

        #region Upload

        [Route("upload")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public ActionResult Upload(UploadTenaObserverPeriod request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}