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
        #region List

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Patient.ReadValue)]
        public ActionResult List(ListMeasurement request)
        {
            return this.View();
        }

        #endregion

        #region Create

        /// <summary>
        /// Returns the create measurement observation form.
        /// </summary>
        /// <returns><see cref="CreatePatient"/></returns>
        [Route("create")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create(CreateMeasurement request)
        {
            return this.View();
        }

        /// <summary>
        /// Creates a measurement observation if valid.
        /// </summary>
        /// <param name="request">The measurementobservation form request<see cref="CreateMeasurementModel"/></param>
        /// <returns>A redirect to <see cref="MeasurementController.List"/> if valid</returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "measurement")]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create(CreateMeasurementModel request)
        {
            return this.View();
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified measurement observation.
        /// </summary>
        /// <param name="request">The request<see cref="UpdateMeasurement"/>.</param>
        /// <returns>ActionResult.</returns>
        [Route("{id:guid}/update")]
        [HttpGet, Hydrate, Dispatch()]
        [PermissionsAttribute(Permissions.Patient.UpdateValue)]
        public ActionResult Update(UpdateMeasurement request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates the specified measurement observation if valid.
        /// </summary>
        /// <param name="request">The request<see cref="UpdateMeasurementModel"/>.</param>
        /// <returns>A redirect to <see cref="MeasurementController.List"/> if valid</returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "measurement")]
        [PermissionsAttribute(Permissions.Patient.UpdateValue)]
        public ActionResult Update(UpdateMeasurementModel request)
        {
            return this.View();
        }

        #endregion
    }
}