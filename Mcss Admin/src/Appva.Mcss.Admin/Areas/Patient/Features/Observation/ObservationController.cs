// <copyright file="ObservationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// Class ObservationController. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [RouteArea("patient"), RoutePrefix("{id:guid}/observation")]
    public sealed class ObservationController : Controller
    {
        #region Routes.

        #region List

        /// <summary>
        /// Lists available observations.
        /// </summary>
        /// <param name="request">The request<see cref="ListObservation"/>.</param>
        /// <returns>ActionResult<see cref="ListObservationModel"/>.</returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Patient.ReadValue)]
        public ActionResult List(ListObservation request)
        {
            return this.View();
        }

        #endregion

        #region Create

        /// <summary>
        /// Create a new observation.
        /// </summary>
        /// <param name="request">The request<see cref="CreateObservation"/>.</param>
        /// <returns>ActionResult<see cref="CreateObservationModel"/>.</returns>
        [Route("create")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create(CreateObservation request)
        {
            return this.View();
        }

        /// <summary>
        /// Creates an observation if valid.
        /// </summary>
        /// <param name="request">The measurementobservation form request<see cref="CreateObservationModel"/></param>
        /// <returns>A redirect to <see cref="ObservationController.List"/> if valid</returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "observation")]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create(CreateObservationModel request)
        {
            return this.View();
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified observation.
        /// </summary>
        /// <param name="request">The request<see cref="UpdateObservation"/>.</param>
        /// <returns>ActionResult<see cref="UpdateObservationModel"/>.</returns>
        [Route("{observationId:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Patient.UpdateValue)]
        public ActionResult Update(UpdateObservation request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates the specified observation if valid.
        /// </summary>
        /// <param name="request">The request<see cref="UpdateObservationModel"/>.</param>
        /// <returns>A redirect to <see cref="ObservationController.List"/> if valid</returns>
        [Route("{observationId:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "observation")]
        [PermissionsAttribute(Permissions.Patient.UpdateValue)]
        public ActionResult Update(UpdateObservationModel request)
        {
            return this.View();
        }

        #endregion
        
        #region AddValue

        /// <summary>
        /// Get. Add a new observation value
        /// </summary>
        /// <param name="request">The request <see cref="AddObservationValue"/>.</param>
        /// <returns>ActionResult<see cref="AddObservationValueModel"/>.</returns>
        [Route("{observationId:guid}/addvalue")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult AddValue(AddObservationValue request)
        {
            return this.View();
        }

        /// <summary>
        /// Post. Add a new observation value if valid.
        /// </summary>
        /// <param name="request">The request <see cref="AddObservationValueModel"/>.</param>
        /// <returns>A redirect to <see cref="ObservationController.List"/> if valid</returns>
        [Route("{observationId:guid}/addvalue")]
        [HttpPost, ValidateAntiForgeryToken, Validate, Dispatch("list", "observation")]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult AddValue(AddObservationValueModel request)
        {
            return this.View();
        }

        #endregion
        
        #endregion
    }
}