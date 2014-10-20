// <copyright file="TaskController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
// <author><a href="mailto:christoffer.rosenqvist@invativa.se">Christoffer Rosenqvist</a></author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Http;
    using Appva.Core.Extensions;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using Appva.Mcss.ResourceServer.Domain.Services;
    using Appva.WebApi.Filters;
    using Common.Logging;
    using Models;
    using Transformers;

    #endregion

    /// <summary>
    /// Task endpoint.
    /// </summary>
    [RoutePrefix("v1/task")]
    public class TaskController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="TaskController"/>.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<TaskController>();

        /// <summary>
        /// The <see cref="ITaskRepository"/>.
        /// </summary>         
        private readonly ITaskRepository taskRepository;

        /// <summary>
        /// The <see cref="ITaxonRepository"/>.
        /// </summary>
        private readonly ITaxonRepository taxonRepository;

        /// <summary>
        /// The <see cref="ISequenceRepository"/>.
        /// </summary>
        private readonly ISequenceRepository sequenceRepository;

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// The <see cref="IInventoryTransactionItemRepository"/>.
        /// </summary>
        private readonly IInventoryTransactionItemRepository inventoryTransactionItemRepository;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskController"/> class.
        /// </summary>
        /// <param name="taskRepository">The <see cref="ITaskRepository"/></param>
        /// <param name="taxonRepository">The <see cref="ITaxonRepository"/></param>
        /// <param name="sequenceRepository">The <see cref="ISequenceRepository"/></param>
        /// <param name="accountRepository">The <see cref="IAccountRepository"/></param>
        /// <param name="inventoryTransactionItemRepository">The <see cref="IInventoryTransactionItemRepository"/></param>
        /// <param name="taskService">The <see cref="ITaskService"/></param>
        /// <param name="inventoryService">The <see cref="IInventoryService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public TaskController(
            ITaskRepository taskRepository, 
            ITaxonRepository taxonRepository, 
            ISequenceRepository sequenceRepository, 
            IAccountRepository accountRepository,
            IInventoryTransactionItemRepository inventoryTransactionItemRepository,
            ITaskService taskService,
            IInventoryService inventoryService,
            ISettingsService settingsService)
        {
            this.taskRepository = taskRepository;
            this.taxonRepository = taxonRepository;
            this.sequenceRepository = sequenceRepository;
            this.accountRepository = accountRepository;
            this.inventoryTransactionItemRepository = inventoryTransactionItemRepository;
            this.taskService = taskService;
            this.inventoryService = inventoryService;
            this.settingsService = settingsService;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="id">TODO: id</param>
        /// <returns>TODO: returns</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var task = this.taskRepository.Get(id);
            if (task.IsNull())
            {
                return this.NotFound();
            }
            var taxons = this.taxonRepository.Search(null, true, new List<string> { "SST" });
            var account = this.accountRepository.Get(this.User.Identity.Id()); 
            return this.Ok(TaskTransformer.ToTaskModel(new List<Task> { task }, task.Scheduled, taxons, account));
        }

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="timeslot">TODO: timeslot</param>
        /// <param name="patientId">TODO: patientId</param>
        /// <param name="categories">TODO: categories</param>
        /// <param name="isNeedsBased">TODO: isNeedsBased</param>
        /// <returns>TODO: return statement</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{timeslot:regex(\\d{4}\\d{2}\\d{2}\\d{2}\\d{2}\\d{2})}/patient/{id:guid}")]
        public IHttpActionResult FindTasksByDateTimeAndPatient([FromUri(Name = "timeslot")] string timeslot, [FromUri(Name = "id")] Guid patientId, [FromUri(Name = "categories")] List<string> categories = null, [FromUri(Name = "is_needs_based")] bool isNeedsBased = false)
        {
            var time = DateTime.ParseExact(timeslot, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var account = this.accountRepository.Get(this.User.Identity.Id());
            var taxons = this.taxonRepository.Search(null, true, new List<string> { "SST" }); 
            var tasks = this.taskService.ListTasksByPatient(patientId, time, time);
            return this.Ok(TaskTransformer.ToTaskModel(tasks, time, taxons, account));
        }

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="model">The create task model</param>
        /// <returns>The created task id</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPost, Validate, Route]
        public IHttpActionResult Create(TaskCreateRequestModel model)
        {
            var sequence = this.sequenceRepository.Get(model.SequenceId);
            if (sequence.IsNull()) 
            {
                return this.NotFound();
            }
            var account = this.accountRepository.Get(this.User.Identity.Id());
            if (account.IsNull())
            {
                return this.NotFound();
            }
            var taxon = this.taxonRepository.Get(model.StatusId);
            if (taxon.IsNull())
            {
                return this.NotFound();
            }
            var inventoryTransactions = new List<InventoryTransactionItem>();
            if (model.InventoryIds.IsNotNull())
            {
                //// TODO: Split inventory ids and SELECT WHERE IN
                inventoryTransactions.AddRange(model.InventoryIds.Select(transaction => this.inventoryTransactionItemRepository.Get(transaction)));
            }
            var task = this.taskRepository.GetBySequenceAndScheduledDate(model.SequenceId, model.DateTimeScheduled);
            if (task.IsNull())
            {
                DateTime? startDate = null;
                DateTime? endDate = null;
                if (sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar)
                {
                    startDate = model.DateTimeScheduled.AddDays((sequence.StartDate.Date - sequence.EndDate.GetValueOrDefault().Date).TotalDays);
                    endDate = model.DateTimeScheduled;
                }
                task = new Task 
                {
                    IsCompleted = true,
                    Name = sequence.Name,
                    Patient = sequence.Patient,
                    Schedule = sequence.Schedule,
                    Sequence = sequence,
                    Taxon = sequence.Patient.Taxon,
                    Scheduled = model.DateTimeScheduled,
                    RangeInMinutesAfter = sequence.RangeInMinutesAfter,
                    RangeInMinutesBefore = sequence.RangeInMinutesBefore,
                    CompletedBy = account,
                    OnNeedBasis = sequence.OnNeedBasis,
                    StatusTaxon = taxon,
                    CompletedDate = DateTime.Now,
                    InventoryTransactions = inventoryTransactions,
                    //// Calendar?
                    StartDate = startDate,
                    EndDate = endDate,
                    AllDay = sequence.AllDay,
                    Absent = sequence.Absent,
                    CanRaiseAlert = sequence.CanRaiseAlert,
                    Overview = sequence.Overview,
                    PauseAnyAlerts = sequence.PauseAnyAlerts
                };
                this.taskRepository.Save(task);
            } 
            else
            {
                task.Modified = DateTime.Now;
                task.IsCompleted = true;
                task.CompletedBy = account;
                task.CompletedDate = DateTime.Now;
                task.StatusTaxon = taxon;
                task.InventoryTransactions = inventoryTransactions;
            }
            foreach (var transaction in inventoryTransactions)
            {
                transaction.Task = task;
            }
            return this.Ok(new { id = task.Id });
        }

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="id">The task id</param>
        /// <param name="model">The update task model</param>
        /// <returns>TODO: Returns?</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPut, Validate, Route("{id:guid}")]
        public IHttpActionResult Update(Guid id, TaskUpdateRequestModel model)
        {
            var task = this.taskRepository.Get(id);
            if (task == null)
            {
                return this.NotFound();
            }
            var account = this.accountRepository.Get(this.User.Identity.Id());
            //// FIXME: Remove this when app is updated
            if (model.StatusId.IsEmpty() || model.StatusId.IsNull())
            {
                model.StatusId = "incomplete";
            }
            switch (model.StatusId)
            {
                case "incomplete":
                    task.Active = false;
                    if (task.InventoryTransactions.IsNotNull() && task.InventoryTransactions.Count > 0)
                    {
                        var transaction = this.inventoryService.RedoInventoryWithdrawal(task, account);
                        task.InventoryTransactions.Add(transaction);
                        Log.Info(x => x("User: {0} ({1}) readded {2} units to inventory {3} by transaction {4}", account.FullName, account.Id, transaction.Value, transaction.Inventory.Id, transaction.Id));
                    }
                    Log.Info(x => x("User: {0} ({1}) changed status of task {2} to incomplete", account.FullName, account.Id, id));
                    break;
            }
            this.taskRepository.Update(task);
            return this.Ok();
        }

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="id">The task id</param>
        /// <returns>Nothing but status 200</returns>
        [HttpPut, Route("{id:guid}/refill")]
        public IHttpActionResult UpdateRefill(Guid id)
        {
            var sequence = this.sequenceRepository.Get(id);
            if (sequence == null)
            {
                return this.NotFound();
            }
            if (! sequence.Refill)
            {
                var account = this.accountRepository.Get(this.User.Identity.Id());
                sequence.Refill = true;
                sequence.RefillOrderedDate = DateTime.Now;
                sequence.RefillOrderedBy = account;
                this.sequenceRepository.Update(sequence);
            }
            return this.Ok();
        }

        /// <summary>
        /// Returns a collection of tasks by query parameters.
        /// </summary>
        /// <param name="nodeId">Optional taxon id</param>
        /// <param name="statusIds">Optional array of statuses, e.g. delayed, completed</param>
        /// <param name="typeIds">Optional array of type keys, e.g. needs_based</param>
        /// <param name="patientId">Optional patient id</param>
        /// <param name="fromDate">Optional from date</param>
        /// <param name="toDate">Optional to date</param>
        /// <returns>A collection of tasks</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("list")]
        public IHttpActionResult List(
            [FromUri(Name = "node_ids")] Guid? nodeId = null, //// TODO: node_id
            [FromUri(Name = "status_ids")] List<string> statusIds = null,
            [FromUri(Name = "type_ids")] List<string> typeIds = null,
            [FromUri(Name = "patient")] Guid? patientId = null, //// TODO: patient_id
            [FromUri(Name = "from_date")] DateTime? fromDate = null,
            [FromUri(Name = "to_date")] DateTime? toDate = null)
            //// TODO: [FromUri(Name = "page")] int page = 1
        {
            bool? isNeedBased = typeIds.IsNotNull() && typeIds.Contains("need_based");
            bool? isCompleted = null;
            bool? isDelayed = null;
            bool? isDelayedHandled = null;
            DateTime? startDate = fromDate.HasValue ? fromDate.GetValueOrDefault().Date : DateTime.Now.Date;
            DateTime? endDate = toDate.HasValue ? toDate.GetValueOrDefault().LastInstantOfDay() : startDate.GetValueOrDefault().LastInstantOfDay();
            if (statusIds != null && statusIds.Contains("completed"))
            {
                isCompleted = true;
            }
            if (statusIds != null && statusIds.Contains("delayed"))
            {
                isDelayed = true;
                isDelayedHandled = false;
                isCompleted = false;
                if (! statusIds.Contains("completed"))
                {
                    startDate = null;
                    endDate = null;
                }
            }
            var retval = new List<BaseTaskModel>();
            var tasks = this.taskRepository.List(new TaskListQuery
            {
                TaxonId = nodeId,
                PatientId = patientId,
                IsNeedBased = isNeedBased,
                IsCompleted = isCompleted,
                IsDelayed = isDelayed,
                IsDelayHandled = isDelayedHandled,
                StartDate = startDate,
                EndDate = endDate
            });
            foreach (var task in tasks)
            {
                var statuses = new List<string>();
                if (task.IsCompleted)
                {
                    statuses.Add("completed");
                }
                if (task.Delayed)
                {
                    statuses.Add("delayed");
                }
                var types = new List<string>();
                if (task.OnNeedBasis)
                {
                    types.Add("need_based");
                }
                retval.Add(new BaseTaskModel
                {
                    Id = ! task.OnNeedBasis ? task.Id : task.Sequence.Id, //// TODO: ?type_ids=need_based skall ge alla tillgänglig vid behovs ordinationer ?type_ids=need_based&status_ids=completed skall ge alla signerad vid behov
                    Category = task.Schedule.ScheduleSettings.Name,
                    Completed = task.IsCompleted ? new CompletedDetailsModel
                    { 
                        Accounts = new List<string> { task.CompletedBy.FullName },
                        Status = TaxonTransformer.ToStatusItemModel(task.StatusTaxon), 
                        Time = task.CompletedDate.GetValueOrDefault()
                    }
                    : null,
                    DateTimeInterval = new List<string>
                    { 
                        "{0:u}".FormatWith(DateTime.Now.AddMinutes(-task.RangeInMinutesBefore)),
                        "{0:u}".FormatWith(DateTime.Now.AddMinutes(task.RangeInMinutesAfter))
                    },
                    DateTimeScheduled = "{0:u}".FormatWith(task.Scheduled),
                    Name = task.Name,
                    Statuses = statuses,
                    Type = types
                });
            }
            var historyLength = this.settingsService.Get<int>("MCSS.Device.Security.HistoryLength", 7);
            return this.Ok(new TimelineGroupModel<BaseTaskModel>()
            {
                Entities = retval,
                NextDate = string.Format("{0:yyyy-MM-dd}", endDate.HasValue ? endDate.GetValueOrDefault().AddDays(1) : endDate),
                PreviousDate = DateTime.Now.AddDays(-historyLength).Date < startDate.GetValueOrDefault() ? string.Format("{0:yyyy-MM-dd}", startDate.GetValueOrDefault().AddDays(-1)) : null,
                GroupingStrategy = "none",
                CurrentDate = startDate.HasValue ? startDate.GetValueOrDefault().Date.Equals(endDate.GetValueOrDefault().Date) ? string.Format("{0:yyyy-MM-dd}", startDate.GetValueOrDefault()) : "multiple" : "multiple"
            });
        }
                                            
        #region Need Based

        /// <summary>
        /// Lists all currently available NeedBased task for given patient
        /// </summary>
        /// <param name="patientId">The patient id</param>
        /// <returns>Returns a collection of all need based tasks</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("needbased")]
        public IHttpActionResult ListNeedBased([FromUri(Name = "patient")] Guid patientId)
        {
            var sequences =this.sequenceRepository.ListByPatientId(patientId, true, DateTime.Now);
            var needBasedTasks = new List<BaseTaskModel>();
            foreach (var sequence in sequences)
            {               
                var types = new List<string> { "need_based" };
                var lastTask = this.taskRepository.GetLastTaskDateBySequence(sequence.Id);
                needBasedTasks.Add(new BaseTaskModel
                {
                    Id = sequence.Id,
                    Category = sequence.Schedule.ScheduleSettings.Name,
                    Completed = null,
                    DateTimeInterval = new List<string>(),
                    DateTimeScheduled = null,
                    Name = sequence.Name,
                    Statuses = new List<string>(),
                    Type = types,
                    LastCompletion = lastTask
                });
            }
            return this.Ok(new TimelineGroupModel<BaseTaskModel>()
            {
                Entities = needBasedTasks,
                NextDate = null,
                PreviousDate = null,
                GroupingStrategy = "none",
                CurrentDate = "multiple"
            });
        }

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="id">The task id?</param>
        /// <returns>TODO: Returns?</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("needbased/{id:guid}")]
        public IHttpActionResult GetNeedBased(Guid id)
        {
            var sequence = this.sequenceRepository.Get(id);
            if (sequence.IsNull())
            {
                return this.NotFound();
            }
            var task = new Task
            {
                Name = sequence.Name,
                OnNeedBasis = true,
                Patient = sequence.Patient,
                RangeInMinutesAfter = sequence.RangeInMinutesAfter,
                RangeInMinutesBefore = sequence.RangeInMinutesBefore,
                Schedule = sequence.Schedule,
                Sequence = sequence,
                Scheduled = DateTime.Now
            };
            var taxons = this.taxonRepository.Search(null, true, new List<string> { "SST" });
            var account = this.accountRepository.Get(this.User.Identity.Id());
            return this.Ok(TaskTransformer.ToTaskModel(new List<Task> { task }, task.Scheduled, taxons, account));
        }

        #endregion

        #region Calendar

        /// <summary>
        /// TODO: Summary!
        /// </summary>
        /// <param name="scheduled">TODO: scheduled</param>
        /// <param name="sequenceId">TODO: sequenceId</param>
        /// <returns>TODO: Returns?</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{scheduled:regex(\\d{4}\\d{2}\\d{2}\\d{2}\\d{2}\\d{2})}/calendar/{sequence:guid}")]
        public IHttpActionResult GetEventDetails([FromUri(Name = "scheduled")] string scheduled, [FromUri(Name = "sequence")] Guid sequenceId)
        {
            var time = DateTime.ParseExact(scheduled, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var task = this.taskRepository.GetBySequenceAndScheduledDate(sequenceId, time);
            if (task.IsNull())
            {
                var sequence = this.sequenceRepository.Get(sequenceId);
                task = new Task
                {
                    Name = sequence.Name,
                    Sequence = sequence,
                    Schedule = sequence.Schedule,
                    Absent = sequence.Absent,
                    AllDay = sequence.AllDay,
                    CanRaiseAlert = sequence.CanRaiseAlert,
                    EndDate = time,
                    Overview = sequence.Overview,
                    Patient = sequence.Patient,
                    PauseAnyAlerts = sequence.PauseAnyAlerts,
                    Scheduled = time,
                    StartDate = sequence.StartDate.AddDays((time.Date - sequence.EndDate.GetValueOrDefault().Date).TotalDays)
                };
            }
            var account = this.accountRepository.Get(this.User.Identity.Id());
            var stdStatusItems = this.taxonRepository.Search(null, true, new List<string>() { "SST" });
            return this.Ok(TaskTransformer.ToTaskModel(new List<Task> { task }, task.Scheduled, stdStatusItems, account));
        }

        #endregion

        #endregion
    }
}