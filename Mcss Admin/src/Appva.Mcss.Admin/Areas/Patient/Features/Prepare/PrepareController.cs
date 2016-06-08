// <copyright file="PrepareController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Prepare
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/prepare")]
    public sealed class PrepareController : Controller
    {
        #region Routes.

        #region Schema View.

        /// <summary>
        /// Returns the prepare schema.
        /// </summary>
        /// <param name="request">The schema preparation request</param>
        /// <returns>A <see cref="PrepareSchemaViewModel"/></returns>
        [Route("schema")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Prepare.ReadValue)]
        public ActionResult Schema(SchemaPreparation request)
        {
            return this.View();
        }

        #endregion

        #region Add.

        /// <summary>
        /// Returns the add preparation for a sequence view.
        /// </summary>
        /// <param name="request">The create preparation request</param>
        /// <returns>A <see cref="PrepareAddSequenceViewModel"/></returns>
        [Route("create")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Prepare.CreateValue)]
        public ActionResult Create(CreatePreparation request)
        {
            return this.View();
        }

        /// <summary>
        /// Saves the preparation.
        /// </summary>
        /// <param name="request">The add prepared sequence request</param>
        /// <returns>A <see cref="SchemaPreparation"/></returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Schema", "Prepare")]
        [PermissionsAttribute(Permissions.Prepare.CreateValue)]
        public ActionResult Create(PrepareAddSequenceViewModel request)
        {
            return this.View();
        }

        #endregion

        #region Update.

        /// <summary>
        /// Returns the prepare sequence edit view.
        /// </summary>
        /// <param name="request">The update prepare sequence</param>
        /// <returns>A <see cref="PrepareEditSequenceViewModel"/></returns>
        [Route("update")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Prepare.UpdateValue)]
        public ActionResult Update(UpdatePreparation request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates the prepare sequence if valid.
        /// </summary>
        /// <param name="request">The update prepare sequence request</param>
        /// <returns>A <see cref="SchemaPreparation"/></returns>
        [Route("update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Schema", "Prepare")]
        [PermissionsAttribute(Permissions.Prepare.UpdateValue)]
        public ActionResult Update(PrepareEditSequenceViewModel request)
        {
            return this.View();
        }

        #endregion

        #region Delete.

        /// <summary>
        /// Inactivates a prepared sequence and physically deletes the prepared tasks. 
        /// </summary>
        /// <param name="request">The delete prepared sequence request</param>
        /// <returns>Redirects to <see cref="SchemaPreparation"/></returns>
        [Route("delete")]
        [HttpGet, Dispatch("Schema", "Prepare")]
        [PermissionsAttribute(Permissions.Prepare.InactivateValue)]
        public ActionResult Delete(DeletePreparation request)
        {
            return this.View();
        }

        #endregion

        #region Mark/Unmark

        /// <summary>
        /// Mark/unmark (create/delete) a prepared task for a prepared sequence.
        /// </summary>
        /// <param name="request">The mark/unmark prepared sequence request</param>
        /// <returns>A <see cref="string"/></returns>
        [Route("mark")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Prepare.UpdateValue)]
        public DispatchJsonResult Mark(MarkPreparation request)
        {
            return this.JsonGet();
        }

        #endregion

        #region Print Popup.

        /// <summary>
        /// Returns the print popup view.
        /// </summary>
        /// <param name="request">The print popup request</param>
        /// <returns>A <see cref="PreparePrintPopUpViewModel"/></returns>
        [Route("print/window")]
        [HttpGet, Dispatch]
        public ActionResult PrintPopUp(PrintModalPreparation request)
        {
            return this.View();
        }

        /// <summary>
        /// Print popup button click redirect.
        /// </summary>
        /// <param name="request">The prepared popup request</param>
        /// <returns>A redirect to <see cref="Print"/></returns>
        [Route("print/window")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult PrintPopUp(PreparePrintPopUpViewModel request)
        {
            return this.RedirectToAction(
                "Print",
                new
                {
                    Id         = request.Id,
                    ScheduleId = request.ScheduleId,
                    StartDate  = request.PrintStartDate,
                    EndDate    = request.PrintEndDate
                });
        }

        #endregion

        #region Print.

        /// <summary>
        /// Returns the print view.
        /// </summary>
        /// <param name="request">The print preparation request</param>
        /// <returns>A pdf file</returns>
        [Route("print")]
        [HttpGet, Dispatch]
        public PdfFileResult Print(PrintPreparation request)
        {
            return this.PdfFile();
        }

        #endregion

        #endregion
    }
}