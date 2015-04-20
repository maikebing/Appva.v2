// <copyright file="InventoryController.cs" company="Appva AB">
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
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Core.Utilities;
    using Appva.Mcss.Web.Mappers;
    using Appva.Mcss.Web.Controllers;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Commands;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InventoryController : IdentityController
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        public InventoryController(IMediator mediator, IIdentityService identities, IAccountService accounts, IPersistenceContext context, ILogService logService)
            : base(mediator, identities, accounts)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region Routes.

        #region List.

        /// <summary>
        /// Returns a list of 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="inventoryId">The inventory id</param>
        /// <param name="year">Optional year</param>
        /// <param name="month">Optional month</param>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <param name="page">Optional page - defaults to 1</param>
        /// <returns><see cref="ActionResult"/></returns>
        public ActionResult List(Guid id, Guid? inventoryId, int? year, int? month, DateTime? startDate, DateTime? endDate, int page = 1)
        {
            var pageSize = 50;
            var patient = this.context.Get<Patient>(id);
            var StartDate = startDate.HasValue ? startDate.Value : DateTime.Now.FirstOfMonth();
            var EndDate = endDate.HasValue ? endDate.Value : DateTime.Now.LastOfMonth().LastInstantOfDay();
            if (year.HasValue)
            {
                StartDate = DateTimeUtilities.January(1, year.Value);
                EndDate   = DateTimeUtilities.December(31, year.Value).LastInstantOfDay();
            }
            if (month.HasValue)
            {
                if (!year.HasValue)
                {
                    year = DateTime.Now.Year;
                }
                StartDate = new DateTime(year.Value, month.Value, 1);
                EndDate = new DateTime(year.Value, month.Value, DateTime.DaysInMonth(year.Value, month.Value)).LastInstantOfDay();
            }
            var inventories = this.context.QueryOver<Sequence>()
                .Where(x => x.Patient.Id == id)
                .JoinQueryOver<Inventory>(x => x.Inventory)
                    .Where(x => x.IsActive)
                .List()
                .Select(x => x.Inventory).ToList();
            if (inventories == null || inventories.Count < 1)
            {
                return View(new ListInventoryViewModel
                {
                    Patient = PatientMapper.ToPatientViewModel(this.context, patient)
                });
            }
            var inventory = this.context.QueryOver<Inventory>()
                .Where(x => x.Id == inventoryId.GetValueOrDefault(inventories.FirstOrDefault().Id))
                .SingleOrDefault();
            var transactions = this.context.QueryOver<InventoryTransactionItem>()
                .Where(x => x.IsActive)
                .And(x => x.Inventory == inventory)
                .And(x => x.CreatedAt >= StartDate)
                .And(x => x.CreatedAt <= EndDate)
                .OrderBy(x => x.CreatedAt).Desc;
            var user = this.Identity();
            this.logService.Info(
                    "Användare {0} läste saldolista sida {1} för boende {2} (REF: {3})."
                    .FormatWith(
                        user.UserName,
                        page,
                        patient.FullName,
                        patient.Id),
                    user,
                    patient,
                    LogType.Read);
            return View(new ListInventoryViewModel
            {
                Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                Inventories = inventories,
                Inventory = inventory,
                Transactions = transactions.Skip((page - 1) * pageSize).Take(pageSize).Future().ToList(),
                TotalTransactionCount = transactions.ToRowCountQuery().FutureValue<int>().Value,
                PageSize = pageSize,
                Page = page,
                StartDate = StartDate,
                EndDate = EndDate,
                Years = DateTimeUtils.GetYearSelectList(patient.CreatedAt.Year, StartDate.Year == EndDate.Year ? StartDate.Year : 0),
                Months = StartDate.Month == EndDate.Month ? DateTimeUtils.GetMonthSelectList(StartDate.Month) : DateTimeUtils.GetMonthSelectList(),
                Year = year,
                Month = month
            });
        }

        #endregion

        #region Add.

        /// <summary>
        /// Returns the add inventory view.
        /// </summary>
        /// <param name="id">The inventory id</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        public ActionResult Add(Guid id, string returnUrl)
        {
            var inventory = this.context.Get<Inventory>(id);
            return View(new InventoryTransactionItemViewModel
            {
                Operation = "add",
                InventoryId = id,
                InventoryName = inventory.Description,
                ReturnUrl = returnUrl
            });
        }

        #endregion

        #region Withdraw.

        /// <summary>
        /// Returns a withdraw from inventory view.
        /// </summary>
        /// <param name="id">The inventory id</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        public ActionResult Withdrawal(Guid id, string returnUrl)
        {
            var inventory = this.context.Get<Inventory>(id);
            return View(new InventoryTransactionItemViewModel
            {
                Operation = "withdrawal",
                InventoryId = id,
                InventoryName = inventory.Description,
                ReturnUrl = returnUrl
            });
        }

        #endregion

        #region Recount.

        /// <summary>
        /// Returns the recount inventory view.
        /// </summary>
        /// <param name="id">The inventory id</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        public ActionResult Recount(Guid id, string returnUrl)
        {
            var inventory = this.context.Get<Inventory>(id);
            return View(new InventoryTransactionItemViewModel
            {
                InventoryId = id,
                ReturnUrl = returnUrl,
                InventoryName = inventory.Description,
                Operation = "recount"
            });
        }

        #endregion

        #region Overview Widget.

        /// <summary>
        /// Returns the dashboard widget.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        public PartialViewResult Overview()
        {
            var taxon = FilterCache.Get(this.context);
            if (!FilterCache.HasCache())
            {
                taxon = FilterCache.GetOrSet(Identity(), this.context);
            }
            var now = DateTime.Now;
            var lastStockCalculationDate = now.AddDays(-30).AddDays(7);
            RecountOverviewItemViewModel dto = null;
            Inventory inventory = null;
            Patient patient = null;
            Schedule schedule = null;
            var allStockCounts = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                .JoinAlias(x => x.Schedule, () => schedule)
                    .Where(() => schedule.IsActive)
                .JoinAlias(x => x.Inventory, () => inventory)
                    .Where(() => inventory.LastRecount < lastStockCalculationDate)
                .JoinAlias(x => x.Patient, () => patient)
                    .Where(() => patient.IsActive && !patient.Deceased)
                    .JoinQueryOver<Taxon>(() => patient.Taxon)
                        .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
                .SelectList(list => list
                    .Select(() => patient.FullName).WithAlias(() => dto.Patient)
                    .Select(() => patient.Id).WithAlias(() => dto.PatientId)
                    .Select(() => inventory.LastRecount).WithAlias(() => dto.LastRecount)
                    .Select(() => inventory.Id).WithAlias(() => dto.InventoryId)
                    .Select(x => x.Name).WithAlias(() => dto.SequenceName)
                )
                .OrderBy(() => inventory.LastRecount).Asc
                .TransformUsing(Transformers.AliasToBean<RecountOverviewItemViewModel>())
                .List<RecountOverviewItemViewModel>();
            return PartialView(new InventoryOverviewViewModel
            {
                DelayedStockCounts = allStockCounts.Where(x => now.Subtract(x.LastRecount.GetValueOrDefault()).TotalDays > 30).ToList(),
                StockCounts = allStockCounts.Where(x => now.Subtract(x.LastRecount.GetValueOrDefault()).TotalDays <= 30).ToList(),
                StockControlIntervalInDays = 30
            });
        }

        #endregion

        #region Create Transaction.

        /// <summary>
        /// Creates a transaction - either an addition, withdrawal or a recount for
        /// the specified inventory.
        /// </summary>
        /// <param name="id">TODO: what id is id?</param>
        /// <param name="model">The inventory transaction model</param>
        /// <param name="returnUrl">The return url</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Create(Guid id, InventoryTransactionItemViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var inventory = this.context.Get<Inventory>(model.InventoryId);
                var previousInventoryLevel = inventory.CurrentLevel;
                switch (model.Operation)
                {
                    case "add":
                        inventory.CurrentLevel += model.Value;
                        break;
                    case "recount":
                        inventory.CurrentLevel = model.Value;
                        inventory.LastRecount = DateTime.Now;
                        break;
                    case "withdrawal":
                        inventory.CurrentLevel = inventory.CurrentLevel - model.Value;
                        break;
                    default:
                        break;
                }
                var transaction = new InventoryTransactionItem
                {
                    Account = Identity(),
                    Description = model.Description,
                    Operation = model.Operation,
                    Value = model.Value,
                    Sequence = model.SequenceId != Guid.Empty ? this.context.Get<Sequence>(model.SequenceId) : null,
                    Task = model.TaskId.Equals(Guid.Empty) ? null : this.context.Get<Task>(model.TaskId),
                    Inventory = inventory,
                    CurrentInventoryValue = inventory.CurrentLevel,
                    PreviousInventoryValue = previousInventoryLevel
                };
                this.context.Save(transaction);
                inventory.Transactions.Add(transaction);
                this.context.Update(inventory);
            }
            return this.Redirect(returnUrl);
        }

        #endregion

        #endregion
    }
}