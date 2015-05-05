// <copyright file="SequenceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc.Filters;
    using Appva.Persistence;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("sequence")]
    public sealed class SequenceController : Controller
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly IMediator mediator;
        private readonly ILogService logService;
        private readonly IScheduleService schedulerService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceController"/> class.
        /// </summary>
        public SequenceController(IMediator mediator, IScheduleService schedulerService, IPersistenceContext context, ILogService logService)
        {
            this.mediator = mediator;
            this.schedulerService = schedulerService;
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region Routes.

        #region Create View.

        /// <summary>
        /// Returns a create sequence view.
        /// </summary>
        /// <param name="request">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns>A create form</returns>
        [Route("patient/{patientId:guid}/schedule/{scheduleId:guid}/create")]
        [HttpGet, Hydrate]
        public ActionResult Create(CreateSequence request)
        {
            return View(this.mediator.Send(request));
        }

        /// <summary>
        /// Saves a sequence if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model"></param>
        /// <returns>A redirect to schedule details if successful</returns>
        [Route("patient/{id:guid}/schedule/{scheduleId:guid}/create")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, Guid scheduleId, SequenceViewModel model)
        {
            //this.mediator.Send(model);
            return this.RedirectToAction("Details", "Schedule", new { Id = id, ScheduleId = scheduleId });
        }

        #endregion

        #region Edit View.

        /// <summary>
        /// Returns the edit form view.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <returns>A sequence edit form</returns>
        [HttpGet, Hydrate]
        [Route("{id:guid}/update")]
        public ActionResult Update(Guid id)
        {
            return this.View(this.mediator.Send(new UpdateSequence
                {
                }));
        }

        /// <summary>
        /// Updates the sequence if valid.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <param name="model">The sequence model</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [Route("{id:guid}/update")]
        public ActionResult Update(Guid id, SequenceViewModel model)
        {
            //this.mediator.Send(model);
            //return this.RedirectToAction("Details", "Schedule", new { Id = patientId, ScheduleId = scheduleId });
            return this.View();
        }

        #endregion

        #region Inactivate.

        /// <summary>
        /// Inactivates a sequence.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Inactivate(Guid id)
        {
            //this.mediator.Publish(new InactivateSequence
            //    {
            //        SequenceId = id
            //    });
            //return this.RedirectToAction("Details", "Schedule", new { Id = patientId, ScheduleId = scheduleId });
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
        [HttpGet]
        public ActionResult Print(Guid id, Guid scheduleId, DateTime startDate, DateTime endDate, bool OnNeedBasis, bool StandardSequences)
        {
            var patient = this.context.Get<Patient>(id);
            var schedule = this.context.Get<Schedule>(scheduleId);
            var sequences = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                .And(x => x.Schedule.Id == schedule.Id);
            if (!OnNeedBasis)
            {
                sequences = sequences.AndNot(x => x.OnNeedBasis);
            }
            if (!StandardSequences)
            {
                sequences = sequences.And(x => x.OnNeedBasis);
            }
            var printable = this.schedulerService.PrintSchedule(startDate, endDate, sequences.List());
            var statusTaxons = schedule.ScheduleSettings.StatusTaxons.Count == 0 ? this.context.QueryOver<Taxon>().Where(x => x.IsActive && x.IsRoot).JoinQueryOver<Taxonomy>(x => x.Taxonomy).Where(x => x.MachineName == "SST").List() : schedule.ScheduleSettings.StatusTaxons.ToList();
            return View("PrintSchema", new PrintViewModel
            {
                From = startDate,
                To = endDate,
                Patient = patient,
                Schedule = schedule.ScheduleSettings,
                StatusTaxons = statusTaxons,
                PrintSchedule = printable,
                EmptySchema = true
            });
        }

        #endregion

        #region Print Date Popup View.

        /// <summary>
        /// Returns the print pop up view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns>The print schedule pop up selection view</returns>
        [HttpGet]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId)
        {
            var patient = this.context.Get<Patient>(id);
            var schedule = this.context.Get<Schedule>(scheduleId);
            return View(new PrintPopUpViewModel
            {
                Id = patient.Id,
                ScheduleSettingsId = schedule.Id,
                StartDate = DateTime.Now.FirstOfMonth(),
                EndDate = DateTime.Now.LastOfMonth()
            });
        }

        /// <summary>
        /// The print pop up post back. 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model">The print model</param>
        /// <returns>Redirects to print if successful</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId, PrintPopUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                return this.RedirectToAction("Print", new
                {
                    Id = id,
                    ScheduleId = scheduleId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    OnNeedBasis = model.OnNeedBasis,
                    StandardSequences = model.StandardSequneces
                });
            }
            return View(model);
        }

        #endregion

        #endregion
    }
    /*
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Business;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc.Html.DataAnnotations;
    using Appva.Core.Extensions;
    using Appva.Mcss.Web.ViewModels;
    using MvcContrib;

    #endregion

    /// <summary>
    /// The sequence controller.
    /// </summary>
    public class SequenceController : AuthorizationController
    {

        #region Public Properties.

        /// <summary>
        /// The <see cref="LogService"/>.
        /// </summary>
        [AutoWired]
        public LogService LogService
        {
            get;
            set;
        }

        #endregion

        #region Create View.

        /// <summary>
        /// Returns a create sequence view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns>A create form</returns>
        [HttpGet]
        public ActionResult Create(Guid id, Guid scheduleId)
        {
            return View(ExecuteCommand<SequenceViewModel>(new BuildSequenceCreateView
            {
                Schedule = Session.Get<Schedule>(scheduleId)
            }));
        }

        /// <summary>
        /// Saves a sequence if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model"></param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, Guid scheduleId, SequenceViewModel model)
        {
            var patient = Session.Get<Patient>(id);
            var schedule = Session.Get<Schedule>(scheduleId);
            if (ModelState.IsValid)
            {
                ExecuteCommand(new CreateOrUpdateSequenceCommand
                {
                    CurrentUser = Identity(),
                    Patient = patient,
                    Schedule = schedule,
                    Model = model
                });
                return this.RedirectToAction<ScheduleController>(c => c.Details(id, scheduleId));
            }
            return View(UpdateModel(patient, schedule, model));
        }

        #endregion

        #region Edit View.

        /// <summary>
        /// Returns the edit form view.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <returns>A sequence edit form</returns>
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            return View(ExecuteCommand<SequenceViewModel>(new BuildSequenceUpdateView
            {
                Sequence = Session.Get<Sequence>(id)
            }));
        }

        /// <summary>
        /// Updates the sequence if valid.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <param name="model">The sequence model</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, SequenceViewModel model)
        {
            var sequence = Session.Get<Sequence>(id);
            var patient = sequence.Patient;
            var schedule = sequence.Schedule;
            if (ModelState.IsValid)
            {
                ExecuteCommand(new CreateOrUpdateSequenceCommand
                {
                    CurrentUser = Identity(),
                    Sequence = sequence,
                    Patient = patient,
                    Schedule = schedule,
                    Model = model,
                    ShouldUpdate = true
                });
                return this.RedirectToAction<ScheduleController>(c => c.Details(patient.Id, schedule.Id));
            }
            return View(UpdateModel(patient, schedule, model));
        }

        #endregion

        #region Inactivate.

        /// <summary>
        /// Inactivates a sequence.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <returns>A redirect to schedule details if successful</returns>
        [HttpGet]
        public ActionResult Inactivate(Guid id)
        {
            var sequence = Session.Get<Sequence>(id);
            ExecuteCommand(new InactivateOrActivateCommand<Sequence>
            {
                Id = sequence.Id
            });
            return this.RedirectToAction<ScheduleController>(c => c.Details(sequence.Patient.Id, sequence.Schedule.Id));
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
        [HttpGet]
        public ActionResult Print(Guid id, Guid scheduleId, DateTime startDate, DateTime endDate, bool OnNeedBasis, bool StandardSequences)
        {
            var patient = Session.Get<Patient>(id);
            var schedule = Session.Get<Schedule>(scheduleId);
            var sequences = Session.QueryOver<Sequence>()
                .Where(x => x.Active == true)
                .And(x => x.Schedule.Id == schedule.Id);
            if (! OnNeedBasis)
            {
                sequences = sequences.AndNot(x => x.OnNeedBasis);
            }
            if (! StandardSequences)
            {
                sequences = sequences.And(x => x.OnNeedBasis);
            }
            var printable = new ScheduleService(Session).PrintSchedule(startDate, endDate, sequences.List());
            var statusTaxons = schedule.ScheduleSettings.StatusTaxons.Count == 0 ? Session.QueryOver<Taxon>().Where(x => x.Active && x.IsRoot).JoinQueryOver<Taxonomy>(x => x.Taxonomy).Where(x => x.MachineName == "SST").List() : schedule.ScheduleSettings.StatusTaxons.ToList();
            return View("PrintSchema", new PrintViewModel
            {
                From = startDate,
                To = endDate,
                Patient = patient,
                Schedule = schedule.ScheduleSettings,
                StatusTaxons = statusTaxons,
                PrintSchedule = printable,
                EmptySchema = true
            });
        }

        #endregion

        #region Print Date Popup View.

        /// <summary>
        /// Returns the print pop up view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns>The print schedule pop up selection view</returns>
        [HttpGet]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId)
        {
            var patient = Session.Get<Patient>(id);
            var schedule = Session.Get<Schedule>(scheduleId);
            return View(new PrintPopUpViewModel
            {
                Id = patient.Id,
                ScheduleSettingsId = schedule.Id,
                StartDate = DateTime.Now.FirstOfMonth(),
                EndDate = DateTime.Now.LastOfMonth()
            });
        }

        /// <summary>
        /// The print pop up post back. 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model">The print model</param>
        /// <returns>Redirects to print if successful</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId, PrintPopUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                return this.RedirectToAction(c => c.Print(id, scheduleId, model.StartDate, model.EndDate, model.OnNeedBasis, model.StandardSequneces));
            }
            return View(model);
        }

        #endregion

        /// <summary>
        /// Hydrates the sequence view model.
        /// TODO: Hydration can be done with attributes instead.
        /// </summary>
        /// <param name="patient">The patient</param>
        /// <param name="schedule">The schedule</param>
        /// <param name="model">The old sequence view model to be updated</param>
        /// <returns>A hydrated sequence view model</returns>
        private SequenceViewModel UpdateModel(Patient patient, Schedule schedule, SequenceViewModel model)
        {
            var reminderRecipients = new RoleService(Session).Roles("reminderrecipients");
            var delegations = Session.QueryOver<Delegation>()
                .Where(x => x.Active == true)
                .And(x => x.Pending == false)
                .And(x => x.StartDate <= DateTime.Now)
                .And(x => x.EndDate >= DateTime.Now)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                .And(x => x.Parent.Id == schedule.ScheduleSettings.DelegationTaxon.Id).List();
            var retval = new Dictionary<Guid, Delegation>();
            foreach (var delegation in delegations)
            {
                if (!retval.ContainsKey(delegation.Taxon.Id))
                {
                    retval.Add(delegation.Taxon.Id, delegation);
                }
            }
            model.Patient = patient;
            model.Delegations = retval.Select(x => new SelectListItem
            {
                Text = x.Value.Name,
                Value = x.Value.Taxon.Id.ToString()
            });
            model.Times = Enumerable.Range(1, 24).Select(x => new CheckBoxViewModel
            {
                Id = x,
                Checked = false
            }).ToList();
            return model;
        }
    }*/
}