// <copyright file="SequenceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Environment;
    using Appva.Core.Logging;
    using Appva.Cqrs;
    using Appva.Domain;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// The sequence http controller.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("sequences")]
    public sealed class SequenceController : Controller
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<SequenceController>();

        /// <summary>
        /// The <see cref="IApplicationEnvironment"/>.
        /// </summary>
        private readonly IApplicationEnvironment environment;

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceController"/> class.
        /// </summary>
        /// <param name="environment">The <see cref="IApplicationEnvironment"/>.</param>
        /// <param name="mediator">The <see cref="IMediator"/>.</param>
        public SequenceController(IApplicationEnvironment environment, IMediator mediator)
        {
            this.environment = environment;
            this.mediator    = mediator;
        }

        #endregion

        #region Create View.

        /// <summary>
        /// Returns a create sequence view.
        /// </summary>
        /// <param name="request">The create sequence request</param>
        /// <returns>A create form</returns>
        [HttpGet, Hydrate]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/create")]
        [Permissions(Permissions.Sequence.CreateValue)]
        public ActionResult Create(CreateSequence request)
        {
            var response = this.mediator.Send(request);
            return this.View(response);
        }

        /// <summary>
        /// Saves a sequence if valid.
        /// </summary>
        /// <param name="request">The create sequence request</param>
        /// <param name="collection">The post collection.</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/create")]
        [Permissions(Permissions.Sequence.CreateValue)]
        public ActionResult Create(CreateSequencePostRequest request, FormCollection collection)
        {
            //// FIXME: Move validation to model.
            if (request.Type == SequenceType.Scheduled && request.Repetition == Repetition.Weekly)
            {
                var start = request.StartDate ?? Date.Today;
                var dows  = request.GetSelectedDaysOfWeek();
                if (dows.Count == 0)
                {
                    ModelState.AddModelError("DaysOfWeek", "");
                }
                if (! dows.Contains(start.DayOfWeek))
                {
                    ModelState.AddModelError("StartDate", "");
                }
            }
            //// HACK: Temporary add the form collection to the request :'(
            request.Collection = collection;
            var response = this.mediator.Send(request);
            return this.RedirectToAction("Details", "Schedule", response);
        }

        #endregion
        
        #region Edit View.

        /// <summary>
        /// Returns the edit form view.
        /// </summary>
        /// <param name="request">The update sequence request</param>
        /// <returns>A sequence edit form</returns>
        [HttpGet, Hydrate]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/{id:guid}/update")]
        [PermissionsAttribute(Permissions.Sequence.UpdateValue)]
        public ActionResult Update(UpdateSequence request)
        {
            var response = this.mediator.Send(request);
            return this.View(response);
        }

        /// <summary>
        /// Updates the sequence if valid.
        /// </summary>
        /// <param name="request">The update sequence request</param>
        /// <param name="collection">The post collection.</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/{id:guid}/update")]
        [PermissionsAttribute(Permissions.Sequence.UpdateValue)]
        public ActionResult Update(UpdateSequenceForm request, FormCollection collection)
        {
            //// HACK: Temporary add the form collection to the request :'(
            request.Collection = collection;
            var response = this.mediator.Send(request);
            return this.RedirectToAction("Details", "Schedule", response);
        }

        #endregion
        
        #region Inactivate.

        /// <summary>
        /// Inactivates a sequence.
        /// </summary>
        /// <param name="request">The inactivate sequence request</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpGet]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/{id:guid}/inactivate")]
        [PermissionsAttribute(Permissions.Sequence.InactivateValue)]
        public ActionResult Inactivate(InactivateSequence request)
        {
            var response = this.mediator.Send(request);
            return this.RedirectToAction("Details", "Schedule", response);
        }

        #endregion

        #region Print View.
        
        /// <summary>
        /// Returns a printable schedule view.
        /// </summary>
        /// <param name="request">The print schedule request</param>
        /// <returns>A pdf file</returns>
        [HttpGet]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/print")]
        [PermissionsAttribute(Permissions.Sequence.PrintValue)]
        public FileContentResult PrintSchema(PrintSequence request)
        {
            var response = this.mediator.Send(request);
            return response;
        }

        #endregion

        #region Print Date Popup View.

        /// <summary>
        /// Returns the print pop up view.
        /// TODO: Rename to PrintSetup.
        /// </summary>
        /// <param name="request">The print sequence request</param>
        /// <returns>The print schedule pop up selection view</returns>
        [HttpGet, Hydrate]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/setup/print")]
        [PermissionsAttribute(Permissions.Sequence.PrintValue)]
        public ActionResult PrintPopUp(PrintSequenceSettings request)
        {
            var response = this.mediator.Send(request);
            return this.View(response);
        }

        /// <summary>
        /// The print pop up postback. 
        /// </summary>
        /// <param name="request">The print sequence request</param>
        /// <returns>Redirects to print if successful</returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [Route("~/{patientId:guid}/schedules/{scheduleId:guid}/sequences/setup/print")]
        [PermissionsAttribute(Permissions.Sequence.PrintValue)]
        public ActionResult PrintPopUp(PrintSequenceSettingsForm request)
        {
            var response = this.mediator.Send(request);
            return this.RedirectToAction("PrintSchema", "Sequence", response);
        }
        
        #endregion
    }
}