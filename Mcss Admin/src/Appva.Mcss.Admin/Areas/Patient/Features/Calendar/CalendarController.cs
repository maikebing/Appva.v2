// <copyright file="CalendarController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Calendar
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Appva.Mcss.Admin.Areas.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/calendar")]
    public sealed class CalendarController : IdentityController
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;
        private readonly IPatientService patientService;
        private readonly ISettingsService settingsService;
        private readonly IScheduleService scheduleService;
        private readonly ITaxonFilterSessionHandler filtering;
        private readonly ISequenceService sequenceService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarController"/> class.
        /// </summary>
        public CalendarController(
            IMediator mediator,
            IIdentityService identities,
            IAccountService accounts,
            IPatientService patientService,
            ISettingsService settingsService,
            IScheduleService scheduleService,
            IPersistenceContext context, ILogService logService,
            ITaxonFilterSessionHandler filtering,
            ISequenceService sequenceService
            )
            : base(mediator, identities, accounts)
        {
            this.patientService = patientService;
            this.settingsService = settingsService;
            this.scheduleService = scheduleService;
            this.context = context;
            this.logService = logService;
            this.filtering = filtering;
            this.sequenceService = sequenceService;
        }

        #endregion

        #region Routes.

        #region List.

        /// <summary>
        /// Returns a list 
        /// </summary>
        /// <param name="request">The list request</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Calendar.ReadValue)]
        public ActionResult List(ListCalendar request)
        {
            return View();
        }

        #endregion

        #region Details
	
	    /// <summary>
	    /// Returns details of an event
	    /// </summary>
	    /// <returns></returns>
	    [Route("details")]
	    [HttpGet, Dispatch]
	    public ActionResult Details(CalendarDetails request)
	    {
	        return View();
	    }
	
	    #endregion 

        #region Create.

        /// <summary>
        /// Returns a create event view.
        /// TODO: EventController>Create>date unused parameter.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="date">Optional date</param>
        /// <returns><see cref="EventViewModel"/></returns>
        
        [Route("create")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Calendar.CreateValue)]
        public ActionResult Create(Identity<CreateEventModel> request)
        {
            return View();
        }

        /// <summary>
        /// Creates an event for a patient if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="date">Optional date</param>
        /// <param name="model">The event model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Calendar")]
        [PermissionsAttribute(Permissions.Calendar.CreateValue)]
        public ActionResult Create(CreateEventModel request)
        {
            return View();
        }

        #endregion

        #region Edit.

        /// <summary>
        /// Returns the event edit view.
        /// </summary>
        /// <param name="id">The event id</param>
        /// <param name="date">The event date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("edit")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Calendar.UpdateValue)]
        public ActionResult Edit(EditEventSequence request)
        {
            return this.View();
        }

        /// <summary>
        /// Edits all events.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="seqId">The event id</param>
        /// <param name="date">The date (for redirect)</param>
        /// <param name="model">The event model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("edit")]
        [HttpPost, MultiButton, Validate, ValidateAntiForgeryToken, Dispatch("List", "Calendar")]
        [PermissionsAttribute(Permissions.Calendar.UpdateValue)]
        public ActionResult Edit(EventViewModel request)
        {                
            return View();
        }

        /// <summary>
        /// Creates a new event (task).
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="seqId">The event id</param>
        /// <param name="date">The event date</param>
        /// <param name="model">The event model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("EditThis")]
        [HttpPost, MultiButton, ValidateAntiForgeryToken]
        [PermissionsAttribute(Permissions.Calendar.UpdateValue)]
        public ActionResult EditThis(Guid id, Guid seqId, DateTime date, EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Category.Equals("new"))
                {
                    model.Category = this.sequenceService.CreateCategory(model.NewCategory).ToString();
                }
                this.sequenceService.CreateTask(
                    seqId,
                    new Guid(model.Category),
                    model.Description,
                    date,
                    date.AddDays((model.EndDate - model.StartDate).TotalDays),
                    model.StartTime,
                    model.EndTime,
                    model.Interval,
                    model.Signable,
                    model.VisibleOnOverview,
                    model.AllDay,
                    model.PauseAnyAlerts,
                    model.Absent
                );
                return this.RedirectToAction("List", new { Id = id, StartDate = date });
            }
            return View(model);
        }

        #endregion

        #region Edit Activity.

        /// <summary>
        /// Returns the edit activity view.
        /// </summary>
        /// <param name="id">The task id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("EditActivity")]
        [PermissionsAttribute(Permissions.Calendar.UpdateValue)]
        public ActionResult EditActivity(Guid id, Guid taskId)
        {
            var evt = this.context.Get<Task>(taskId);
            var categories = this.sequenceService.GetCategories();
            return View(new EventViewModel
            {
                TaskId = evt.Id,
                PatientId = evt.Patient.Id,
                Description = evt.Sequence.Description,
                StartDate = (DateTime)evt.StartDate,
                EndDate = (DateTime)evt.EndDate,
                AllDay = evt.AllDay,
                StartTime = string.Format("{0:HH:mm}", evt.StartDate),
                EndTime = string.Format("{0:HH:mm}", evt.EndDate),
                Absent = evt.Absent,
                PauseAnyAlerts = evt.PauseAnyAlerts,
                Signable = evt.CanRaiseAlert,
                VisibleOnOverview = evt.Overview,
                Category = evt.Schedule.ScheduleSettings.Id.ToString(),
                Categories = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                CalendarSettings = this.settingsService.GetCalendarSettings(),
            });
        }

        /// <summary>
        /// Updates the activity if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="taskId">The task id</param>
        /// <param name="date">Optional activity date</param>
        /// <param name="model">THe event model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("EditActivity")]
        [HttpPost, ValidateAntiForgeryToken]
        [PermissionsAttribute(Permissions.Calendar.UpdateValue)]
        public ActionResult EditActivity(Guid id, Guid taskId, DateTime? date, EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var evt = this.context.Get<Task>(taskId);
                evt.Absent = model.Absent;
                evt.PauseAnyAlerts = model.PauseAnyAlerts;
                evt.CanRaiseAlert = model.Signable;
                evt.EndDate = model.EndDate.AddHours(Double.Parse(model.EndTime.Split(':')[0])).AddMinutes(Double.Parse(model.EndTime.Split(':')[1]));
                evt.StartDate = model.StartDate.AddHours(Double.Parse(model.StartTime.Split(':')[0])).AddMinutes(Double.Parse(model.StartTime.Split(':')[1]));
                evt.Scheduled = (DateTime)evt.EndDate;
                evt.Overview = model.VisibleOnOverview;
                this.context.Save(evt);
                return this.RedirectToAction("List", new { Id = id, StartDate = date });
            }
            return View(model);
        }

        #endregion

        #region Remove Or Split View.

        /// <summary>
        /// Deletes an event by id.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="sequenceId">The event id</param>
        /// <param name="date">The redirect date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Remove")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Calendar.DeleteValue)]
        public ActionResult Remove(Guid id, Guid sequenceId, DateTime date)
        {
            var evt = this.sequenceService.Find(sequenceId);
            this.sequenceService.Delete(evt);
            return this.RedirectToAction("List", new { Id = evt.Patient.Id, StartDate = date });
        }

        #endregion

        #region Remove Activity

        /// <summary>
        /// Deletes an activity by id.
        /// </summary>
        /// <param name="id">The task id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("RemoveActivity")]
        [PermissionsAttribute(Permissions.Calendar.DeleteValue)]
        public ActionResult RemoveActivity(Guid id, Guid taskId)
        {
            var evt = this.context.Get<Task>(taskId);
            this.sequenceService.DeleteActivity(evt);
            return this.RedirectToAction("List", new { Id = evt.Patient.Id, StartDate = evt.StartDate });
        }

        #endregion

        #region Overview Gadget

        /// <summary>
        /// Returns the dashboard overview.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        [Route("~/patient/calendar/overview")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Dashboard.ReadCalendarValue)]
        public PartialViewResult Overview(CalendarOverview request)
        {
            return this.PartialView();
        }

        /// <summary>
        /// Ajax request to quittance a task on the overview.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <param name="date">The date</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("~/patient/calendar/quittance")]
        public JsonResult Quittance(Guid id, DateTime date)
        {
            var sequence = this.context.Get<Sequence>(id);
            var tasks = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                .And(x => x.Sequence == sequence)
                .List();
            var task = this.scheduleService.FindTasks(
                date,
                new List<Schedule> { sequence.Schedule },
                new List<Sequence> { sequence },
                tasks,
                new List<Task>()).FirstOrDefault();
            if (task.IsNotNull())
            {
                task.Quittanced = true;
                task.QuittancedBy = Identity();
                if (task.IsTransient)
                {
                    this.context.Save(task);
                }
                else
                {
                    this.context.Update(task);
                }
                this.logService.Info(string.Format("Användare {0} kvitterade händelse {1} (ref. {2})", Identity().FullName, task.Name, task.Id), Identity(), task.Patient);
                return Json(new { success = true, name = Identity().FullName }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, name = Identity().FullName }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ajax request to unquittance a task on the overview.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <param name="date">The date</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("~/patient/calendar/unquittance")]
        public JsonResult UnQuittance(Guid id, DateTime date)
        {
            var sequence = this.context.Get<Sequence>(id);
            var tasks = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                .And(x => x.Sequence == sequence)
                .List();
            var task = this.scheduleService.FindTasks(
                date,
                new List<Schedule> { sequence.Schedule },
                new List<Sequence> { sequence },
                tasks,
                new List<Task>()).FirstOrDefault();
            if (task.IsNotNull())
            {
                task.Quittanced = false;
                task.QuittancedBy = null;
                if (task.IsTransient)
                {
                    this.context.Save(task);
                }
                else
                {
                    this.context.Update(task);
                }
                this.logService.Info(string.Format("Användare {0} ångrade kvittering på händelse {1} (ref. {2})", Identity().FullName, task.Name, task.Id), Identity(), task.Patient);
                return Json(new { success = true, name = Identity().FullName }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, name = Identity().FullName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}