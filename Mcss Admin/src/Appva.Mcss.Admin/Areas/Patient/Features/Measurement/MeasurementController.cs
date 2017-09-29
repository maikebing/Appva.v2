using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Mvc.Security;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Models;
using Appva.Mvc;

namespace Appva.Mcss.Admin.Controllers
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/measurement")]
    public sealed class MeasurementController : Controller
    {
        public MeasurementController()
        {

        }

        #region List

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Patient.ReadValue)]
        public ActionResult List(ListMeasurement request)
        {
            return this.View();
        }

        #endregion

        #region Create

        /// <summary>
        /// Returns the create patient form.
        /// </summary>
        /// <returns><see cref="CreatePatient"/></returns>
        [Route("create")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create(CreateMeasurement request)
        {
            return View();
        }

        /// <summary>
        /// Creates a patient if valid.
        /// </summary>
        /// <param name="request">The patient form request</param>
        /// <returns>A redirect to <see cref="PatientController.List"/> if valid</returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "measurement")]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create(CreateMeasurementModel request)
        {
            return View();
        }

        #endregion

    }
}