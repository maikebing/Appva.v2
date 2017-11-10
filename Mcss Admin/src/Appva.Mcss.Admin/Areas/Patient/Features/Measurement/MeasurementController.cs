// <copyright file="MeasurementController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Patient
{
    #region Imports

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/measurement")]
    public sealed class MeasurementController : Controller
    {
        #region Routes

        #region List

        /// <summary>
        /// Lists the available observations.
        /// </summary>
        /// <param name="request">The request<see cref="ListMeasurement"/>.</param>
        /// <returns>ActionResult<see cref="ListMeasurementModel"/>.</returns>
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
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request<see cref="CreateMeasurement"/>.</param>
        /// <returns>ActionResult<see cref="CreateMeasurementModel"/>.</returns>
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
        /// <returns>ActionResult<see cref="UpdateMeasurementModel"/>.</returns>
        [Route("{MeasurementId:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
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
        [Route("{MeasurementId:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "measurement")]
        [PermissionsAttribute(Permissions.Patient.UpdateValue)]
        public ActionResult Update(UpdateMeasurementModel request)
        {
            return this.View();
        }

        #endregion
        
        #region AddValue

        /// <summary>
        /// Get. Add a new mearuement value
        /// </summary>
        /// <param name="request">The request<see cref="AddMeasurementValue"/>.</param>
        /// <returns>ActionResult<see cref="AddMeasurementValueModel"/>.</returns>
        [Route("{measurementid:guid}/addvalue")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult AddValue(AddMeasurementValue request)
        {
            return this.View();
        }

        /// <summary>
        /// Post. Add a new measurement value
        /// </summary>
        /// <param name="request">The request<see cref="AddMeasurementValueModel"/>.</param>
        /// <returns>ActionResult<see cref="ViewMeasurementModel"/>.</returns>
        [Route("{measurementid:guid}/addvalue")]
        [HttpPost, ValidateAntiForgeryToken, Validate, Dispatch("list", "measurement")]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult AddValue(AddMeasurementValueModel request)
        {
            return this.View();
        }

        #endregion
        
        #endregion
    }
}