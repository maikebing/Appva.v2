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
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;

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
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="startDate">The start date</param>
        /// <returns></returns>
        [Route("schema")]
        [HttpGet, Dispatch]
        public ActionResult Schema(SchemaPreparation request)
        {
            return this.View();
        }

        #endregion

        #region Add.

        /// <summary>
        /// Returns the add preparation for a sequence view.
        /// </summary>
        /// <param name="id">TODO: Remove id.</param>
        /// <param name="scheduleId">TODO: Remove scheduleId.</param>
        /// <returns></returns>
        [Route("create")]
        [HttpGet, Dispatch]
        public ActionResult Create(CreatePreparation request)
        {
            return View();
        }

        /// <summary>
        /// Saves the preparation.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Schema", "Prepare")]
        public ActionResult Create(PrepareAddSequenceViewModel request)
        {
            return View();
        }

        #endregion

        #region Update.

        /// <summary>
        /// Returns the prepare sequence edit view.
        /// </summary>
        /// <param name="id">The prepare sequence id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("update")]
        [HttpGet, Dispatch]
        public ActionResult Update(UpdatePreparation request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates the prepare sequence if valid.
        /// </summary>
        /// <param name="id">The prepare sequence id</param>
        /// <param name="model">The model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Schema", "Prepare")]
        public ActionResult Update(PrepareEditSequenceViewModel request)
        {
            return View();
        }

        #endregion

        #region Delete.

        /// <summary>
        /// Inactivates a prepare sequence and physically deletes the prepared tasks. 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="sequenceId">The sequence id</param>
        /// <param name="startDate">The start date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("delete")]
        [HttpGet, /*Validate, ValidateAntiForgeryToken,*/ Dispatch("Schema", "Prepare")]
        public ActionResult Delete(DeletePreparation request)
        {
            return this.View();
        }

        #endregion

        #region Mark/Unmark

        /// <summary>
        /// Mark/unmark (create/delete) a prepared task for a prepared sequence.
        /// </summary>
        /// <param name="prepareSequenceId">The prepared sequence id</param>
        /// <param name="date">The date which is to be marked/unmarked</param>
        /// <param name="unMark">True if delete</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("mark")]
        [HttpGet, /*Validate, ValidateAntiForgeryToken,*/ Dispatch]
        public DispatchJsonResult Mark(MarkPreparation request)
        {
            return this.JsonGet();
        }

        #endregion

        #region Print Popup.

        /// <summary>
        /// Returns the print popup view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="time">The date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("print/window")]
        [HttpGet, Dispatch]
        public ActionResult PrintPopUp(PrintModalPreparation request)
        {
            return View();
        }

        /// <summary>
        /// Print popup button click redirect.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model">The print model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("print/window")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult PrintPopUp(PreparePrintPopUpViewModel request)
        {
            return this.RedirectToAction("Print",
                    new
                    {
                        Id = request.Id,
                        ScheduleId = request.ScheduleId,
                        StartDate = request.PrintStartDate,
                        EndDate = request.PrintEndDate
                    });
        }

        #endregion

        #region Print.

        /// <summary>
        /// Returns the print view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("print")]
        [HttpGet, Dispatch]
        public ActionResult Print(PrintPreparation request)
        {
            return View();
        }

        #endregion

        #endregion
    }
}