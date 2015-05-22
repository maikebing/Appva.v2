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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Appva.Core.Extensions;
    using Appva.Mcss.Web.Mappers;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;

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
        private readonly IEventService eventService;

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
            IEventService eventService,
            IPersistenceContext context, ILogService logService)
            : base(mediator, identities, accounts)
        {
            this.patientService = patientService;
            this.settingsService = settingsService;
            this.scheduleService = scheduleService;
            this.eventService = eventService;
            this.context = context;
            this.logService = logService;
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
        public ActionResult List(ListCalendar request)
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
        public ActionResult Create(Identity<EventViewModel> request)
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
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        public ActionResult Create(EventViewModel request)
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
        [HttpPost, /*MultiButton,*/ ValidateAntiForgeryToken]
        [Route("EditAll")]
        public ActionResult EditAll(Guid id, Guid seqId, DateTime date, EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Category.Equals("new"))
                {
                    model.Category = this.eventService.CreateCategory(model.NewCategory).ToString();
                }
                this.eventService.Update(
                    seqId,
                    new Guid(model.Category),
                    model.Description,
                    model.StartDate,
                    model.EndDate,
                    model.StartTime,
                    model.EndTime,
                    model.Interval,
                    model.IntervalFactor,
                    model.SpecificDate,
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

        /// <summary>
        /// Creates a new event (task).
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="seqId">The event id</param>
        /// <param name="date">The event date</param>
        /// <param name="model">The event model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, /*MultiButton,*/ ValidateAntiForgeryToken]
        [Route("EditThis")]
        public ActionResult EditThis(Guid id, Guid seqId, DateTime date, EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Category.Equals("new"))
                {
                    model.Category = this.eventService.CreateCategory(model.NewCategory).ToString();
                }
                this.eventService.CreateTask(
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
        public ActionResult EditActivity(Guid id)
        {
            var evt = this.context.Get<Task>(id);
            var categories = this.eventService.GetCategories();
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
        [HttpPost, ValidateAntiForgeryToken]
        [Route("EditActivity")]
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
        /// <param name="id">The event id</param>
        /// <param name="date">The redirect date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("Remove")]
        public ActionResult Remove(Guid id, DateTime date)
        {
            var evt = this.eventService.Get(id);
            this.eventService.DeleteSequence(evt);
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
        public ActionResult RemoveActivity(Guid id)
        {
            var evt = this.context.Get<Task>(id);
            this.eventService.DeleteActivity(evt);
            return this.RedirectToAction("List", new { Id = evt.Patient.Id, StartDate = evt.StartDate });
        }

        #endregion

        #region Overview Gadget

        /// <summary>
        /// Returns the dashboard overview.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        [Route("~/patient/calendar/overview")]
        public PartialViewResult Overview()
        {
            var taxon = FilterCache.Get(this.context);
            if (!FilterCache.HasCache())
            {
                taxon = FilterCache.GetOrSet(Identity(), this.context);
            }
            var categories = this.eventService.GetCategories();
            Patient patientAlias = null;
            Taxon taxonAlias = null;
            var sequences = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                .And(x => x.Overview)
                .JoinAlias(x => x.Patient, () => patientAlias)
                    .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
                .Fetch(x => x.Patient).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                        .Where(x => x.ScheduleType == ScheduleType.Calendar)
                .List();
            var tasks = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                .And(Restrictions.Disjunction().Add<Task>( /// Alla kvitteringar inom veckan
                    x => x.Scheduled < DateTime.Now.AddDays(7) &&
                         x.Scheduled > DateTime.Now.Date &&
                         x.Quittanced
                ).Add<Task>( /// Samtliga tasks som ej kvitterats 
                    x => x.Scheduled < DateTime.Now.Date &&
                         x.Quittanced == false
                ))
                .And(x => x.Overview)
                .OrderBy(x => x.Scheduled).Asc
                .JoinAlias(x => x.Patient, () => patientAlias)
                    .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
                .Fetch(x => x.Patient).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .JoinQueryOver<Sequence>(x => x.Sequence)
                    .Where(x => x.IsActive)
                .JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.ScheduleType == ScheduleType.Calendar)
                .List();
            var schedules = this.context.QueryOver<Schedule>()
                .Where(x => x.IsActive)
                .JoinAlias(x => x.Patient, () => patientAlias)
                    .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.ScheduleType == ScheduleType.Calendar)
                .List();
            var startTime = tasks.Count > 0 ? tasks.FirstOrDefault().Scheduled : DateTime.Now;
            if (startTime > DateTime.Now)
            {
                startTime = DateTime.Now;
            }
            /// Måste gå igenom så att alla sequencer finns. 
            /// Vi kan ha en sekvens som inte ska vara på overview men har en task som är ett undantag och skall vara med på overview
            foreach (var task in tasks)
            {
                if (!sequences.Contains(task.Sequence))
                {
                    sequences.Add(task.Sequence);
                }
            }
            return PartialView(new EventOverviewViewModel
            {
                Activities = this.scheduleService.FindTasks(startTime, DateTime.Now.AddDays(7), schedules, sequences, tasks, new List<Task>())
                    .Where(x => x.Overview && !x.Quittanced && x.Patient.IsActive && !x.Patient.Deceased)
                    .ToList(),
                Categories = categories.IsNotNull() ? categories.ToDictionary(x => categories.IndexOf(x)) : new Dictionary<int, ScheduleSettings>(),
                CalendarColors = this.settingsService.GetCalendarColorQuantity()
            });
        }

        /// <summary>
        /// Ajax request to quittance a task on the overview.
        /// </summary>
        /// <param name="id">The sequence id</param>
        /// <param name="date">The date</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("Quittance")]
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
                this.context.Update(task);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}