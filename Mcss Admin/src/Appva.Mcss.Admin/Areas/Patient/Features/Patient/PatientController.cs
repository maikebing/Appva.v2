// <copyright file="PatientController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Controllers
{
    #region Imports.

    using System.Web.Mvc;
    using System.Web.UI;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient")]
    public sealed class PatientController : Controller
    {
        #region Routes.

        #region List Patients.

        /// <summary>
        /// Returns a sub set of the patients.
        /// </summary>
        /// <param name="request">The query and filtering</param>
        /// <returns><see cref="ListPatientModel"/></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Patient.ReadValue)]
        public ActionResult List(ListPatient request)
        {
            return this.View();
        }

        #endregion

        #region Create Patient.

        /// <summary>
        /// Returns the create patient form.
        /// </summary>
        /// <returns><see cref="CreatePatient"/></returns>
        [Route("create")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<CreatePatient>))]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a patient if valid.
        /// </summary>
        /// <param name="request">The patient form request</param>
        /// <returns>A redirect to <see cref="PatientController.List"/> if valid</returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Patient")]
        [PermissionsAttribute(Permissions.Patient.CreateValue)]
        public ActionResult Create(CreatePatient request)
        {
            return View();
        }

        #endregion

        #region Update Patient.

        /// <summary>
        /// Returns the update patient form.
        /// </summary>
        /// <param name="request">The patient update request</param>
        /// <returns><see cref="UpdatePatient"/></returns>
        [Route("{id:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Patient.UpdateValue)]
        public ActionResult Update(Identity<UpdatePatient> request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates a patient if valid.
        /// </summary>
        /// <param name="request">The update patient request</param>
        /// <returns>If successful; a redirect to <see cref="ScheduleController.List"/></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        [PermissionsAttribute(Permissions.Patient.UpdateValue)]
        public ActionResult Update(UpdatePatient request)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region Reactivate Patient.

        /// <summary>
        /// Reactivates a patient.
        /// FIXME: Should be HttpPost, Validate and ValidateAntiForgeryToken.
        /// </summary>
        /// <param name="request">The reactivation request</param>
        /// <returns>If successful; a redirect to <see cref="PatientController.List"/></returns>
        [Route("{id:guid}/reactivate")]
        [HttpGet, Dispatch("List", "Patient")]
        [PermissionsAttribute(Permissions.Patient.ReactivateValue)]
        public ActionResult Reactivate(ReactivatePatient request)
        {
            return this.View();
        }

        #endregion

        #region Inactivate Patient.

        /// <summary>
        /// Inactivates a patient.
        /// </summary>
        /// <param name="request">The inactivation request</param>
        /// <returns>If successful; a redirect to <see cref="PatientController.List"/></returns>
        [Route("{id:guid}/inactivate")]
        [HttpGet, Dispatch("List", "Patient")]
        [PermissionsAttribute(Permissions.Patient.InactivateValue)]
        public ActionResult Inactivate(InactivatePatient request)
        {
            return this.View();
        }

        #endregion

        #region Verify Unique Patient.

        /// <summary>
        /// Returns whether or not the personal/national identifier number is unique
        /// across the patients.
        /// FIXME: Should be HttpPost, Validate and ValidateAntiForgeryToken.
        /// </summary>
        /// <param name="request">The verify unique request</param>
        /// <returns>True if the patient is unique</returns>
        [Route("is-unique")]
        [HttpPost, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult VerifyUniquePatient(VerifyUniquePatient request)
        {
            return this.JsonPost();
        }

        #endregion

        #region QuickSearch Json.

        /// <summary>
        /// Returns a set of patients by filters.
        /// </summary>
        /// <param name="request">The quick search request</param>
        /// <returns><see cref="IEnumerable{object}"/></returns>
        [Route("quicksearch")]
        [HttpGet, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult QuickSearch(QuickSearchPatient request)
        {
            return this.JsonGet();
        }

        #endregion

        #endregion
    }
}