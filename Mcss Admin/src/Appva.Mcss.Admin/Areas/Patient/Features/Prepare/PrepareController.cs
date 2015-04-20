// <copyright file="PrepareController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Prepare
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc.Filters;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using Appva.Mcss.Web.Mappers;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Infrastructure.Controllers;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrepareController : IdentityController
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrepareController"/> class.
        /// </summary>
        public PrepareController(IMediator mediator, IIdentityService identities, IAccountService accounts, IPersistenceContext context, ILogService logService)
            : base(mediator, identities, accounts)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region Routes.

        #region Schema View.

        /// <summary>
        /// Returns the prepare schema.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="startDate">The start date</param>
        /// <returns></returns>
        public ActionResult Schema(Guid id, Guid scheduleId, DateTime? startDate)
        {
            var schedule = this.context.Get<Schedule>(scheduleId);
            if (schedule.ScheduleSettings.HasSetupDrugsPanel)
            {
                if (!startDate.HasValue)
                {
                    startDate = DateTime.Now;
                }
                var StartDate = startDate.Value.FirstDateOfWeek().Date;
                var patient = this.context.Get<Patient>(id);
                var sequences = this.context.QueryOver<PreparedSequence>()
                    .Where(x => x.IsActive)
                    .And(x => x.CreatedAt < StartDate.AddDays(7))
                    .And(x => x.Schedule.Id == schedule.Id)
                    .List();
                var tasks = this.context.QueryOver<PreparedTask>()
                    .Where(x => x.Date >= StartDate && x.Date < StartDate.AddDays(7))
                    .And(x => x.Schedule.Id == schedule.Id)
                    .List();
                var taskWithInActiveSequence = tasks.Where(x => x.PreparedSequence.IsActive == false).ToList();
                foreach (var task in taskWithInActiveSequence)
                {
                    if (!sequences.Any(x => x.Id == task.PreparedSequence.Id))
                    {
                        sequences.Add(task.PreparedSequence);
                    }
                }
                return View(new PrepareSchemaViewModel
                {
                    Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                    Schedule = schedule,
                    StartDate = StartDate,
                    Sequences = sequences,
                    Week = StartDate.GetWeekNumber(),
                    Tasks = tasks,
                    ArchivedWeek = StartDate.IsLessThan(DateTime.Now.FirstDateOfWeek().Date)
                });
            }
            return this.RedirectToAction("Details", "Schedule", new
            {
                id = id,
                scheduleId = scheduleId
            });
        }

        #endregion

        #region Add.

        /// <summary>
        /// Returns the add preparation for a sequence view.
        /// </summary>
        /// <param name="id">TODO: Remove id.</param>
        /// <param name="scheduleId">TODO: Remove scheduleId.</param>
        /// <returns></returns>
        public ActionResult Add(Guid id, Guid scheduleId)
        {
            return View(new PrepareAddSequenceViewModel());
        }

        /// <summary>
        /// Saves the preparation.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(Guid id, Guid scheduleId, PrepareAddSequenceViewModel model)
        {
            var schedule = this.context.Get<Schedule>(scheduleId);
            if (ModelState.IsValid)
            {
                var prepareSequence = new PreparedSequence
                {
                    Name = model.Name,
                    Schedule = schedule
                };
                this.context.Save(prepareSequence);
                return this.RedirectToAction("Schema", new
                {
                    Id = id,
                    ScheduleId = scheduleId,
                    StartDate = DateTime.Now
                });
            }
            return View(model);
        }

        #endregion

        #region Edit.

        /// <summary>
        /// Returns the prepare sequence edit view.
        /// </summary>
        /// <param name="id">The prepare sequence id</param>
        /// <returns><see cref="ActionResult"/></returns>
        public ActionResult Edit(Guid id)
        {
            var sequence = this.context.Get<PreparedSequence>(id);
            return View(new PrepareEditSequenceViewModel
            {
                Id = sequence.Id,
                Name = sequence.Name
            });
        }

        /// <summary>
        /// Updates the prepare sequence if valid.
        /// </summary>
        /// <param name="id">The prepare sequence id</param>
        /// <param name="model">The model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, PrepareEditSequenceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prepareSequence = this.context.Get<PreparedSequence>(id);
                prepareSequence.Name = model.Name;
                this.context.Update(prepareSequence);
                var schedule = prepareSequence.Schedule.Id;
                var patient = prepareSequence.Schedule.Patient.Id;
                return this.RedirectToAction("Schema", new
                {
                    Id = patient,
                    ScheduleId = schedule,
                    StartDate = DateTime.Now
                });
            }
            return View(model);
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
        public ActionResult Delete(Guid id, Guid scheduleId, Guid sequenceId, DateTime startDate)
        {
            var prepareSequence = this.context.Get<PreparedSequence>(sequenceId);
            prepareSequence.IsActive = false;
            prepareSequence.UpdatedAt = startDate.Date.AddDays(-1);
            this.context.Update(prepareSequence);
            var tasks = this.context.QueryOver<PreparedTask>()
                .Where(x => x.PreparedSequence.Id == sequenceId)
                .And(x => x.Date >= startDate)
                .List();
            foreach (var task in tasks)
            {
                this.context.Delete(task);
            }
            return this.RedirectToAction("Schema", new
            {
                Id = id,
                ScheduleId = scheduleId,
                StartDate = startDate
            });
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
        public JsonResult Mark(Guid prepareSequenceId, DateTime date, bool unMark)
        {
            var tasks = this.context.QueryOver<PreparedTask>()
                    .Where(x => x.PreparedSequence.Id == prepareSequenceId)
                    .And(x => x.Date == date)
                    .List();
            if (unMark)
            {
                foreach (var task in tasks)
                {
                    this.context.Delete(task);
                }
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            if (tasks.Count == 0)
            {
                var prepareSequence = this.context.Get<PreparedSequence>(prepareSequenceId);
                var newTask = new PreparedTask
                {
                    Date = date,
                    PreparedBy = Identity(),
                    PreparedSequence = prepareSequence,
                    Schedule = prepareSequence.Schedule
                };
                this.context.Save(newTask);
                return Json(Identity().FullName, JsonRequestBehavior.AllowGet);
            }
            return Json(tasks.First().PreparedBy.FullName, JsonRequestBehavior.AllowGet);
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
        public ActionResult PrintPopUp(Guid id, Guid scheduleId, DateTime time)
        {
            return View(new PreparePrintPopUpViewModel
            {
                PrintStartDate = time.FirstOfMonth(),
                PrintEndDate = time.FirstOfMonth().AddDays(time.DaysInMonth() - 1),
                ScheduleId = scheduleId,
                Id = id
            });
        }

        /// <summary>
        /// Print popup button click redirect.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <param name="model">The print model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId, PreparePrintPopUpViewModel model)
        {
            return this.RedirectToAction("Print",
                    new
                    {
                        Id = id,
                        ScheduleId = scheduleId,
                        StartDate = model.PrintStartDate,
                        EndDate = model.PrintEndDate
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
        public ActionResult Print(Guid id, Guid scheduleId, DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.LastInstantOfDay();
            var patient = this.context.Get<Patient>(id);
            var schedule = this.context.Get<Schedule>(scheduleId);
            var tasks = this.context.QueryOver<PreparedTask>()
                .Where(x => x.Schedule.Id == schedule.Id)
                .And(x => x.Date >= startDate && x.Date <= endDate)
                .List();
            var printSchedule = new Dictionary<DateTime, IDictionary<string, IDictionary<int, string>>>();
            var signatures = new Dictionary<int, IDictionary<string, string>>();
            foreach (var task in tasks)
            {
                var date = task.Date;
                if (!printSchedule.ContainsKey(date.FirstOfMonth()))
                {
                    printSchedule.Add(date.FirstOfMonth(), new Dictionary<string, IDictionary<int, string>>());
                }
                var seqUID = string.Format("{0}:{1}", task.PreparedSequence.Name, task.PreparedSequence.Id);
                if (!printSchedule[date.FirstOfMonth()].ContainsKey(seqUID))
                {
                    printSchedule[date.FirstOfMonth()].Add(seqUID, new Dictionary<int, string>());
                }
                if (!signatures.ContainsKey(date.Month))
                {
                    signatures.Add(date.Month, new Dictionary<string, string>());
                }
                var uid = string.Format("{0}:{1}", task.PreparedBy.FullName, task.PreparedBy.Id);
                if (!signatures[date.Month].ContainsKey(uid))
                {
                    var sign = string.Format("{0}{1}", task.PreparedBy.FirstName.Substring(0, 1), task.PreparedBy.LastName.Substring(0, 1));
                    if (signatures[date.Month].Values.Contains(sign))
                    {
                        var counter = 2;
                        while (signatures[date.Month].Values.Contains(string.Format("{0}{1}", sign, counter)))
                        {
                            counter++;
                        }
                        sign = string.Format("{0}{1}", sign, counter);
                    }
                    signatures[date.Month].Add(uid, sign);
                }
                if (!printSchedule[date.FirstOfMonth()][seqUID].ContainsKey(date.Day))
                {
                    printSchedule[date.FirstOfMonth()][seqUID].Add(date.Day, signatures[date.Month][uid]);
                }
            }
            return View(new PreparePrintViewModel
            {
                PrintSchedule = printSchedule,
                Schedule = schedule,
                Patient = patient,
                Signatures = signatures
            });
        }

        #endregion

        #endregion
    }
}