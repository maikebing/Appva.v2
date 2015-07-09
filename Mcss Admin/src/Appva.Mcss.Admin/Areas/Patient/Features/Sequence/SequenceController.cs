// <copyright file="SequenceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("sequence")]
    public sealed class SequenceController : Controller
    {
        #region Routes.

        #region Create View.

        /// <summary>
        /// Returns a create sequence view.
        /// </summary>
        /// <param name="request">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns>A create form</returns>
        [Route("patient/{id:guid}/schedule/{scheduleId:guid}/create")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Sequence.CreateValue)]
        public ActionResult Create(CreateSequence request)
        {
            return View();
        }

        /// <summary>
        /// Saves a sequence if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model"></param>
        /// <returns>A redirect to schedule details if successful</returns>
        [Route("patient/{id:guid}/schedule/{scheduleId:guid}/create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Details", "Schedule")]
        [PermissionsAttribute(Permissions.Sequence.CreateValue)]
        public ActionResult Create(CreateSequenceForm request)
        {
            return this.View();
        }

        #endregion

        #region Edit View.

        /// <summary>
        /// Returns the edit form view.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <returns>A sequence edit form</returns>
        [Route("{id:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Sequence.UpdateValue)]
        public ActionResult Update(UpdateSequence request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates the sequence if valid.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <param name="model">The sequence model</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Details", "Schedule")]
        [PermissionsAttribute(Permissions.Sequence.UpdateValue)]
        public ActionResult Update(UpdateSequenceForm request)
        {
            return this.View();
        }

        #endregion

        #region Inactivate.

        /// <summary>
        /// Inactivates a sequence.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [Route("{id:guid}/inactivate")]
        [HttpGet, /*Validate, ValidateAntiForgeryToken,*/ Dispatch("Details", "Schedule")]
        [PermissionsAttribute(Permissions.Sequence.InactivateValue)]
        public ActionResult Inactivate(InactivateSequence request)
        {
            return this.View();
        }

        #endregion

        #region Print View.

        /// <summary>
        /// Returns a printable schedule view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="OnNeedBasis">If false skip need based</param>
        /// <param name="StandardSequences">If false only show need based</param>
        /// <returns></returns>
        [HttpGet, Dispatch]
        [Route("{id:guid}/print/{scheduleId:guid}")]
        [PermissionsAttribute(Permissions.Sequence.PrintValue)]
        public ActionResult PrintSchema(PrintSequence request)
        {
            return this.View();
        }

        #endregion

        #region Print Date Popup View.

        /// <summary>
        /// TODO PRINT SETUP!!!
        /// Returns the print pop up view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns>The print schedule pop up selection view</returns>
        [HttpGet, Hydrate, Dispatch]
        [Route("{id:guid}/setup/print/{scheduleId:guid}")]
        [PermissionsAttribute(Permissions.Sequence.PrintValue)]
        public ActionResult PrintPopUp(PrintSequenceSettings request)
        {
            return this.View();
        }

        /// <summary>
        /// The print pop up post back. 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model">The print model</param>
        /// <returns>Redirects to print if successful</returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("PrintSchema", "Sequence")]
        [Route("{id:guid}/setup/print/{scheduleId:guid}")]
        [PermissionsAttribute(Permissions.Sequence.PrintValue)]
        public ActionResult PrintPopUp(PrintSequenceSettingsForm request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}